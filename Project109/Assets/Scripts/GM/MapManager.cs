using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public enum MapState
{
    None,
    Battle,
    Secret
}

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

    public RoutePathfinding routePathfinding;

    public MapDataSO currentMapData;
    public MapState currentMapState;
    public MapDataInfo mapDataInfo;
    public GameMap currentGameMap;

    private List<CellData> spawnEnemyCells = new List<CellData>();
    private List<CellData> spawnPlayerCells = new List<CellData>();
    private List<CellData> spawnNPCCells = new List<CellData>();
    public GameObject mapSpawnRootPrefab;

    //맵 이동 시 제거할 오브젝트 모음
    public List<GameObject> currentSpawnEnemyList;
    public List<GameObject> currentSpawnNPCList;
    public List<GameObject> currentSpawnUIList;
    public List<GameObject> currentSpawnEtcList;

    //NPC 프리팹(모델링은 Addressables에서 탐색 후 생성)
    [SerializeField] private GameObject eventObjectPrefab;
    [SerializeField] private GameObject shopObjectPrefab;
    [SerializeField] private GameObject restoreObjectPrefab;
    [SerializeField] private GameObject insightObjectPrefab;
    [SerializeField] private GameObject rewardMapObjectPrefab;

    private void Start()
    {
        routePathfinding = new RoutePathfinding();
    }

    // 이 함수는 노드에 진입할 때 호출됩니다.
    public void GenerateStage(LocationType mapLocation, IncountType incountType, BattleData battleData = null)
    {
        //플레이어를 제외한 생성된 모든 요소 제거
        ClearStage();

        //이전의 맵 타일 및 생성된 오브젝트 제거 후 새롭게 맵 데이터 업데이트 및 타일 생성하도록 코딩 진행
        UpdateMapData(mapLocation.ToString(), incountType);

        currentGameMap = Instantiate(mapSpawnRootPrefab).GetComponent<GameMap>();

        spawnEnemyCells.Clear();
        spawnPlayerCells.Clear();
        spawnNPCCells.Clear();

        foreach (var cell in currentMapData.cells)
        {
            if (cell.terrainID != "Empty")
            {
                GenerateObjectInMap(cell.terrainID, cell.position);
            }

            if (cell.objectID != "Empty")
            {
                GenerateObjectInMap(cell.objectID, cell.position);
            }

            switch (cell.eventID)
            {
                case "EnemySpawn":
                    spawnEnemyCells.Add(cell);
                    break;
                case "PlayerSpawn":
                    spawnPlayerCells.Add(cell);
                    break;
                case "NPCSpawn":
                    spawnNPCCells.Add(cell);
                    break;
            }
        }

        currentGameMap.TileCreateByMapData(currentMapData);

        if (battleData != null && battleData.monsterNames != null && (incountType == IncountType.Battle || incountType == IncountType.Elite || incountType == IncountType.Boss))
        {
            ShuffleList(spawnEnemyCells);
            int spawnEnemyCount = Mathf.Min(spawnEnemyCells.Count, battleData.monsterNames.Count);
            GenerateEnemy(currentMapData, battleData.monsterNames, spawnEnemyCount);
        }

        SpawnPlayerInMap(currentMapData, spawnPlayerCells.Count);

        if (incountType == IncountType.Store || incountType == IncountType.Restore || incountType == IncountType.Secret || incountType == IncountType.SecretBox)
        {
            EventData eventData = null;
            if (incountType == IncountType.Secret && RunManager.instance.currentIncountNode != null)
            {
                eventData = RunManager.instance.currentIncountNode.eventNodeData;
            }
            GenerateNPC(currentMapData, incountType, eventData);
        }
    }

    private void GenerateObjectInMap(string objectID, Vector2Int pos)
    {
        if (AssetCacheManager.instance.TryGetModel(objectID, out GameObject prefab))
        {
            GameObject spawned = Instantiate(prefab);

            Vector3 worldPos = new Vector3(
                        pos.x * currentMapData.cellSize + currentMapData.gridOffset.x,
                        currentMapData.gridOffset.y,
                        pos.y * currentMapData.cellSize + currentMapData.gridOffset.z
                    );

            spawned.transform.position = worldPos;
            spawned.transform.SetParent(currentGameMap.transform);
        }
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

    public void GenerateNPC(MapDataSO mapData, IncountType incountType, EventData eventData = null)
    {
        if (spawnNPCCells.Count == 0)
        {
            Debug.LogWarning("No NPC Spawn Cells Found in Map Data!");
            return;
        }

        CellData cell = spawnNPCCells[0];
        spawnNPCCells.RemoveAt(0);

        Vector3 worldPos = new Vector3(
            cell.position.x * mapData.cellSize + mapData.gridOffset.x,
            mapData.gridOffset.y,
            cell.position.y * mapData.cellSize + mapData.gridOffset.z
        );

        GameObject npcObj = null;
        GameObject uiObj = null;

        switch (incountType)
        {
            case IncountType.Restore:
                if (restoreObjectPrefab != null)
                {
                    RestoreUIManager newRestoreNPC = Instantiate(restoreObjectPrefab, worldPos, Quaternion.identity).GetComponent<RestoreUIManager>();
                    if (newRestoreNPC != null)
                    {
                        newRestoreNPC.CreateRestoreUI();
                        npcObj = newRestoreNPC.gameObject;
                        uiObj = newRestoreNPC.GetRestoreUI() != null ? newRestoreNPC.GetRestoreUI().gameObject : null;
                    }
                }
                break;
            case IncountType.Store:
                if (shopObjectPrefab != null)
                {
                    ShopUIManager newShopNPC = Instantiate(shopObjectPrefab, worldPos, Quaternion.identity).GetComponent<ShopUIManager>();
                    if (newShopNPC != null)
                    {
                        newShopNPC.AddRandomItems();
                        newShopNPC.UpdateShopItems();
                        npcObj = newShopNPC.gameObject;
                        uiObj = newShopNPC.GetShopUI() != null ? newShopNPC.GetShopUI().gameObject : null;
                    }
                }
                break;
            case IncountType.SecretBox:
                if (rewardMapObjectPrefab != null)
                {
                    ChoiceRewardUIHandler newRewardNPC = Instantiate(rewardMapObjectPrefab, worldPos, Quaternion.identity).GetComponent<ChoiceRewardUIHandler>();
                    if (newRewardNPC != null)
                    {
                        //newRewardNPC.SetReward(RewardItemType.Relic, RandomCardPickupType.Common, RandomRelicPickupType.CommonToUnique, 0);
                        npcObj = newRewardNPC.gameObject;
                        uiObj = newRewardNPC.GetRewardUI();
                    }
                }
                break;
            case IncountType.Secret:
                if (eventObjectPrefab != null)
                {
                    EventHandler newEventNPC = Instantiate(eventObjectPrefab, worldPos, Quaternion.identity).GetComponent<EventHandler>();
                    if (newEventNPC != null)
                    {
                        if (eventData != null)
                        {
                            newEventNPC.SetEventData(eventData);
                            newEventNPC.UpdateEventDescription("START");
                        }
                        npcObj = newEventNPC.gameObject;
                        uiObj = newEventNPC.eventDescription != null ? newEventNPC.eventDescription.gameObject : null;
                    }
                }
                break;
        }

        if (npcObj != null)
        {
            currentSpawnNPCList.Add(npcObj);
        }
        if (uiObj != null)
        {
            currentSpawnUIList.Add(uiObj);
        }
    }

    public void SpawnPlayerInMap(MapDataSO mapData, int spawnCount)
    {
        if (spawnPlayerCells.Count == 0)
        {
            Debug.LogWarning("No Player Spawn Cells Found in Map Data!");
            return;
        }
        ShuffleList(spawnPlayerCells);

        // 셀의 그리드 좌표를 실제 월드 좌표로 변환 (셀 크기 및 오프셋 적용)
        Vector3 worldPos = new Vector3(
            spawnPlayerCells[0].position.x * mapData.cellSize + mapData.gridOffset.x,
            mapData.gridOffset.y,
            spawnPlayerCells[0].position.y * mapData.cellSize + mapData.gridOffset.z
        );
        //플레이어 이동 및 타일 정보 업데이트
        RunManager.instance.player.character.transform.position = worldPos;
        if (RunManager.instance.player.character.characterMove != null)
        {
            RunManager.instance.player.character.characterMove.SetCurrentTile(currentGameMap.GetTileMap()[spawnPlayerCells[0].position.x][spawnPlayerCells[0].position.y]);
        }
    }

    void UpdateMapData(string stageName, IncountType incountType)
    {
        //맵 데이터에서 기본 베이스와 추가 바리에이션을 찾아서 등록 진행
        if (AssetCacheManager.instance.TryGetStageMapData(stageName, out StageMapDataBundle stageMapData))
        {

            // Fallback: 기본적으로 전투 맵을 할당해두어 에셋이 없는 경우에 대비합니다.
            if (stageMapData.battleMapDataList != null && stageMapData.battleMapDataList.Count > 0)
            {
                currentMapData = stageMapData.battleMapDataList[0];
            }

            switch (incountType)
            {
                case IncountType.Battle:
                    if (stageMapData.battleMapDataList != null && stageMapData.battleMapDataList.Count > 0)
                    {
                        currentMapData = stageMapData.battleMapDataList[Random.Range(0, stageMapData.battleMapDataList.Count)];
                        Debug.Log($"ChooseBattleMapData: {currentMapData.stageName}");
                    }
                    break;
                case IncountType.Elite:
                    if (stageMapData.eliteMapDataList != null && stageMapData.eliteMapDataList.Count > 0)
                    {
                        currentMapData = stageMapData.eliteMapDataList[Random.Range(0, stageMapData.eliteMapDataList.Count)];
                        Debug.Log($"ChooseEliteMapData: {currentMapData.stageName}");
                    }
                    break;
                case IncountType.Boss:
                    if (stageMapData.bossMapDataList != null && stageMapData.bossMapDataList.Count > 0)
                    {
                        currentMapData = stageMapData.bossMapDataList[Random.Range(0, stageMapData.bossMapDataList.Count)];
                        Debug.Log($"ChooseBossMapData: {currentMapData.stageName}");
                    }
                    break;
                case IncountType.Secret:
                    if (stageMapData.secretMapDataList != null && stageMapData.secretMapDataList.Count > 0)
                    {
                        currentMapData = stageMapData.secretMapDataList[Random.Range(0, stageMapData.secretMapDataList.Count)];
                        Debug.Log($"ChooseSecretMapData: {currentMapData.stageName}");
                    }
                    break;
                case IncountType.Store:
                    if (stageMapData.storeMapDataList != null && stageMapData.storeMapDataList.Count > 0)
                    {
                        currentMapData = stageMapData.storeMapDataList[Random.Range(0, stageMapData.storeMapDataList.Count)];
                        Debug.Log($"ChooseStoreMapData: {currentMapData.stageName}");
                    }
                    break;
                case IncountType.Restore:
                    if (stageMapData.restoreMapDataList != null && stageMapData.restoreMapDataList.Count > 0)
                    {
                        currentMapData = stageMapData.restoreMapDataList[Random.Range(0, stageMapData.restoreMapDataList.Count)];
                        Debug.Log($"ChooseRestoreMapData: {currentMapData.stageName}");
                    }
                    break;
                case IncountType.SecretBox:
                    if (stageMapData.secretMapDataList != null && stageMapData.secretMapDataList.Count > 0)
                    {
                        currentMapData = stageMapData.secretMapDataList[Random.Range(0, stageMapData.secretMapDataList.Count)];
                        Debug.Log($"ChooseSecretBoxMapData (using Secret): {currentMapData.stageName}");
                    }
                    break;
            }

            if (AssetCacheManager.instance.TryGetMapInfo(stageName, out var mapDataInfo))
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

    public bool mapCreateTest;
    private void Update()
    {
        if (mapCreateTest)
        {
            mapCreateTest = false;
            if (AssetCacheManager.instance.TryGetBattle("Battle_Test_Data", out BattleData battleData))
            {
                currentMapState = MapState.Battle;
                GenerateStage(LocationType.Temple, IncountType.Battle, battleData);
            }
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

        if (currentGameMap != null)
        {
            Destroy(currentGameMap.gameObject);
            currentGameMap = null;
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

        List<List<Tile>> map = currentGameMap.GetTileMap();

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
