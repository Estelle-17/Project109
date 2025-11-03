using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BattleMapScript : MonoBehaviour
{
    [SerializeField]
    private List<List<Tile>> map;
    private List<string> mapTiles;

    public GameObject prefabTile;
    public int column;
    public int row;
    public int centerColumn;
    public int centerRow;
    public int tilePadding;

    public Vector3 CheckTileMapLocationByRowAndColumn(int newColumn, int newRow)
    {
        return map[newColumn][newRow].transform.position;
    }

    public Vector3 CheckTileMapCenterLocation()
    {
        return map[centerColumn][centerRow].transform.position;
    }

    public List<List<Tile>> GetTileMap() { return map; }

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

        centerColumn = column / 2;
        centerRow = row / 2;

        //장애물 예시
        map[5][2].tileState = TileState.Obstacle;
        map[4][2].tileState = TileState.Obstacle;
        map[6][2].tileState = TileState.Obstacle;
    }

    /// <summary>
    /// mapTiles을 통해 원하는 모양의 맵 타일을 생성해주는 함수
    /// </summary>
    public void TileCreateByMap()
    {
        int startX = 0;
        int startZ = 0; 

        map = new List<List<Tile>>();
        for (int columnIndex = 0; columnIndex < mapTiles[0].Length; columnIndex++)
        {
            map.Add(new List<Tile>());
            for (int rowIndex = 0; rowIndex < mapTiles.Count; rowIndex++)
            {
                Tile tile = GameObject.Instantiate(prefabTile).transform.GetComponent<Tile>();
                tile.transform.localPosition = transform.position + new Vector3(startX - columnIndex * tilePadding, 0.01f, startZ + rowIndex * tilePadding);
                tile.transform.parent = transform;
                tile.SetCoord(columnIndex, rowIndex);
                if (mapTiles[columnIndex][rowIndex] == '1')
                {
                    tile.tileState = TileState.Full;
                }
                //tile.CreateRandomTileObject();  //랜덤한 모양의 타일 오브젝트 생성
                map[columnIndex].Add(tile);
            }
        }

        //맵 크기 저장
        column = map.Count;
        row = map[0].Count;

        Debug.Log($"Column: {column}, Row: {row}");

        centerColumn = column / 2;
        centerRow = row / 2;

        //장애물 예시
        map[5][2].tileState = TileState.Obstacle;
        map[4][2].tileState = TileState.Obstacle;
        map[6][2].tileState = TileState.Obstacle;

        SetTileBase();
    }

    //이동할 수 있는 타일들의 외각을 표시해주는 함수
    public void SetTileBase()
    {
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
                                Debug.Log($"Find Line {x}, {y}");
                                map[columnIndex][rowIndex].tileBaseTextureObjects[i].SetActive(true);
                            }
                        }
                        else
                        {
                            Debug.Log($"Find Line {x}, {y}");
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
    void SetMapTiles()
    {
        mapTiles = new List<string>();
        int currentMapStage = 1;
        switch(currentMapStage)
        {
            case 1:
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("00000000000000");
                mapTiles.Add("11111000011001");
                mapTiles.Add("11111110011001");
                mapTiles.Add("11111111111001");
                mapTiles.Add("11111111111001");
                break;
        }
    }

    public bool mapCreateTest;
    private void Update()
    {
        if(mapCreateTest)
        {
            mapCreateTest = false;
            SetMapTiles();
            TileCreateByMap();
            //TileCreate();
        }
    }
}
