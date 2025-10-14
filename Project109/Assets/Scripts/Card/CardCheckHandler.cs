using Unity.VisualScripting;
using UnityEngine;

public class CardCheckHandler : UIPanelBase
{
    public ActionCardHandler cardHandler;

    void Start()
    {
        
    }

    //클릭한 카드 데이터를 확인하고 화면 상에 보여줌
    public void OnCardCheckUI(ActionCardData newCardData)
    {
        if (cardHandler == null)
            return;

        cardHandler.UpdateActionCardData(newCardData);
        cardHandler.bIsCardHighlight = false;

        UIActive();
    }
}
