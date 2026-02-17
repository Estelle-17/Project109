using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardCheckHandler : UIPanelBase
{
    public ActionCardHandler cardHandler;

    [SerializeField] private GameObject detailDescriptionUIPrefab;
    [SerializeField] private Transform detailDescriptionUITransform;

    [SerializeField] private ExtraDescriptionManager extraDescriptionManager;

    void Start()
    {
        
    }

    //클릭한 카드 데이터를 확인하고 화면 상에 보여줌
    public void OnCardCheckUI(ActionCardData newCardData)
    {
        if (cardHandler == null)
            return;

        cardHandler.UpdateActionCardData(newCardData);
        cardHandler.bIsCardHighlight = false;
        cardHandler.bAlwaysShowExtraDescription = true;

        UIActive();

        //상세 설명 UI 생성
        if (CardMasteryManager.instance == null)
        {
            return;
        }

        //이전에 있던 상세 설명 UI 삭제
        foreach (Transform child in detailDescriptionUITransform)
        {
            Destroy(child.gameObject);
        }

        //카드 상세 설명 업데이트
        UpdateCardExtraDescription(newCardData, cardHandler.extraDescriptionSpawnPos.transform, cardHandler.transform.localScale);

        //카드 마스터리 업그레이드 정보 가져오기
        Dictionary<string, int> masteryUpgrades = CardMasteryManager.instance.GetCardMasteryUpgrades(newCardData.ID);

        if(masteryUpgrades != null)
        {
            if (AssetCacheManager.instance.TryGetCardDescription(newCardData.path, out CardDescription description))
            {
                foreach (MasteryDescription mastery in description.masteryDescriptions)
                {
                    if (masteryUpgrades.ContainsKey(mastery.path))
                    {
                        GameObject detailDescriptionUIObj = Instantiate(detailDescriptionUIPrefab, detailDescriptionUITransform);
                        MasteryUpgradeCheckDescription detailDescriptionUI = detailDescriptionUIObj.GetComponent<MasteryUpgradeCheckDescription>();
                        detailDescriptionUI.SetDescriptionText($"{mastery.name} X {masteryUpgrades[mastery.path]}");
                    }
                }
            }
        }

        //레이아웃 갱신
        LayoutRebuilder.ForceRebuildLayoutImmediate(detailDescriptionUITransform.GetComponent<RectTransform>());

        //카드 효과 범위 업데이트
        if(UIManager.instance != null)
        {
            UIManager.instance.ClearEffectAreaTiles();

            //효과 범위 설정
            UIManager.instance.SetEffectAreaFromTargetDistance(newCardData.targetMinDistance,
                                                               newCardData.targetMaxDistance,
                                                               TileType.TargetTile);

            //추가 효과 범위 설정
            foreach (EffectArea additionalEffectArea in newCardData.additionalEffectAreaList)
            {
                UIManager.instance.SetEffectAreaFromShapeGenerator(additionalEffectArea.areaType,
                                                                   additionalEffectArea.distance,
                                                                   TileType.AdditionalEffectTile);
            }
        }
    }

    public void UpdateCardExtraDescription(ActionCardData cardData, Transform spawnPos, Vector3 newLocalScale)
    {
        extraDescriptionManager.transform.localScale = newLocalScale;
        extraDescriptionManager.transform.position = spawnPos.position;

        if (cardData.maxMasteryPoint > 0)
        {
            Debug.Log("Set Mastery Point Description");
            extraDescriptionManager.SetMasteryPointDescription(cardData, cardData.maxMasteryPoint);
        }
        else
        {
            extraDescriptionManager.ClearMasteryPointDescription();
        }

        if (AssetCacheManager.instance.TryGetCardDescription(cardData.path, out CardDescription description))
        {
            if (extraDescriptionManager)
            {
                extraDescriptionManager.SetExtraDescription(description.extraDescriptions);
            }
        }
        else
        {
            if (extraDescriptionManager)
            {
                extraDescriptionManager.ClearExtraDescription();
            }
            Debug.LogWarning("Card description not found for path: " + cardData.path);
        }
    }
}
