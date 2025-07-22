using UnityEngine;
using System.Collections.Generic;

public class UpgradeUIHandler : UIPanelBase
{ 
    public List<ActionCardHandler> upgradeCardList;
    public GameObject viewLayout;

    void Start()
    {
        
    }

    void AddCardEvent()
    {
        foreach (ActionCardHandler card in upgradeCardList)
        {
            card.OnCardClick.AddListener(() => UpgradeCard(card.cardData));
        }
    }

    void UpgradeCard(ActionCardData cardData)
    {
        Debug.Log($"{cardData.cardName} 행동 카드의 업그레이드 여부를 확인합니다.");
        //카드 업그레이드 확인 UI 활성화 및 클릭된 카드 데이터로 변경
    }
}