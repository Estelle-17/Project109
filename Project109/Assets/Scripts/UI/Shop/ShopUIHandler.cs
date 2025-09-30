using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopUIHandler : UIPanelBase
{
    public GameObject cardCollection;
    public GameObject relicCollection;
    public GameObject potionCollection;

    public GameObject cardPrefab;
    public GameObject relicPrefab;
    public GameObject potionPrefab;
    public GameObject eraseCardUIPrefab;

    public List<ActionCardHandler> cardList;
    public List<RelicHandler> relicList;
    public List<GameObject> potionList;

    public Button eraseCardButton;

    void Start()
    {
        eraseCardButton.onClick.AddListener(OpenEraseCardUI);   //버튼 등록
    }

    /// <summary>
    /// 상점에 생성될 카드, 유물, 포션의 갯수를 정해진 수만큼 빈 칸을 생성시킴.
    /// 기본적으로 카드는 최대 6개, 유물은 3개, 포션은 3개임
    /// </summary>
    public void CreateStoreItemCollections(int cardNumber, int relicNumber, int potionNumber)
    {
        //갯수에 맞는 아이템 배경 생성
        cardNumber = Mathf.Clamp(cardNumber, 0, 6);
        for(int i = 0; i < cardNumber; i++)
        {
            cardList.Add(GameObject.Instantiate(cardPrefab, cardCollection.transform).GetComponent<ActionCardHandler>());
        }

        relicNumber = Mathf.Clamp(relicNumber, 0, 3);
        for (int i = 0; i < relicNumber; i++)
        {
            relicList.Add(GameObject.Instantiate(relicPrefab, relicCollection.transform).GetComponent<RelicHandler>());
        }

        potionNumber = Mathf.Clamp(potionNumber, 0, 3);
        for (int i = 0; i < potionNumber; i++)
        {
            potionList.Add(GameObject.Instantiate(potionPrefab, potionCollection.transform));
        }
    }

    public void UpdateCardList(List<ActionCardData> cardData)
    {
        int cardCount = Mathf.Clamp(cardData.Count, 0, cardList.Count); //상점의 카드 수만큼 데이터를 불러와 등록

        for(int i = 0; i < cardCount; i++)
        {
            ActionCardData currentCardData = cardData[i];
            cardList[i].UpdateActionCardData(currentCardData);
            cardList[i].OnCardClick.AddListener(() => PurchaseCard(currentCardData));
        }
    }

    public void UpdateRelicList(List<RelicData> relicData)
    {
        int relicCount = Mathf.Clamp(relicData.Count, 0, relicList.Count); //상점의 유물 수만큼 데이터를 불러와 등록

        for (int i = 0; i < relicCount; i++)
        {
            RelicData currentRelicData = relicData[i];
            relicList[i].UpdateRelicData(currentRelicData);
            relicList[i].OnRelicClick.AddListener(() => PurchaseRelic(currentRelicData));
        }
    }

    void PurchaseCard(ActionCardData cardData)
    {
        Debug.Log($"{cardData.cardName} 행동 카드를 구매합니다.");

        //덱에 카드 추가하는 로직 구현
        CardDeckManager.instance.AddCard(cardData);
    }

    void PurchaseRelic(RelicData relicData)
    {
        Debug.Log($"{relicData.relicName} 유물을 구매합니다.");

        RelicManager.instance.AddRelic(relicData);
    }

    public void OpenEraseCardUI()
    {
        EraseCardDeckManager eraseCardDeckManager = Instantiate(eraseCardUIPrefab).GetComponent<EraseCardDeckManager>();
        eraseCardDeckManager.SetEraseCardCount(1);    //카드를 지우는 갯수 입력
    }
}
