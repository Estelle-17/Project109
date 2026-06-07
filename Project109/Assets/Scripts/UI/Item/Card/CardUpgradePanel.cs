using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardUpgradePanel : UIPanelBase
{
    public CardUpgradeDetailView upgradeCardCheckHandler;
    public CardMasteryUpgradeDetailView upgradeMasteryCardCheckHandler;

    public Transform contentTransform;

    private Dictionary<int, GameObject> activeCardUIs = new Dictionary<int, GameObject>();

    private void Awake()
    {
        if (RunManager.instance != null && RunManager.instance.player != null)
        {
            RefreshAllCardUIs();
        }
        else
        {
            Debug.LogWarning("Player is null!");
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject uiObject in activeCardUIs.Values)
        {
            if (ObjectPoolManager.instance != null)
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

    private void HandleCardAdded(Card card)
    {
        if (ObjectPoolManager.instance == null)
        {
            Debug.LogError("ObjectPoolManager is not initialized. Check ObjewctPoolManager In Hierarchy");
            return;
        }

        CardUI cardUI = ObjectPoolManager.instance.GetCardUI(contentTransform).GetComponent<CardUI>();

        if (cardUI != null && contentTransform != null)
        {
            cardUI.UpdateCardInstance(card);
            AddCardClickEvent(cardUI);

            activeCardUIs.Add(card.runtimeID, cardUI.gameObject);
        }
        else
        {
            if (cardUI != null)
            {
                ObjectPoolManager.instance.ReturnCardUI(cardUI.gameObject);
            }
        }
    }

    private void RefreshAllCardUIs()
    {
        foreach (GameObject cardUIObject in activeCardUIs.Values)
        {
            if (ObjectPoolManager.instance != null)
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
                if (card.cardData != null && !card.cardData.isUpgraded)
                {
                    HandleCardAdded(card);
                }
            }
        }

        Debug.Log("현재 가진 카드들 등록 완료");
    }

    void AddCardClickEvent(CardUI cardUI)
    {
        //이전에 등록했던 클릭 이벤트 제거
        cardUI.OnCardClick.RemoveAllListeners();

        //카드가 눌리면 카드 데이터를 전달과 동시에 함수 실행
        Card cardInstance = cardUI.GetCardInstance();
        cardUI.OnCardClick.AddListener(() => CheckUpgradeCard(cardInstance));
    }

    void CheckUpgradeCard(Card card)
    {
        if(card == null || card.cardData == null || upgradeCardCheckHandler == null || upgradeMasteryCardCheckHandler == null)
        {
            Debug.LogWarning("CheckUpgradeCard: card or handlers are null");
            return;
        }

        if(card.cardData.maxMasteryPoint > 0)
        {
            upgradeMasteryCardCheckHandler.OnCardCheckUI(card);
        }
        else
        {
            upgradeCardCheckHandler.OnCardCheckUI(card);
        }
    }
}
