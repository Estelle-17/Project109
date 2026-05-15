using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField]
    private List<List<Tile>> map;

    public MapDataSO currentMapData;
    public MapDataInfo mapDataInfo;

    private List<CellData> spawnEnemyCells = new List<CellData>();
    private List<CellData> spawnPlayerCells = new List<CellData>();
    private List<CellData> spawnNPCCells = new List<CellData>();
    private List<CellData> spawnObstacleCells = new List<CellData>();
    private List<CellData> spawnTrapCells = new List<CellData>();
    public GameObject prefabTile;
    public Transform spawnTileTransform;
    public int mapColumn;
    public int mapRow;

    //맵 이동 시 제거할 오브젝트 모음
    public List<GameObject> currentSpawnEnemyList;
    public List<GameObject> currentSpawnNPCList;
    public List<GameObject> currentSpawnUIList;
    public List<GameObject> currentSpawnEtcList;

    public List<List<Tile>> GetTileMap() { return map; }

    // 이 함수는 노드에 진입할 때 호출됩니다.
    public void GenerateStage(LocationType mapLocation, BattleData battleData)
    {
        //플레이어를 제외한 생성된 모든 요소 제거
        ClearStage();

        //이전의 맵 타일 및 생성된 오브젝트 제거 후 새롭게 맵 데이터 업데이트 및 타일 생성하도록 코딩 진행
        UpdateMapData(mapLocation.ToString());
        TileCreateByMapData();

        // 맵 오브젝트 생성
        if (currentMapData.mapPrefab != null)
        {
            Instantiate(currentMapData.mapPrefab, Vector3.zero, Quaternion.identity);
        }

        ShuffleList(spawnEnemyCells);
        int spawnEnemyCount = Mathf.Min(spawnEnemyCells.Count, battleData.monsterNames.Count);
        GenerateEnemy(currentMapData, battleData.monsterNames, spawnEnemyCount);

        ShuffleList(spawnTrapCells);
        Debug.Log($"Spawn Trap Cells: {spawnTrapCells.Count}");
        GenerateTrap(currentMapData, spawnTrapCells.Count);

        ShuffleList(spawnObstacleCells);
        Debug.Log($"Spawn Obstacle Cells: {spawnObstacleCells.Count}");
        GenerateObstacle(currentMapData, spawnObstacleCells.Count);

    }

    public void GenerateEnemy(MapDataSO mapData, List<string> spawnMonsterList, int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            CellData cell = spawnEnemyCells[i];

            Debug.Log($"Check {name}...");
            //캐싱된 데이터에서 몬스터 데이터 탐색
            if (AssetCacheManager.instance.TryGetMonster(spawnMonsterList[i], out var newMonsterData))
            {
                Debug.Log($"MonsterData {newMonsterData.name} Load Success.");
                //몬스터 데이터에 맞는 프리팹 탐색
                if (AssetCacheManager.instance.TryGetModel(newMonsterData.objectPath, out var monsterPrefab))
                {
                    Debug.Log("Monsterprefab Load Success.");

                    // 셀의 그리드 좌표를 실제 월드 좌표로 변환 (셀 크기 및 오프셋 적용)
                    Vector3 worldPos = new Vector3(
                        cell.position.x * mapData.cellSize + mapData.gridOffset.x,
                        mapData.gridOffset.y,
                        cell.position.y * mapData.cellSize + mapData.gridOffset.z
                    );

                    // 적 생성
                    GameObject newMonster = Instantiate(monsterPrefab, worldPos, Quaternion.identity);

                    currentSpawnEnemyList.Add(newMonster);
                }
            }
        }
    }

    public void GenerateNPC(MapDataSO mapData, int npcCount)
    {
        // NPC 생성 로직 (적 생성과 유사하게 구현)
        // 예시에서는 NPC 데이터와 프리팹을 탐색하여 생성하는 방식으로 작성
    }

    public void GenerateTrap(MapDataSO mapData, int trapCount)
    {
        // 함정 생성 로직 (적 생성과 유사하게 구현)
        for (int i = 0; i < trapCount; i++)
        {
            CellData cell = spawnTrapCells[i];

            // 함정 이름 탐색 (고정 ID가 있으면 그것을 사용하고, 그렇지 않으면 맵 데이터에서 무작위로 선택)
            string trapName = "";
            if (cell.fixedId != null && cell.fixedId != "")
            {
                trapName = cell.fixedId;
            }
            else
            {
                trapName = mapDataInfo.appearTrapsDataPath[Random.Range(0, mapDataInfo.appearTrapsDataPath.Count)];
            }

            Debug.Log($"Check {name}...");
            //캐싱된 데이터에서 함정 데이터 탐색
            if (AssetCacheManager.instance.TryGetTrap(trapName, out var newTrapData))
            {
                Debug.Log($"TrapData {newTrapData.name} Load Success.");
                //함정 데이터에 맞는 프리팹 탐색
                if (AssetCacheManager.instance.TryGetModel(newTrapData.objectPath, out var trapPrefab))
                {
                    Debug.Log("Trapprefab Load Success.");

                    // 셀의 그리드 좌표를 실제 월드 좌표로 변환 (셀 크기 및 오프셋 적용)
                    Vector3 worldPos = new Vector3(
                        cell.position.x * mapData.cellSize + mapData.gridOffset.x,
                        mapData.gridOffset.y,
                        cell.position.y * mapData.cellSize + mapData.gridOffset.z
                    );

                    // 함정 생성
                    GameObject newTrap = Instantiate(trapPrefab, worldPos, Quaternion.identity);

                    currentSpawnEtcList.Add(newTrap);
                }
            }
            else
            {
                Debug.LogWarning($"Failed to Find TrapData for {trapName}");
            }
        }
    }

    public void GenerateObstacle(MapDataSO mapData, int obstacleCount)
    {
        // 장애물 생성 로직 (적 생성과 유사하게 구현)
        for (int i = 0; i < obstacleCount; i++)
        {
            CellData cell = spawnObstacleCells[i];
            // 장애물 이름 탐색 (고정 ID가 있으면 그것을 사용하고, 그렇지 않으면 맵 데이터에서 무작위로 선택)
            string obstacleName = "";
            if (cell.fixedId != null && cell.fixedId != "")
            {
                obstacleName = cell.fixedId;
            }
            else
            {
                obstacleName = mapDataInfo.appearObstaclesDataPath[Random.Range(0, mapDataInfo.appearObstaclesDataPath.Count)];
            }
            Debug.Log($"Check {name}...");
            //캐싱된 데이터에서 장애물 데이터 탐색
            if (AssetCacheManager.instance.TryGetObstacle(obstacleName, out var newObstacleData))
            {
                Debug.Log($"ObstacleData {newObstacleData.name} Load Success.");
                //장애물 데이터에 맞는 프리팹 탐색
                if (AssetCacheManager.instance.TryGetModel(newObstacleData.objectPath, out var obstaclePrefab))
                {
                    Debug.Log("Obstacleprefab Load Success.");
                    // 셀의 그리드 좌표를 실제 월드 좌표로 변환 (셀 크기 및 오프셋 적용)
                    Vector3 worldPos = new Vector3(
                        cell.position.x * mapData.cellSize + mapData.gridOffset.x,
                        mapData.gridOffset.y,
                        cell.position.y * mapData.cellSize + mapData.gridOffset.z
                    );
                    // 장애물 생성
                    GameObject newObstacle = Instantiate(obstaclePrefab, worldPos, Quaternion.identity);
                    currentSpawnEtcList.Add(newObstacle);
                }
                else
                {
                    Debug.LogWarning($"Failed to Find Obstacle Prefab for {obstacleName}");
                }
            }
            else
            {
                Debug.LogWarning($"Failed to Find ObstacleData for {obstacleName}");
            }
        }
    }

    public void GeneratePlayer(MapDataSO mapData, int playerCount)
    {
        // 플레이어 생성 로직 (적 생성과 유사하게 구현)
        // 예시에서는 플레이어 데이터와 프리팹을 탐색하여 생성하는 방식으로 작성
    }

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
                Tile tile = Instantiate(prefabTile, spawnTileTransform).GetComponent<Tile>();
                tile.transform.localPosition = transform.position +
                                               new Vector3(startX + (columnIndex) * currentMapData.cellSize,
                                                        0.01f,
                                                        startZ + (rowIndex) * currentMapData.cellSize);
                tile.SetCoord(columnIndex, rowIndex);
                //}
                map[columnIndex].Add(tile);
            }
        }

        //맵 크기 저장
        mapColumn = map.Count;
        mapRow = map[0].Count;

        Debug.Log($"MapManager Column: {mapColumn}, Row: {mapRow}");

        //장애물과 몬스터, NPC 스폰 위치 지정
        ApplyVariationLayout();

        SetMapOutsideLine();
    }

    public void ApplyVariationLayout()
    {
        foreach (var cell in currentMapData.cells)
        {
            switch (cell.cellType)
            {
                case CellType.RandomObstacleMarker:
                case CellType.FixedObstacle:
                    map[cell.position.x][cell.position.y].tileState = TileState.Obstacle;
                    spawnObstacleCells.Add(cell);
                    break;
                case CellType.PlayerSpawn:
                    map[cell.position.x][cell.position.y].tileState = TileState.Empty;
                    spawnPlayerCells.Add(cell);
                    break;
                case CellType.EnemySpawn:
                    map[cell.position.x][cell.position.y].tileState = TileState.Empty;
                    spawnEnemyCells.Add(cell);
                    break;
                case CellType.NPCSpawn:
                    map[cell.position.x][cell.position.y].tileState = TileState.Empty;
                    spawnNPCCells.Add(cell);
                    break;
                case CellType.RandomTrapMarker:
                case CellType.FixedTrap:
                    map[cell.position.x][cell.position.y].tileState = TileState.Trap;
                    spawnTrapCells.Add(cell);
                    break;
                case CellType.Wall:
                    map[cell.position.x][cell.position.y].tileState = TileState.Full;
                    break;
                case CellType.Floor:
                    map[cell.position.x][cell.position.y].tileState = TileState.Empty;
                    break;
            }
        }
    }

    //이동할 수 있는 타일들의 외각을 표시해주는 함수
    public void SetMapOutsideLine()
    {
        //현재 외각선 초기화
        for (int columnIndex = 0; columnIndex < mapColumn; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < mapRow; rowIndex++)
            {
                foreach (GameObject obj in map[columnIndex][rowIndex].tileBaseTextureObjects)
                {
                    obj.SetActive(false);
                }
            }
        }

        //이후 장애물과 맵의 끝 부분을 탐색하여 외각선 생성
        for (int columnIndex = 0; columnIndex < mapColumn; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < mapRow; rowIndex++)
            {
                //현재 위치가 비어있을 경우
                if (map[columnIndex][rowIndex].tileState == TileState.Empty || map[columnIndex][rowIndex].tileState == TileState.Trap)
                {
                    //상,하,좌,우 순으로 탐색
                    int[] dirX = { 0, 0, 1, -1 };
                    int[] dirY = { -1, 1, 0, 0 };

                    for (int i = 0; i < 4; i++)
                    {
                        int x = columnIndex + dirX[i];
                        int y = rowIndex + dirY[i];

                        //맵의 범위 내에 있는 경우
                        if (x < mapColumn && x >= 0 && y < mapRow && y >= 0)
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

    void UpdateMapData(string stageName)
    {
        //맵 데이터에서 기본 베이스와 추가 바리에이션을 찾아서 등록 진행
        if (AssetCacheManager.instance.TryGetMap(stageName, out MapDataSO mapData))
        {
            Debug.Log($"Find MapData: {mapData.stageName}");
            currentMapData = mapData;
            mapColumn = mapData.height;
            mapRow = mapData.width;

            if(AssetCacheManager.instance.TryGetMapInfo(stageName, out var mapDataInfo))
            {
                this.mapDataInfo = mapDataInfo;
            }
            else
            {
                Debug.LogWarning($"Failed to Find MapDataInfo");
            }
        }
        else
        {
            Debug.LogWarning($"Failed to Find MapData");
        }
    }

    public void UpdateMapVariationFromName(string variationName)
    {
        ApplyVariationLayout();

        SetMapOutsideLine();
    }

    public bool mapCreateTest;
    private void Update()
    {
        if (mapCreateTest)
        {
            mapCreateTest = false;
            UpdateMapData("Temple");
            TileCreateByMapData();
        }
    }

    public void ClearStage()
    {
        // 현재 씬에 존재하는 적 오브젝트를 모두 제거
        foreach (GameObject enemy in currentSpawnEnemyList)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        currentSpawnEnemyList.Clear();
        // 현재 씬에 존재하는 NPC 오브젝트를 모두 제거
        foreach (GameObject obj in currentSpawnNPCList)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        currentSpawnNPCList.Clear();
        // 현재 씬에 존재하는 UI 오브젝트를 모두 제거
        foreach (GameObject ui in currentSpawnUIList)
        {
            if (ui != null)
            {
                Destroy(ui);
            }
        }
        currentSpawnUIList.Clear();
        // 현재 씬에 존재하는 장애물, 함정 오브젝트를 모두 제거
        foreach (GameObject etc in currentSpawnEtcList)
        {
            if (etc != null)
            {
                Destroy(etc);
            }
        }
        currentSpawnEtcList.Clear();

        //현재 씬에 존재하는 맵 타일 제거
        if (map != null)
        {
            foreach (var column in map)
            {
                foreach (var tile in column)
                {
                    if (tile != null)
                    {
                        Destroy(tile.gameObject);
                    }
                }
            }
            map.Clear();
        }
    }

    // 리스트를 무작위로 섞는 유틸리티 함수
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public List<Tile> CheckPlayerMoveTiles(Tile moveStart, int canMoveDistance)
    {
        List<Tile> checkList = new List<Tile>();

        Queue<Tile> checkNextTiles = new Queue<Tile>();
        Queue<Tile> checkCurrentTiles = new Queue<Tile>();
        checkCurrentTiles.Enqueue(moveStart);

        int column = map.Count;
        int row = map[0].Count;

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

                    //맵을 넘어가는 경우 제외
                    if (x >= column || y >= row || x < 0 || y < 0)
                        continue;
                        

                    //플레이어 위치 혹은 갈 수 없는 경우 제외
                    if (map[x][y].GetCoord().column == moveStart.GetCoord().column && map[x][y].GetCoord().row == moveStart.GetCoord().row ||
                        map[x][y].tileState != TileState.Empty && map[x][y].tileState != TileState.Trap)
                        continue;

                    map[x][y].tileState = TileState.CanMove;
                    map[x][y].ChangeEffect();

                    checkList.Add(map[x][y]);
                    checkNextTiles.Enqueue(map[x][y]);
                }
            }

            checkCurrentTiles = new Queue<Tile>(checkNextTiles);
            //Debug.Log("현재 계산해야 할 타일 갯수 : " + checkCurrentTiles.Count);
            checkNextTiles.Clear();
        }

        return checkList;
    }
}
