using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Script Execution Order를 통해 CardDeckManager를 이 클래스보다 먼저 실행되도록 변경됨
/// </summary>
public class CardDeckViewManager : UIPanelBase
{
    public Transform contentTransform;

    private Dictionary<int, GameObject> activeCardUIs = new Dictionary<int, GameObject>();

    private void OnEnable()
    {
        if (CardDeckManager.instance != null)
        {
            CardDeckManager.instance.RequestAllCardRefresh();
        }
    }

    private void Awake()
    {
        if (CardDeckManager.instance != null)
        {
            CardDeckManager.instance.OnCardAdded += HandleCardAdded;
            CardDeckManager.instance.OnCardRemoved += HandleCardRemoved;
            CardDeckManager.instance.OnCardsRefreshed += RefreshAllCardUIs;
            CardDeckManager.instance.OnCardUpgrade += HandleCardUpgrade;

            gameObject.SetActive(false);

            Debug.Log("Awake is Done!");
        }
        else
        {
            Debug.LogWarning("CardDeckManager.instance is null!");
        }
    }

    private void OnDestroy()
    {
        foreach(GameObject uiObject in activeCardUIs.Values)
        {
            if(ObjectPoolManager.instance != null)
            {
                ObjectPoolManager.instance.ReturnCardUI(uiObject);
            }
            else
            {
                Destroy(uiObject);
            }
        }
        activeCardUIs.Clear();
    }

    private void HandleCardAdded(ActionCardData card)
    {
        if(ObjectPoolManager.instance == null)
        {
            Debug.LogError("ObjectPoolManager is not initialized. Check ObjewctPoolManager In Hierarchy");
            return;
        }

        ActionCardHandler cardUI = ObjectPoolManager.instance.GetCardUI(contentTransform).GetComponent<ActionCardHandler>();
        
        if(cardUI != null && contentTransform != null)
        {
            cardUI.UpdateActionCardData(card);
            AddCardClickEvent(cardUI);

            activeCardUIs.Add(card.runtimeID, cardUI.gameObject);

            Debug.Log("Add card is success!");
        }
        else
        {
            ObjectPoolManager.instance.ReturnCardUI(cardUI.gameObject);
        }

        Debug.Log("HandleCardAdded is end");
    }

    private void HandleCardRemoved(int runtimeID)
    {
        if(activeCardUIs.TryGetValue(runtimeID, out GameObject cardUIObject))
        {
            if (ObjectPoolManager.instance != null)
            {
                ObjectPoolManager.instance.ReturnCardUI(cardUIObject);
            }
            else
            {
                Destroy(cardUIObject);

            }
            activeCardUIs.Remove(runtimeID);      
        }
    }

    private void HandleCardUpgrade(int runtimeID)
    {
        if (activeCardUIs.TryGetValue(runtimeID, out GameObject cardUIObject))
        {
            ActionCardHandler cardHandler = cardUIObject.GetComponent<ActionCardHandler>();
            if(cardHandler != null)
            {
                cardHandler.UpgradeCard();
            }
        }
    }

    private void RefreshAllCardUIs()
    {
        foreach(GameObject cardUIObject in activeCardUIs.Values)
        {
            if(ObjectPoolManager.instance != null)
            {
                ObjectPoolManager.instance.ReturnCardUI(cardUIObject);
            }
            else
            {
                Destroy(cardUIObject);
            }

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
        //이전에 등록했던 클릭 이벤트 제거
        cardHandler.OnCardClick.RemoveAllListeners();

        //카드가 눌리면 카드 데이터를 전달과 동시에 함수 실행
        ActionCardData cardData = cardHandler.GetCardData();
        cardHandler.OnCardClick.AddListener(() => CardCheck(cardData));
    }

    void CardCheck(ActionCardData newData)
    {
        UIManager.instance.cardCheckHandler.OnCardCheckUI(newData);
    }
}
