using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using GameItem.Types;

public class CardRewardHandler : UIPanelBase
{
    [SerializeField] private List<ActionCardHandler> cardList;

    public GameObject rootObject;
    public GameObject cardObjectPrefab;
    [SerializeField] private Transform cardSpawnTransform;

    void Start()
    {

    }

    public void SettingCards(RandomCardPickupType pickupType, int rewardCardCount)
    {
        GameItemRewardManager.instance.ResetCardLists();

        for (int count = 0; count < rewardCardCount; count++)
        {
            ActionCardHandler card = Instantiate(cardObjectPrefab, cardSpawnTransform).GetComponent<ActionCardHandler>();

            if(card == null)
                continue;

            ActionCardData cardData = GameItemRewardManager.instance.GetRandomCardDataByPickupType(pickupType);

            card.UpdateActionCardData(cardData);

            card.OnCardClick.AddListener(() => GetCard(cardData));
        }
    }


    void GetCard(ActionCardData newCardData)
    {
        CardDeckManager.instance.AddCard(newCardData);

        UIManager.instance.ReactivateTempDeactiveUIPanel();
        //이 카드 선택지를 제공한 NPC오브젝트 제거 및 캔버스 제거
        Destroy(rootObject);
        Destroy(gameObject);
    }
    
}
