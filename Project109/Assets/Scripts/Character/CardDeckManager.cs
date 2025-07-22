using UnityEngine;
using System.Collections.Generic;

public class CardDeckManager : UIPanelBase
{
    public List<ActionCardHandler> cardDeck;
    public Transform cardListTransform;

    void Start()
    {
        AddCardDeck();
    }

    void AddCardDeck()
    {
        if (cardListTransform == null)
            return;

        //자식으로 들어가 있는 카드들 추가
        for (int i = 0; i < cardListTransform.childCount; i++)
        {
            cardDeck.Add(cardListTransform.GetChild(i).GetComponent<ActionCardHandler>());
        }

        //이후 카드들한테 클릭 이벤트 등록
        AddCardClickEvent();
    }

    void AddCardClickEvent()
    {
        //카드가 눌리면 카드 데이터를 전달과 동시에 함수 실행
        foreach (ActionCardHandler card in cardDeck)
        {
            ActionCardData cardData = card.cardData;
            card.OnCardClick.AddListener(() => CardCheck(cardData));
        }
    }

    void CardCheck(ActionCardData newData)
    {
        UIManager.instance.cardCheckHandler.OnCardCheckUI(newData);
    }
}
