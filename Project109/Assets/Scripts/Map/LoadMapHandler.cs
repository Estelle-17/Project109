using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadMapHandler : MonoBehaviour
{
    public Image fadeImage;
    public AddressableDataLoader dataLoader;

    //프리팹은 나중에 모딩을 생각해서 addressable로 변경 예정
    public GameObject eventObjectPrefab;
    public GameObject shopObjectPrefab;
    public GameObject restoreObjectPrefab;

    void Start()
    {
        dataLoader = GameObject.Find("AddressablesData Loader").GetComponent<AddressableDataLoader>();
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
        foreach(GameObject obj in GameManager.instance.currentSpawnEnemyOrNPCList)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in GameManager.instance.currentSpawnUIList)
        {
            Destroy(obj);
        }

        //다음 노드로 이동하였으니 다음 노드들의 가려진 부분들 중 일부가 보이도록 ExploreMap 업데이트
        GameManager.instance.currentExploreUI.OpenExploreMapNodesBasedOnFloorLength();
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
        }

        StartCoroutine(FadeOut(isLoadingNode));
    }

    void SpawnMonsterInBattleNodeData(BattleNodeData nodeData)
    {
        foreach(MonsterSpawnInfo info in nodeData.monsterAppearInformation)
        {
            if(dataLoader.TryGetMonster(info.monsterName, out var newMonsterData))
            {
                GameObject newMonster = Instantiate(newMonsterData.monsterPrefab);
                newMonster.transform.position = GameManager.instance.currentMap.CheckTileMapLocationByRowAndColumn(info.spawnPointX, info.spawnPointY);

                GameManager.instance.currentSpawnEnemyOrNPCList.Add(newMonster);
            }
        }
    }

    void SpawnEventNPC(EventData eventData)
    {
        EventHandler newEventNPC = Instantiate(eventObjectPrefab).GetComponent<EventHandler>();
        newEventNPC.SetEventData(eventData);    //이벤트 데이터 전달
        newEventNPC.UpdateEventDescription();   //선택지 생성
        newEventNPC.transform.position = GameManager.instance.currentMap.CheckTileMapCenterLocation();

        //맵 이동 시 지워질 오브젝트 목록으로 등록
        GameManager.instance.currentSpawnEnemyOrNPCList.Add(newEventNPC.gameObject);
        GameManager.instance.currentSpawnUIList.Add(newEventNPC.eventDescription.gameObject);
    }

    void SpawnShopNPC()
    {
        ShopUIManager newShopNPC = Instantiate(shopObjectPrefab).GetComponent<ShopUIManager>();
        if (newShopNPC != null)
        {
            newShopNPC.AddRandomItems();    //상점에 아이템 추가
            newShopNPC.UpdateShopItems();   //아이템UI로 생성
            newShopNPC.transform.position = GameManager.instance.currentMap.CheckTileMapCenterLocation();
        }

        //맵 이동 시 지워질 오브젝트 목록으로 등록
        GameManager.instance.currentSpawnEnemyOrNPCList.Add(newShopNPC.gameObject);
        GameManager.instance.currentSpawnUIList.Add(newShopNPC.GetShopUI().gameObject);
    }

    void SpawnRestoreNPC()
    {
        RestoreUIManager newRestoreNPC = Instantiate(restoreObjectPrefab).GetComponent<RestoreUIManager>();
        if(newRestoreNPC != null)
        {
            newRestoreNPC.CreateRestoreUI();
            newRestoreNPC.transform.position = GameManager.instance.currentMap.CheckTileMapCenterLocation();
        }

        //맵 이동 시 지워질 오브젝트 목록으로 등록
        GameManager.instance.currentSpawnEnemyOrNPCList.Add(newRestoreNPC.gameObject);
        GameManager.instance.currentSpawnUIList.Add(newRestoreNPC.GetRestoreUI().gameObject);
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
