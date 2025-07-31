using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Script Execution Order를 통해 CardDeckManager를 이 클래스보다 먼저 실행되도록 변경됨
/// </summary>
public class CardDeckViewManager : UIPanelBase
{
    public Transform cardListTransform;

    public GameObject cardUIPrefab;

    private Dictionary<int, GameObject> activeCardUIs = new Dictionary<int, GameObject>();

    //private void OnEnable()
    //{
    //    if(CardDeckManager.instance != null)
    //    {
    //        CardDeckManager.instance.RequestAllCardRefresh();
    //    }
    //}

    private void Awake()
    {
        if (CardDeckManager.instance != null)
        {
            CardDeckManager.instance.OnCardAdded += HandleCardAdded;
            CardDeckManager.instance.OnCardRemoved += HandleCardRemoved;
            CardDeckManager.instance.OnCardsRefreshed += RefreshAllCardUIs;
        }
        else
        {
            Debug.LogWarning("CardDeckManager.instance is null!");
        }

        gameObject.SetActive(false);

        Debug.LogWarning("Awake is Done!");
    }

    private void HandleCardAdded(ActionCardData card)
    {
        ActionCardHandler cardUI = Instantiate(cardUIPrefab, cardListTransform).GetComponent<ActionCardHandler>();
        
        if(cardUI != null && cardListTransform != null)
        {
            cardUI.UpdateActionCardData(card);
            AddCardClickEvent(cardUI);

            activeCardUIs.Add(card.runtimeID, cardUI.gameObject);

            Debug.Log("Add card is success!");
        }

        Debug.Log("HandleCardAdded is end");
    }

    private void HandleCardRemoved(int runtimeID)
    {
        if(activeCardUIs.TryGetValue(runtimeID, out GameObject cardUIObject))
        {
            Destroy(cardUIObject);
            activeCardUIs.Remove(runtimeID);
        }
    }

    private void RefreshAllCardUIs()
    {
        foreach(GameObject cardUIObject in activeCardUIs.Values)
        {
            Destroy(cardUIObject);
        }
        activeCardUIs.Clear();

        if(CardDeckManager.instance != null)
        {
            foreach (ActionCardData card in CardDeckManager.instance.GetCardDeckList())
            {
                HandleCardAdded(card);
            }
        }
    }

    void AddCardClickEvent(ActionCardHandler cardHandler)
    {
        //카드가 눌리면 카드 데이터를 전달과 동시에 함수 실행
        ActionCardData cardData = cardHandler.GetCardData();
        cardHandler.OnCardClick.AddListener(() => CardCheck(cardData));
    }

    void CardCheck(ActionCardData newData)
    {
        UIManager.instance.cardCheckHandler.OnCardCheckUI(newData);
    }
}
