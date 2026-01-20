using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardCheckHandler : UIPanelBase
{
    public ActionCardHandler cardHandler;

    [SerializeField] private GameObject detailDescriptionUIPrefab;
    [SerializeField] private Transform detailDescriptionUITransform;

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
    }
}
