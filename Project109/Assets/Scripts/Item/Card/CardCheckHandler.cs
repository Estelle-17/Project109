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


    //클릭한 카드 데이터를 확인하고 화면 상에 보여줌 (CardData 오버로드)
    public void OnCardCheckUI(CardData newCardData)
    {
        if (cardHandler == null)
            return;

        cardHandler.UpdateCardData(newCardData);
        cardHandler.bIsCardHighlight = false;
        cardHandler.bAlwaysShowExtraDescription = true;

        UIActive();

        //이전에 있던 상세 설명 UI 삭제
        foreach (Transform child in detailDescriptionUITransform)
        {
            Destroy(child.gameObject);
        }

        //카드 상세 설명 업데이트
        UpdateCardExtraDescription(newCardData, cardHandler.extraDescriptionSpawnPos.transform, cardHandler.transform.localScale);

        //레이아웃 갱신
        LayoutRebuilder.ForceRebuildLayoutImmediate(detailDescriptionUITransform.GetComponent<RectTransform>());

        //카드 효과 범위 업데이트
        if(UIManager.instance != null)
        {
            UIManager.instance.UpdateEffectAreaUI(newCardData);
        }
    }

    //클릭한 카드 데이터를 확인하고 화면 상에 보여줌 (CardBase 오버로드)
    public void OnCardCheckUI(CardBase card)
    {
        if (card == null || cardHandler == null)
            return;

        cardHandler.UpdateCardInstance(card);
        cardHandler.bIsCardHighlight = false;
        cardHandler.bAlwaysShowExtraDescription = true;

        UIActive();

        //이전에 있던 상세 설명 UI 삭제
        foreach (Transform child in detailDescriptionUITransform)
        {
            Destroy(child.gameObject);
        }

        //카드 상세 설명 업데이트
        UpdateCardExtraDescription(card, cardHandler.extraDescriptionSpawnPos.transform, cardHandler.transform.localScale);

        //카드 마스터리 업그레이드 정보 가져오기 (인스턴스로부터 직접 조회)
        Dictionary<string, int> masteryUpgrades = card.activeMasteryUpgrades;

        if(masteryUpgrades != null && card.cardData != null)
        {
            if (AssetCacheManager.instance.TryGetCardDescription(card.cardData.cardName, out CardDescription description))
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
            UIManager.instance.UpdateEffectAreaUI(card);
        }
    }


    public void UpdateCardExtraDescription(CardData cardData, Transform spawnPos, Vector3 newLocalScale)
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

        if (AssetCacheManager.instance.TryGetCardDescription(cardData.cardName, out CardDescription description))
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
            Debug.LogWarning("Card description not found for cardName: " + cardData.cardName);
        }
    }

    public void UpdateCardExtraDescription(CardBase card, Transform spawnPos, Vector3 newLocalScale)
    {
        if (card == null || card.cardData == null) return;
        extraDescriptionManager.transform.localScale = newLocalScale;
        extraDescriptionManager.transform.position = spawnPos.position;

        if (card.cardData.maxMasteryPoint > 0)
        {
            Debug.Log("Set Mastery Point Description");
            extraDescriptionManager.SetMasteryPointDescription(card, card.cardData.maxMasteryPoint);
        }
        else
        {
            extraDescriptionManager.ClearMasteryPointDescription();
        }

        if (AssetCacheManager.instance.TryGetCardDescription(card.cardData.cardName, out CardDescription description))
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
            Debug.LogWarning("Card description not found for cardName: " + card.cardData.cardName);
        }
    }
}
