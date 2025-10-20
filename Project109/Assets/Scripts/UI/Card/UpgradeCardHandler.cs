using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpgradeCardHandler : UIPanelBase
{
    public UpgradeCardCheckHandler upgradeCardCheckHandler;

    public Transform contentTransform;

    private Dictionary<int, GameObject> activeCardUIs = new Dictionary<int, GameObject>();

    private void Awake()
    {
        if (CardDeckManager.instance != null)
        {
            UIActive();
            RefreshAllCardUIs();
        }
        else
        {
            Debug.LogWarning("CardDeckManager.instance is null!");
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

    private void HandleCardAdded(ActionCardData card)
    {
        if (ObjectPoolManager.instance == null)
        {
            Debug.LogError("ObjectPoolManager is not initialized. Check ObjewctPoolManager In Hierarchy");
            return;
        }

        ActionCardHandler cardUI = ObjectPoolManager.instance.GetCardUI(contentTransform).GetComponent<ActionCardHandler>();

        if (cardUI != null && contentTransform != null)
        {
            cardUI.UpdateActionCardData(card, false);
            AddCardClickEvent(cardUI);

            activeCardUIs.Add(card.runtimeID, cardUI.gameObject);
        }
        else
        {
            ObjectPoolManager.instance.ReturnCardUI(cardUI.gameObject);
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

        if (CardDeckManager.instance != null)
        {
            foreach (ActionCardData card in CardDeckManager.instance.GetCardDeckList())
            {
                HandleCardAdded(card);
            }
        }

        Debug.Log("현재 가진 카드들 등록 완료");
    }

    void AddCardClickEvent(ActionCardHandler cardHandler)
    {
        //이전에 등록했던 클릭 이벤트 제거
        cardHandler.OnCardClick.RemoveAllListeners();

        //카드가 눌리면 카드 데이터를 전달과 동시에 함수 실행
        ActionCardData cardData = cardHandler.GetCardData();
        cardHandler.OnCardClick.AddListener(() => CheckUpgradeCard(cardData));
    }

    void CheckUpgradeCard(ActionCardData newCardData)
    {
        upgradeCardCheckHandler.OnCardCheckUI(newCardData);
    }
}
