using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadMapHandler : MonoBehaviour
{
    public Image fadeImage;
    public AssetCacheManager dataLoader;

    //프리팹은 나중에 모딩을 생각해서 addressable로 변경 예정
    public GameObject eventObjectPrefab;
    public GameObject shopObjectPrefab;
    public GameObject restoreObjectPrefab;
    public GameObject insightObjectPrefab;

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
        GameManager.instance.currentExploreUI.OpenExploreMapNodesBasedOnFloorLength();
        //이전에 이동한 노드를 제외한 나머지 노드 가리기
        GameManager.instance.currentExploreUI.CloseBeforeNodes();

        GameManager.instance.currentExploreUI.CloseUI();

        IncountNode newIncountNode = GameManager.instance.currentIncountNode;
        if(newIncountNode != null)
        {
            switch (newIncountNode.incountType)
            {
                case IncountType.None:
                    break;
                case IncountType.Battle:
                    SpawnMonsterInBattleNodeData(newIncountNode.battleNodeData);
                    break;
                case IncountType.Elite:

                    break;
                case IncountType.Boss:

                    break;
                case IncountType.Restore:
                    SpawnRestoreNPC();
                    break;
                case IncountType.Store:
                    SpawnShopNPC();
                    break;
                case IncountType.SecretBox:
                    SpawnRewardNPC();
                    break;
                case IncountType.Secret:
                    if(newIncountNode.eventNodeData != null)
                    {
                        SpawnEventNPC(newIncountNode.eventNodeData);
                    }
                    break;
                default:
                    break;
            }

            switch(newIncountNode.extraIncountType)
            {
                case ExtraIncountType.None:
                    break;
                case ExtraIncountType.Insight:
                    break;
                case ExtraIncountType.ShineWell:
                    break;
            }
        }

        StartCoroutine(FadeOut(isLoadingNode));
    }

    public void SpawnMonsterInBattleNodeData(BattleData nodeData)
    {
        GameManager.instance.currentMap.UpdateMapVariationFromName(nodeData.battleMapVariationName);

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
                    newMonster.transform.position = GameManager.instance.currentMap.CheckEnemySpawnPoint();

                    GameManager.instance.currentSpawnEnemyList.Add(newMonster);
                }
            }
        }
    }

    void SpawnEventNPC(EventData eventData)
    {
        GameManager.instance.currentMap.UpdateMapVariationFromName("NPC");

        EventHandler newEventNPC = Instantiate(eventObjectPrefab).GetComponent<EventHandler>();
        newEventNPC.SetEventData(eventData);    //이벤트 데이터 전달
        newEventNPC.UpdateEventDescription("START");   //선택지 생성. 이벤트의 시작은 특수한 상황을 제외하고 언제나 START로 시작됨
        newEventNPC.transform.position = GameManager.instance.currentMap.CheckNPCSpawnPoint();

        //맵 이동 시 지워질 오브젝트 목록으로 등록
        GameManager.instance.currentSpawnNPCList.Add(newEventNPC.gameObject);
        GameManager.instance.currentSpawnUIList.Add(newEventNPC.eventDescription.gameObject);
    }

    void SpawnShopNPC()
    {
        GameManager.instance.currentMap.UpdateMapVariationFromName("NPC");

        ShopUIManager newShopNPC = Instantiate(shopObjectPrefab).GetComponent<ShopUIManager>();
        if (newShopNPC != null)
        {
            newShopNPC.AddRandomItems();    //상점에 아이템 추가
            newShopNPC.UpdateShopItems();   //아이템UI로 생성
            newShopNPC.transform.position = GameManager.instance.currentMap.CheckNPCSpawnPoint();
        }

        //맵 이동 시 지워질 오브젝트 목록으로 등록
        GameManager.instance.currentSpawnNPCList.Add(newShopNPC.gameObject);
        GameManager.instance.currentSpawnUIList.Add(newShopNPC.GetShopUI().gameObject);
    }

    void SpawnRestoreNPC()
    {
        GameManager.instance.currentMap.UpdateMapVariationFromName("NPC");

        RestoreUIManager newRestoreNPC = Instantiate(restoreObjectPrefab).GetComponent<RestoreUIManager>();
        if(newRestoreNPC != null)
        {
            newRestoreNPC.CreateRestoreUI();
            newRestoreNPC.transform.position = GameManager.instance.currentMap.CheckNPCSpawnPoint();
        }

        //맵 이동 시 지워질 오브젝트 목록으로 등록
        GameManager.instance.currentSpawnNPCList.Add(newRestoreNPC.gameObject);
        GameManager.instance.currentSpawnUIList.Add(newRestoreNPC.GetRestoreUI().gameObject);
    }

    void SpawnRewardNPC()
    {
        GameManager.instance.currentMap.UpdateMapVariationFromName("NPC");

        //유물 선택지 생성
        GameItemRewardManager.instance.InstantiateRelicReward(RandomItemPickupType.CommonToUnique, 
                                                          GameManager.instance.currentMap.CheckNPCSpawnPoint());
    }

    public void DestroyCurrentSpawnEnemy()
    {
        foreach (GameObject obj in GameManager.instance.currentSpawnEnemyList)
        {
            Destroy(obj);
        }
    }
    public void DestroyCurrentSpawnNpc()
    {
        foreach (GameObject obj in GameManager.instance.currentSpawnNPCList)
        {
            Destroy(obj);
        }
    }
    public void DestroyCurrentSpawnUI()
    {
        foreach (GameObject obj in GameManager.instance.currentSpawnUIList)
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

        if(isLoadingNode)
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
