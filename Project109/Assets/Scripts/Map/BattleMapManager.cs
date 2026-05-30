using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class BattleMapManager : MonoBehaviour
{
    [SerializeField]
    private List<List<Tile>> map;

    public MapDataSO currentMapData;

    private List<Vector2Int> enemySpawnTiles = new List<Vector2Int>();
    private List<Vector2Int> playerSpawnTiles = new List<Vector2Int>();
    private List<Vector2Int> npcSpawnTiles = new List<Vector2Int>();
    private List<Vector2Int> obstacleSpawnTiles = new List<Vector2Int>();
    private List<Vector2Int> trapSpawnTiles = new List<Vector2Int>();
    public GameObject prefabTile;
    public int column;
    public int row;
    public (int column, int row) centerCoord;
    public (int column, int row) rewardCoord;
    public (int column, int row) playerSpawnCoord;
    public float tilePadding;

    public Vector3 CheckTileMapLocationByRowAndColumn(int newColumn, int newRow)
    {
        if (map == null)
            return Vector3.zero;
        return map[newColumn][newRow].transform.position;
    }

    public Vector3 CheckTileMapCenterLocation()
    {
        if (map == null)
            return Vector3.zero;
        return map[centerCoord.column][centerCoord.row].transform.position;
    }

    public Vector3 CheckTileMapRewardLocation()
    {
        if (map == null)
            return Vector3.zero;
        return map[rewardCoord.column][rewardCoord.row].transform.position;
    }

    public List<List<Tile>> GetTileMap() { return map; }

    public Vector3 CheckPlayerSpawnLocation()
    {
        if (playerSpawnTiles.Count == 0)
        {
            Debug.LogWarning("No Player Spawn Tiles Available!");
            return map[centerCoord.column][centerCoord.row].transform.position; ; // 또는 적절한 기본 위치 반환
        }

        int randomIndex = Random.Range(0, playerSpawnTiles.Count);
        Vector2Int spawnTile = playerSpawnTiles[randomIndex];
        playerSpawnTiles.RemoveAt(randomIndex); // 스폰된 타일은 리스트에서 제거하여 중복 스폰 방지
        map[spawnTile.x][spawnTile.y].tileState = TileState.Trap; // 스폰된 타일을 Full 상태로 변경
        return map[spawnTile.x][spawnTile.y].transform.position;
    }

    public Vector3 CheckEnemySpawnPoint()
    {
        if (enemySpawnTiles.Count == 0)
        {
            Debug.LogWarning("No Enemy Spawn Tiles Available!");
            return map[centerCoord.column][centerCoord.row].transform.position; ; // 또는 적절한 기본 위치 반환
        }

        int randomIndex = Random.Range(0, enemySpawnTiles.Count);
        Vector2Int spawnTile = enemySpawnTiles[randomIndex];
        enemySpawnTiles.RemoveAt(randomIndex); // 스폰된 타일은 리스트에서 제거하여 중복 스폰 방지
        map[spawnTile.x][spawnTile.y].tileState = TileState.Trap; // 스폰된 타일을 Full 상태로 변경
        return map[spawnTile.x][spawnTile.y].transform.position;
    }

    public Vector3 CheckNPCSpawnPoint()
    {
        if (npcSpawnTiles.Count == 0)
        {
            Debug.LogWarning("No NPC Spawn Tiles Available!");
            return map[centerCoord.column][centerCoord.row].transform.position; ; // 또는 적절한 기본 위치 반환
        }

        int randomIndex = Random.Range(0, npcSpawnTiles.Count);
        Vector2Int spawnTile = npcSpawnTiles[randomIndex];
        npcSpawnTiles.RemoveAt(randomIndex); // 스폰된 타일은 리스트에서 제거하여 중복 스폰 방지
        map[spawnTile.x][spawnTile.y].tileState = TileState.Trap; // 스폰된 타일을 Full 상태로 변경
        return map[spawnTile.x][spawnTile.y].transform.position;
    }

    //임시 코드
    public Vector3 CheckTrapSpawnPoint()
    {
        if (trapSpawnTiles.Count == 0)
        {
            Debug.LogWarning("No Trap Spawn Tiles Available!");
            return map[centerCoord.column][centerCoord.row].transform.position; ; // 또는 적절한 기본 위치 반환
        }
        int randomIndex = Random.Range(0, trapSpawnTiles.Count);
        Vector2Int spawnTile = trapSpawnTiles[randomIndex];
        trapSpawnTiles.RemoveAt(randomIndex); // 스폰된 타일은 리스트에서 제거하여 중복 스폰 방지
        map[spawnTile.x][spawnTile.y].tileState = TileState.Trap; // 스폰된 타일을 Trap 상태로 변경
        return map[spawnTile.x][spawnTile.y].transform.position;
    }

    //임시 코드
    public Vector3 CheckObstacleSpawnPoint()
    {
        if (obstacleSpawnTiles.Count == 0)
        {
            Debug.LogWarning("No Obstacle Spawn Tiles Available!");
            return map[centerCoord.column][centerCoord.row].transform.position; ; // 또는 적절한 기본 위치 반환
        }
        int randomIndex = Random.Range(0, obstacleSpawnTiles.Count);
        Vector2Int spawnTile = obstacleSpawnTiles[randomIndex];
        obstacleSpawnTiles.RemoveAt(randomIndex); // 스폰된 타일은 리스트에서 제거하여 중복 스폰 방지
        map[spawnTile.x][spawnTile.y].tileState = TileState.Obstacle; // 스폰된 타일을 Obstacle 상태로 변경
        return map[spawnTile.x][spawnTile.y].transform.position;
    }

    void Start()
    {

    }

    /// <summary>
    /// mapTiles을 통해 원하는 모양의 맵 타일을 생성해주는 함수
    /// </summary>
    public void TileCreateByMapData()
    {
        float startX = currentMapData.gridOffset.x;
        float startZ = currentMapData.gridOffset.z;

        map = new List<List<Tile>>();
        for (int columnIndex = 0; columnIndex < currentMapData.height; columnIndex++)
        {
            map.Add(new List<Tile>());
            for (int rowIndex = 0; rowIndex < currentMapData.width; rowIndex++)
            {
                Tile tile = GameObject.Instantiate(prefabTile).transform.GetComponent<Tile>();
                tile.transform.localPosition = transform.position +
                                               new Vector3(startX + (columnIndex) * tilePadding,
                                                        0.01f,
                                                        startZ + (rowIndex) * tilePadding);
                tile.transform.parent = transform;
                tile.SetCoord(columnIndex, rowIndex);
                //tile.transform.localScale = Vector3.one;
                //if (baseMapTiles[columnIndex][rowIndex] == '1')
                //{
                //    tile.tileState = TileState.Full;
                //}
                //else if (baseMapTiles[columnIndex][rowIndex] == 'R')
                //{
                //    tile.tileState = TileState.Full;
                //    rewardCoord = (columnIndex, rowIndex);
                //}
                //else if (baseMapTiles[columnIndex][rowIndex] == 'S')
                //{
                //    playerSpawnCoord = (columnIndex, rowIndex);
                //}
                //else if (baseMapTiles[columnIndex][rowIndex] == 'C')
                //{
                //    centerCoord = (columnIndex, rowIndex);
                //}
                map[columnIndex].Add(tile);
            }
        }

        //맵 크기 저장
        column = map.Count;
        row = map[0].Count;

        Debug.Log($"Column: {column}, Row: {row}");

        //장애물과 몬스터, NPC 스폰 위치 지정
        ApplyVariationLayout();
    }

    public void ApplyVariationLayout()
    {

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
    void UpdateMapData(string stageName)
    {
        //맵 데이터에서 기본 베이스와 추가 바리에이션을 찾아서 등록 진행
        MapDataSO mapData;
        if (AssetCacheManager.instance.TryGetMap(stageName, out mapData))
        {
            Debug.Log($"Find MapData: {mapData.stageName}");
            currentMapData = mapData;
            column = mapData.height;
            row = mapData.width;
            tilePadding = mapData.cellSize;
        }
        else
        {
            Debug.LogWarning($"Failed to Find MapData");
        }
    }

    public void UpdateMapTiles(string stageName, string variationName)
    {
        UpdateMapData(stageName);
        TileCreateByMapData();
    }

    public void UpdateMapVariationFromName(string variationName)
    {
        ApplyVariationLayout();
    }

    public bool mapCreateTest;
    private void Update()
    {
        //if(mapCreateTest)
        //{
        //    mapCreateTest = false;
        //    UpdateMapData("Temple");
        //    TileCreateByMapData();
        //    //TileCreate();
        //}
    }
}
