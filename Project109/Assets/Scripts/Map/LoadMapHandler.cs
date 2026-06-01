using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameItem.Types;

public class LoadMapHandler : MonoBehaviour
{
    public Image fadeImage;
    public AssetCacheManager dataLoader;



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
        //다음 노드로 이동하였으니 다음 노드들의 가려진 부분들 중 일부가 보이도록 ExploreMap 업데이트
        RunManager.instance.currentExploreUI.OpenExploreMapNodesBasedOnFloorLength();
        //이전에 이동한 노드를 제외한 나머지 노드 가리기
        RunManager.instance.currentExploreUI.CloseBeforeNodes();

        RunManager.instance.currentExploreUI.CloseUI();

        IncountNode newIncountNode = RunManager.instance.currentIncountNode;
        if(newIncountNode != null)
        {
            var mapManager = RunManager.instance.currentMap;
            var playerChar = RunManager.instance.player.character;
            var mapPrefabs = RunManager.instance.MapPrefabs;

            switch (newIncountNode.incountType)
            {
                case IncountType.None:
                    break;
                case IncountType.Battle:
                    mapManager.GenerateStage(LocationType.Temple, IncountType.Battle, playerChar, mapPrefabs, null, newIncountNode.battleNodeData);
                    //GameItemRewardManager.instance.SpawnRewardBox(RunManager.instance.currentMap.CheckTileMapRewardLocation());
                    break;
                case IncountType.Elite:
                    mapManager.GenerateStage(LocationType.Temple, IncountType.Elite, playerChar, mapPrefabs, null, newIncountNode.battleNodeData);
                    //GameItemRewardManager.instance.SpawnRewardBox(RunManager.instance.currentMap.CheckTileMapRewardLocation());
                    break;
                case IncountType.Boss:
                    mapManager.GenerateStage(LocationType.Temple, IncountType.Boss, playerChar, mapPrefabs, null, newIncountNode.battleNodeData);
                    //GameItemRewardManager.instance.SpawnRewardBox(RunManager.instance.currentMap.CheckTileMapRewardLocation());
                    break;
                case IncountType.Restore:
                    mapManager.GenerateStage(LocationType.Temple, IncountType.Restore, playerChar, mapPrefabs);
                    break;
                case IncountType.Store:
                    mapManager.GenerateStage(LocationType.Temple, IncountType.Store, playerChar, mapPrefabs);
                    break;
                case IncountType.SecretBox:
                    mapManager.GenerateStage(LocationType.Temple, IncountType.SecretBox, playerChar, mapPrefabs);
                    break;
                case IncountType.Secret:
                    if(newIncountNode.eventNodeData != null)
                    {
                        mapManager.GenerateStage(LocationType.Temple, IncountType.Secret, playerChar, mapPrefabs, newIncountNode.eventNodeData);
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
                    if (newIncountNode.eventNodeData != null)
                    {
                        mapManager.GenerateNPC(mapManager.currentMapData, IncountType.Secret, mapPrefabs, newIncountNode.eventNodeData);
                    }
                    break;
                case ExtraIncountType.ShineWell:
                    break;
            }

            // 맵 생성이 끝난 후 RunManager에게 상태 전이 알림
            RunManager.instance.OnMapStateChanged(mapManager.currentMapState);
        }

        StartCoroutine(FadeOut(isLoadingNode));
    }

    public void SpawnMonsterInBattleNodeData(BattleData nodeData)
    {
       
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
