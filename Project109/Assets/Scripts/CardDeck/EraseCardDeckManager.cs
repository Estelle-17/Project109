using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class EraseCardDeckManager : UIPanelBase
{
    public Transform contentTransform;

    private int eraseCardCount;
    public List<int> eraseCardIDs;

    public Button eraseCardButton;

    private Dictionary<int, GameObject> activeCardUIs = new Dictionary<int, GameObject>();

    private void Awake()
    {
        if (CardDeckManager.instance != null)
        {
            UIActive();
            RefreshAllCardUIs();
            eraseCardButton.onClick.AddListener(StartEraseCards);
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
    }

    public void SetEraseCardCount(int count)
    {
        eraseCardCount = count;
    }

    void AddCardClickEvent(ActionCardHandler cardHandler)
    {
        //카드가 눌리면 카드 데이터를 전달과 동시에 함수 실행
        ActionCardData cardData = cardHandler.GetCardData();
        cardHandler.OnCardClick.AddListener(() => AddEraseCard(cardHandler));
    }

    void AddEraseCard(ActionCardHandler newCardHandler)
    {
        //이미 선택된 카드가 한번 더 선택되었을 경우 지울 카드 리스트에서 제거
        if (eraseCardIDs.Contains(newCardHandler.GetCardData().runtimeID))
        {
            eraseCardIDs.Remove(newCardHandler.GetCardData().runtimeID);
            newCardHandler.bIsCardHighlight = true;
            newCardHandler.OffSelectHighlight(); //선택이 해제되었음을 알리기 위한 하이라이트 비활성화
        }
        else if(eraseCardIDs.Count < eraseCardCount)
        {
            eraseCardIDs.Add(newCardHandler.GetCardData().runtimeID);   //지울 카드를 리스트에 저장
            newCardHandler.bIsCardHighlight = false;
            newCardHandler.OnSelectHighlight(); //선택됬음을 알리기 위한 하이라이트 활성화
        }
    }

    public void StartEraseCards()
    {
        if(eraseCardIDs.Count == eraseCardCount) 
        {
            Debug.Log($"선택된 카드를 제거합니다.");
            foreach (int cardID in eraseCardIDs)
            {
                CardDeckManager.instance.RemoveCard(cardID);
            }

            //카드 제거 후 UI 제거
            UIDeactive();
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"{eraseCardCount}만큼 카드를 선택해야 합니다.");
        }
    }
}
