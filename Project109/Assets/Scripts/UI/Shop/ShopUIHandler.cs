using UnityEngine;
using System.Collections.Generic;

public class ShopUIHandler : UIPanelBase
{
    public GameObject cardCollection;
    public GameObject relicCollection;
    public GameObject potionCollection;

    public GameObject cardCollectionTriggers;
    public GameObject relicCollectionTriggers;
    public GameObject potionCollectionTriggers;

    public GameObject cardPrefab;
    public GameObject relicPrefab;
    public GameObject potionPrefab;

    public List<ActionCardHandler> cardList;
    public List<RelicHandler> relicList;
    public List<GameObject> potionList;

    public List<ShopItemTrigger> cardTriggerList;
    public List<ShopItemTrigger> relicTriggerList;
    public List<ShopItemTrigger> potionTriggerList;

    void Start()
    {
        
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

        //상점 아이템과 상호작용이 가능하도록 미리 만들어진 오브젝트를 리스트에 추가
        //for (int i = 0; i < cardCollectionTriggers.transform.childCount; i++)
        //{
        //    cardTriggerList.Add(cardCollectionTriggers.transform.GetChild(i).GetComponent<ShopItemTrigger>());
        //    cardTriggerList[i].ShopItemType = ShopItems.Card;
        //}

        //for (int i = 0; i < relicCollectionTriggers.transform.childCount; i++)
        //{
        //    relicTriggerList.Add(relicCollectionTriggers.transform.GetChild(i).GetComponent<ShopItemTrigger>());
        //    relicTriggerList[i].ShopItemType = ShopItems.Relic;
        //}

        //for (int i = 0; i < potionCollectionTriggers.transform.childCount; i++)
        //{
        //    potionTriggerList.Add(potionCollectionTriggers.transform.GetChild(i).GetComponent<ShopItemTrigger>());
        //    potionTriggerList[i].ShopItemType = ShopItems.Potion;
        //}
    }

    public void UpdateCardList(List<ActionCardData> cardData)
    {
        int cardCount = Mathf.Clamp(cardData.Count, 0, cardList.Count); //상점의 카드 수만큼 데이터를 불러와 등록

        for(int i = 0; i < cardCount; i++)
        {
            ActionCardData currentCardData = cardData[i];
            cardList[i].UpdateActionCardData(currentCardData);
            cardList[i].OnCardClick.AddListener(() => PurchaseCard(currentCardData));

            //cardTriggerList[i].cardHandler = cardList[i];
        }
    }

    public void UpdateRelicList(List<RelicData> relicData)
    {
        int relicCount = Mathf.Clamp(relicData.Count, 0, relicList.Count); //상점의 유물 수만큼 데이터를 불러와 등록

        for (int i = 0; i < relicCount; i++)
        {
            relicList[i].relicData = relicData[i];
            relicList[i].UpdateRelicData();

            //relicTriggerList[i].relicHandler = relicList[i];
        }
    }

    void PurchaseCard(ActionCardData cardData)
    {
        Debug.Log($"{cardData.cardName} 행동 카드를 구매합니다.");
        //덱에 카드 추가하는 로직 구현
    }
}
