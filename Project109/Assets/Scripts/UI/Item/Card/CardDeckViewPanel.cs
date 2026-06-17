using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Script Execution Order를 통해 CardDeckManager를 이 클래스보다 먼저 실행되도록 변경됨
/// </summary>
public class CardDeckViewPanel : UIPanelBase, IOnAddCard, IOnRemoveCard, IOnCardUpgrade, IOnCardsRefreshed
{
    public Transform contentTransform;

    private Dictionary<int, GameObject> activeCardUIs = new Dictionary<int, GameObject>();

    protected override void OnEnable()
    {
        base.OnEnable();
        if (RunManager.instance != null && RunManager.instance.player != null)
        {
            RunManager.instance.player.deck.RequestAllCardRefresh();
        }
    }

    private void Awake()
    {
        if (RunManager.instance != null && RunManager.instance.player != null)
        {
            RunManager.instance.player.eventBus.Add<IOnAddCard>(this);
            RunManager.instance.player.eventBus.Add<IOnRemoveCard>(this);
            RunManager.instance.player.eventBus.Add<IOnCardUpgrade>(this);
            RunManager.instance.player.eventBus.Add<IOnCardsRefreshed>(this);

            gameObject.SetActive(false);

            Debug.Log("Awake is Done!");
        }
        else
        {
            Debug.LogWarning("Player event bus is null!");
        }
    }

    private void OnDestroy()
    {
        if (RunManager.instance != null && RunManager.instance.player != null)
        {
            RunManager.instance.player.eventBus.Remove<IOnAddCard>(this);
            RunManager.instance.player.eventBus.Remove<IOnRemoveCard>(this);
            RunManager.instance.player.eventBus.Remove<IOnCardUpgrade>(this);
            RunManager.instance.player.eventBus.Remove<IOnCardsRefreshed>(this);
        }

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

    public void OnAddCard(Card card)
    {
        HandleCardAdded(card);
    }

    public void OnRemoveCard(Card card)
    {
        HandleCardRemoved(card.runtimeID);
    }

    public void OnCardUpgrade(Card card)
    {
        HandleCardUpgrade(card.runtimeID);
    }

    public void OnCardsRefreshed()
    {
        RefreshAllCardUIs();
    }

    private void HandleCardAdded(Card card)
    {
        if(ObjectPoolManager.instance == null)
        {
            Debug.LogError("ObjectPoolManager is not initialized. Check ObjewctPoolManager In Hierarchy");
            return;
        }

        CardUI cardUI = ObjectPoolManager.instance.GetCardUI(contentTransform).GetComponent<CardUI>();
        
        if(cardUI != null && contentTransform != null)
        {
            cardUI.UpdateCardInstance(card);
            AddCardClickEvent(cardUI);

            activeCardUIs.Add(card.runtimeID, cardUI.gameObject);

            Debug.Log("Add card is success!");
        }
        else
        {
            if (cardUI != null)
            {
                ObjectPoolManager.instance.ReturnCardUI(cardUI.gameObject);
            }
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
            CardUI cardUI = cardUIObject.GetComponent<CardUI>();
            if(cardUI != null)
            {
                cardUI.UpgradeCard();
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

        if (RunManager.instance != null && RunManager.instance.player != null)
        {
            foreach (Card card in RunManager.instance.player.deck.GetCards())
            {
                HandleCardAdded(card);
            }
        }
    }

    void AddCardClickEvent(CardUI cardUI)
    {
        //이전에 등록했던 클릭 이벤트 제거
        cardUI.OnCardClick.RemoveAllListeners();

        //카드가 눌리면 카드 데이터를 전달과 동시에 함수 실행
        Card cardInstance = cardUI.GetCardInstance();
        cardUI.OnCardClick.AddListener(() => CardCheck(cardInstance));
    }

    void CardCheck(Card card)
    {
        if (card != null)
        {
            UIManager.instance.cardCheckHandler.OnCardCheckUI(card);
        }
    }
}
