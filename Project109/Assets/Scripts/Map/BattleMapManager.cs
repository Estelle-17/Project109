using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class BattleMapManager : MonoBehaviour
{
    [SerializeField]
    private List<List<Tile>> map;
    private List<string> baseMapTiles;
    private List<string> variationLayout;

    private MapData currentMapData;

    public Queue<(int column, int row)> enemySpawnCoord;
    public Queue<(int column, int row)> npcSpawnCoord;

    public GameObject prefabTile;
    public int column;
    public int row;
    public (int column, int row) centerCoord;
    public (int column, int row) rewardCoord;
    public (int column, int row) playerSpawnCoord;
    public float tilePadding;

    public Vector3 CheckTileMapLocationByRowAndColumn(int newColumn, int newRow)
    {
        return map[newColumn][newRow].transform.position;
    }

    public Vector3 CheckTileMapCenterLocation()
    {
        return map[centerCoord.column][centerCoord.row].transform.position;
    }

    public Vector3 CheckTileMapRewardLocation()
    {
        return map[rewardCoord.column][rewardCoord.row].transform.position;
    }
    public Vector3 CheckPlayerSpawnLocation()
    {
        return map[playerSpawnCoord.column][playerSpawnCoord.row].transform.position;
    }

    public List<List<Tile>> GetTileMap() { return map; }
    public Vector3 CheckEnemySpawnPoint()
    {
        if (enemySpawnCoord.Count != 0)
        {
            (int column, int row) coord = enemySpawnCoord.Peek();
            enemySpawnCoord.Dequeue();
            Vector3 pos = map[coord.column][coord.row].transform.position;

            return pos;
        }
        else
        {
            return CheckTileMapCenterLocation();
        }
    }

    public Vector3 CheckNPCSpawnPoint()
    {
        if (npcSpawnCoord.Count != 0)
        {
            (int column, int row) coord = npcSpawnCoord.Peek();
            npcSpawnCoord.Dequeue();
            Vector3 pos = map[coord.column][coord.row].transform.position;

            return pos;
        }
        else
        {
            Debug.Log("No NPC Spawn Point Available, Return Center Location");
            return CheckTileMapCenterLocation();
        }
    }

    void Start()
    {
        enemySpawnCoord = new Queue<(int column, int row)>();
        npcSpawnCoord = new Queue<(int column, int row)>();
    }

    /// <summary>
    /// column, row 데이터를 통해 직사각형 타일을 생성해주는 함수
    /// </summary>
    public void TileCreate()
    {
        int startX = 0;
        int startZ = 0;

        map = new List<List<Tile>>();
        for (int columnIndex = 0; columnIndex < column; columnIndex++)
        {
            map.Add(new List<Tile>());
            for(int rowIndex = 0; rowIndex < row; rowIndex++)
            {
                Tile tile = GameObject.Instantiate(prefabTile).transform.GetComponent<Tile>();
                tile.transform.localPosition = transform.position + new Vector3(startX - columnIndex * tilePadding, 0.01f, startZ + rowIndex * tilePadding);
                tile.transform.parent = transform;
                tile.SetCoord(columnIndex, rowIndex);
                //tile.CreateRandomTileObject();  //랜덤한 모양의 타일 오브젝트 생성
                map[columnIndex].Add(tile);
            }
        }
    }

    /// <summary>
    /// mapTiles을 통해 원하는 모양의 맵 타일을 생성해주는 함수
    /// </summary>
    public void TileCreateByMapData()
    {
        int startX = 0;
        int startZ = 0; 

        map = new List<List<Tile>>();
        for (int columnIndex = 0; columnIndex < baseMapTiles.Count; columnIndex++)
        {
            map.Add(new List<Tile>());
            for (int rowIndex = 0; rowIndex < baseMapTiles[columnIndex].Length; rowIndex++)
            {
                Tile tile = GameObject.Instantiate(prefabTile).transform.GetComponent<Tile>();
                tile.transform.localPosition = transform.position + new Vector3(
                                                        startX - (columnIndex) * tilePadding,
                                                        0.01f,
                                                        startZ + (rowIndex) * tilePadding);
                tile.transform.parent = transform;
                tile.SetCoord(columnIndex, rowIndex);
                if (baseMapTiles[columnIndex][rowIndex] == '1')
                {
                    tile.tileState = TileState.Full;
                }
                else if (baseMapTiles[columnIndex][rowIndex] == 'R')
                {
                    tile.tileState = TileState.Full;
                    rewardCoord = (columnIndex, rowIndex);
                }
                else if (baseMapTiles[columnIndex][rowIndex] == 'S')
                {
                    playerSpawnCoord = (columnIndex, rowIndex);
                }
                else if (baseMapTiles[columnIndex][rowIndex] == 'C')
                {
                    centerCoord = (columnIndex, rowIndex);
                }
                map[columnIndex].Add(tile);
            }
        }

        //맵 크기 저장
        column = map.Count;
        row = map[0].Count;

        Debug.Log($"Column: {column}, Row: {row}");

        ApplyVariationLayout();

        SetMapOutsideLine();
    }

    public void ApplyVariationLayout()
    {
        enemySpawnCoord.Clear();
        npcSpawnCoord.Clear();
        for (int columnIndex = 0; columnIndex < column; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < row; rowIndex++)
            {
                //바리에이션을 보고 장애물 추가
                if (variationLayout[columnIndex][rowIndex] == 'O')
                {
                    map[columnIndex][rowIndex].tileState = TileState.Obstacle;
                }
                else if (variationLayout[columnIndex][rowIndex] == 'E') //몬스터 스폰 위치 저장
                {
                    enemySpawnCoord.Enqueue((columnIndex, rowIndex));
                }
                else if (variationLayout[columnIndex][rowIndex] == 'N') //NPC스폰 위치 저장
                {
                    npcSpawnCoord.Enqueue((columnIndex, rowIndex));
                }
                else
                {   
                    //기본 맵의 벽이 아닐 경우 이동 가능한 타일로 지정
                    if (map[columnIndex][rowIndex].tileState != TileState.Full)
                    {
                        map[columnIndex][rowIndex].tileState = TileState.Empty;
                    }
                }
            }
        }
    }

    //이동할 수 있는 타일들의 외각을 표시해주는 함수
    public void SetMapOutsideLine()
    {
        //현재 외각선 초기화
        for (int columnIndex = 0; columnIndex < column; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < row; rowIndex++)
            {
                foreach(GameObject obj  in map[columnIndex][rowIndex].tileBaseTextureObjects)
                {
                    obj.SetActive(false);
                }
            }
        }

        //이후 장애물과 맵의 끝 부분을 탐색하여 외각선 생성
        for (int columnIndex = 0; columnIndex < column; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < row; rowIndex++)
            {
                //현재 위치가 비어있을 경우
                if (map[columnIndex][rowIndex].tileState == TileState.Empty)
                {
                    //상,하,좌,우 순으로 탐색
                    int[] dirX = { 0, 0, -1, 1 };
                    int[] dirY = { -1, 1, 0, 0 };

                    for (int i = 0; i < 4; i++)
                    {
                        int x = columnIndex + dirX[i];
                        int y = rowIndex + dirY[i];

                        //맵의 범위 내에 있는 경우
                        if (x < column && x >= 0 && y < row && y >= 0)
                        {
                            //탐색된 위치가 이동 불가능한 위치일 때
                            if (map[x][y].tileState == TileState.Full || map[x][y].tileState == TileState.Obstacle)
                            {
                                map[columnIndex][rowIndex].tileBaseTextureObjects[i].SetActive(true);
                            }
                        }
                        else
                        {
                            map[columnIndex][rowIndex].tileBaseTextureObjects[i].SetActive(true);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 선택된 플레이어가 이동할 수 있는 타일들을 찾아주는 함수
    /// </summary>
    public List<Tile> CheckPlayerMoveTiles(Tile moveStart, int canMoveDistance)
    {
        List<Tile> checkList = new List<Tile>();
        
        Queue<Tile> checkNextTiles = new Queue<Tile>();
        Queue<Tile> checkCurrentTiles = new Queue<Tile>();
        checkCurrentTiles.Enqueue(moveStart);

        for (int currentDistance = 0; currentDistance < canMoveDistance; currentDistance++)
        {
            while (checkCurrentTiles.Count != 0)
            {
                Tile t = checkCurrentTiles.Dequeue();

                //상,하,좌,우 순으로 탐색
                int[] dirX = { 0, 0, 1, -1 };
                int[] dirY = { 1, -1, 0, 0 };

                for (int i = 0; i < 4; i++)
                {
                    int x = t.GetCoord().column + dirX[i];
                    int y = t.GetCoord().row + dirY[i];

                    //맵을 넘어가거나 비어있지 않을 경우 제외
                    if (x >= column || y >= row || x < 0 || y < 0 || map[x][y].tileState != TileState.Empty)
                        continue;

                    //플레이어 위치일 경우 제외
                    if (map[x][y].GetCoord().column == moveStart.GetCoord().column && map[x][y].GetCoord().row == moveStart.GetCoord().row)
                        continue;

                    Debug.Log(map[x][y].GetCoordToString() + " OK");
                    map[x][y].tileState = TileState.CanMove;
                    map[x][y].ChangeEffect();

                    checkList.Add(map[x][y]);
                    checkNextTiles.Enqueue(map[x][y]);
                }
            }

            checkCurrentTiles = new Queue<Tile>(checkNextTiles);
            Debug.Log("현재 계산해야 할 타일 갯수 : " + checkCurrentTiles.Count);
            checkNextTiles.Clear();
        }

        return checkList;
    }

    //임시 MapTiles생성 함수
    void UpdateMapData(string stageName, string variationName)
    {
        //맵 데이터에서 기본 베이스와 추가 바리에이션을 찾아서 등록 진행
        MapData mapData;
        if (AssetCacheManager.instance.TryGetMap(stageName, out mapData))
        {
            Debug.Log($"Find MapData: {mapData.mapName}");
            currentMapData = mapData;
            baseMapTiles = mapData.baseLayout;
            foreach(MapVariation variation in mapData.variations)
            {
                if(variation.variationName == variationName)
                {
                    variationLayout = variation.obstacleLayout;
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning($"Failed to Find MapData");
        }
    }
    
    void UpdateMapVariationLayout(string variationName)
    {
        foreach (MapVariation variation in currentMapData.variations)
        {
            if (variation.variationName == variationName)
            {
                variationLayout = variation.obstacleLayout;
                break;
            }
        }
    }

    public void UpdateMapTiles(string stageName, string variationName)
    {
        UpdateMapData(stageName, variationName);
        TileCreateByMapData();
    }

    public void UpdateMapVariationFromName(string variationName)
    {
        UpdateMapVariationLayout(variationName);

        ApplyVariationLayout();

        SetMapOutsideLine();
    }

    public bool mapCreateTest;
    private void Update()
    {
        if(mapCreateTest)
        {
            mapCreateTest = false;
            UpdateMapData("LostTemple", "Elite");
            TileCreateByMapData();
            //TileCreate();
        }
    }
}
