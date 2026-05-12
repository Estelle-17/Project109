using GameItem.Types;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadMapHandler : MonoBehaviour
{
    public Image fadeImage;
    public AssetCacheManager dataLoader;

    //프리팹은 나중에 모딩을 생각해서 addressable로 변경 예정
    [SerializeField] private GameObject eventObjectPrefab;
    [SerializeField] private GameObject shopObjectPrefab;
    [SerializeField] private GameObject restoreObjectPrefab;
    [SerializeField] private GameObject insightObjectPrefab;
    [SerializeField] private GameObject rewardMapObjectPrefab;

    void Start()
    {
        dataLoader = GameObject.Find("AssetData Loader").GetComponent<AssetCacheManager>();
        fadeImage.gameObject.SetActive(false);
    }

    public void StartFadeInOut(bool isLoadingNode)
    {
        fadeImage.gameObject.SetActive(true);

        StartCoroutine(FadeIn(isLoadingNode));
    }

    void LoadCurrentNodeDataInMap(bool isLoadingNode)
    {
        //맵에 남아있는 적들과 NPC, UI들 제거
        DestroyCurrentSpawnEnemy();
        DestroyCurrentSpawnNpc();
        DestroyCurrentSpawnUI();

        //다음 노드로 이동하였으니 다음 노드들의 가려진 부분들 중 일부가 보이도록 ExploreMap 업데이트
        RunManager.instance.currentExploreUI.OpenExploreMapNodesBasedOnFloorLength();
        //이전에 이동한 노드를 제외한 나머지 노드 가리기
        RunManager.instance.currentExploreUI.CloseBeforeNodes();

        RunManager.instance.currentExploreUI.CloseUI();

        IncountNode newIncountNode = RunManager.instance.currentIncountNode;
        if (newIncountNode != null)
        {
            switch (newIncountNode.incountType)
            {
                case IncountType.None:
                    break;
                case IncountType.Battle:
                    RunManager.instance.currentMapState = MapState.Battle;
                    SpawnMonsterInBattleNodeData(newIncountNode.battleNodeData);
                    GameItemRewardManager.instance.SpawnRewardBox(RunManager.instance.currentMap.CheckTileMapRewardLocation());
                    break;
                case IncountType.Elite:
                    RunManager.instance.currentMapState = MapState.Battle;
                    GameItemRewardManager.instance.SpawnRewardBox(RunManager.instance.currentMap.CheckTileMapRewardLocation());
                    break;
                case IncountType.Boss:
                    RunManager.instance.currentMapState = MapState.Battle;
                    GameItemRewardManager.instance.SpawnRewardBox(RunManager.instance.currentMap.CheckTileMapRewardLocation());
                    break;
                case IncountType.Restore:
                    RunManager.instance.currentMapState = MapState.None;
                    SpawnRestoreNPC();
                    break;
                case IncountType.Store:
                    RunManager.instance.currentMapState = MapState.None;
                    SpawnShopNPC();
                    break;
                case IncountType.SecretBox:
                    RunManager.instance.currentMapState = MapState.None;
                    SpawnRewardNPC();
                    break;
                case IncountType.Secret:
                    if (newIncountNode.eventNodeData != null)
                    {
                        RunManager.instance.currentMapState = MapState.Event;
                        SpawnEventNPC(newIncountNode.eventNodeData, true);
                    }
                    break;
                default:
                    break;
            }

            switch (newIncountNode.extraIncountType)
            {
                case ExtraIncountType.None:
                    break;
                case ExtraIncountType.Insight:
                    if (newIncountNode.eventNodeData != null)
                    {
                        SpawnEventNPC(newIncountNode.eventNodeData, false);
                    }
                    break;
                case ExtraIncountType.ShineWell:
                    break;
            }

            //플레이어 위치 이동
            RunManager.instance.player.character.transform.position = RunManager.instance.currentMap.CheckPlayerSpawnLocation();
            CharacterMove charMove = RunManager.instance.player.character.characterMove;
            if (charMove != null)
            {
                (int column, int row) coord = RunManager.instance.currentMap.playerSpawnCoord;
                charMove.SetCurrentTile(RunManager.instance.currentMap.GetTileMap()[coord.column][coord.row]);
            }
        }

        StartCoroutine(FadeOut(isLoadingNode));
    }

    public void SpawnMonsterInBattleNodeData(BattleData nodeData)
    {
        RunManager.instance.currentMap.UpdateMapVariationFromName(nodeData.battleMapVariationName);

        foreach (string name in nodeData.monsterNames)
        {
            Debug.Log($"Check {name}...");
            //캐싱된 데이터에서 몬스터 데이터 탐색
            if (dataLoader.TryGetMonster(name, out var newMonsterData))
            {
                Debug.Log($"MonsterData {newMonsterData.name} Load Success.");
                //몬스터 데이터에 맞는 프리팹 탐색
                if (dataLoader.TryGetModel(newMonsterData.objectPath, out var monsterObject))
                {
                    Debug.Log("Monsterprefab Load Success.");
                    GameObject newMonster = Instantiate(monsterObject);
                    newMonster.transform.position = RunManager.instance.currentMap.CheckEnemySpawnPoint();

                    RunManager.instance.currentSpawnEnemyList.Add(newMonster);
                }
            }
        }
    }

    void SpawnEventNPC(EventData eventData, bool isProcessUpdateVariation)
    {
        if (isProcessUpdateVariation)
        {
            RunManager.instance.currentMap.UpdateMapVariationFromName("NPC");
        }

        EventHandler newEventNPC = Instantiate(eventObjectPrefab).GetComponent<EventHandler>();
        newEventNPC.SetEventData(eventData);    //이벤트 데이터 전달
        newEventNPC.UpdateEventDescription("START");   //선택지 생성. 이벤트의 시작은 특수한 상황을 제외하고 언제나 START로 시작됨
        newEventNPC.transform.position = RunManager.instance.currentMap.CheckNPCSpawnPoint();

        //맵 이동 시 지워질 오브젝트 목록으로 등록
        RunManager.instance.currentSpawnNPCList.Add(newEventNPC.gameObject);
        RunManager.instance.currentSpawnUIList.Add(newEventNPC.eventDescription.gameObject);
    }

    void SpawnShopNPC()
    {
        RunManager.instance.currentMap.UpdateMapVariationFromName("NPC");

        ShopUIManager newShopNPC = Instantiate(shopObjectPrefab).GetComponent<ShopUIManager>();
        if (newShopNPC != null)
        {
            newShopNPC.AddRandomItems();    //상점에 아이템 추가
            newShopNPC.UpdateShopItems();   //아이템UI로 생성
            newShopNPC.transform.position = RunManager.instance.currentMap.CheckNPCSpawnPoint();
        }

        //맵 이동 시 지워질 오브젝트 목록으로 등록
        RunManager.instance.currentSpawnNPCList.Add(newShopNPC.gameObject);
        RunManager.instance.currentSpawnUIList.Add(newShopNPC.GetShopUI().gameObject);
    }

    void SpawnRestoreNPC()
    {
        RunManager.instance.currentMap.UpdateMapVariationFromName("NPC");

        RestoreUIManager newRestoreNPC = Instantiate(restoreObjectPrefab).GetComponent<RestoreUIManager>();
        if (newRestoreNPC != null)
        {
            newRestoreNPC.CreateRestoreUI();
            newRestoreNPC.transform.position = RunManager.instance.currentMap.CheckNPCSpawnPoint();
        }

        //맵 이동 시 지워질 오브젝트 목록으로 등록
        RunManager.instance.currentSpawnNPCList.Add(newRestoreNPC.gameObject);
        RunManager.instance.currentSpawnUIList.Add(newRestoreNPC.GetRestoreUI().gameObject);
    }

    void SpawnRewardNPC()
    {
        RunManager.instance.currentMap.UpdateMapVariationFromName("NPC");

        //유물 선택지 생성
        GameItemRewardManager.instance.InstantiateItemReward(RewardItemType.Relic,
                                                             RandomRelicPickupType.CommonToUnique,
                                                             RunManager.instance.currentMap.CheckNPCSpawnPoint());
    }

    public void DestroyCurrentSpawnEnemy()
    {
        foreach (GameObject obj in RunManager.instance.currentSpawnEnemyList)
        {
            Destroy(obj);
        }
    }
    public void DestroyCurrentSpawnNpc()
    {
        foreach (GameObject obj in RunManager.instance.currentSpawnNPCList)
        {
            Destroy(obj);
        }
    }
    public void DestroyCurrentSpawnUI()
    {
        foreach (GameObject obj in RunManager.instance.currentSpawnUIList)
        {
            Destroy(obj);
        }
    }

    public IEnumerator FadeIn(bool isLoadingNode)
    {
        Color color = fadeImage.color;
        float duration = 0.35f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = color;

            yield return null;
        }

        color.a = 1;
        fadeImage.color = color;

        if (isLoadingNode)
        {
            LoadCurrentNodeDataInMap(isLoadingNode);
        }
    }

    public IEnumerator FadeOut(bool isLoadingNode)
    {
        Color color = fadeImage.color;
        float duration = 0.35f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsed / duration));
            fadeImage.color = color;

            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;

        if (isLoadingNode)
        {

        }

        fadeImage.gameObject.SetActive(false);
    }
}
