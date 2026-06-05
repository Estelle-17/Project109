using GameItem.Types;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardRewardPanel : UIPanelBase
{
    [SerializeField] private List<CardUI> cardList;

    public GameObject rootObject;
    public GameObject cardObjectPrefab;
    [SerializeField] private Transform cardSpawnTransform;

    void Start()
    {

    }

    public void SettingCards(string dropTableID, int rewardCardCount)
    {
        GameItemRewardManager.instance.ResetCardLists();

        for (int count = 0; count < rewardCardCount; count++)
        {
            CardUI card = Instantiate(cardObjectPrefab, cardSpawnTransform).GetComponent<CardUI>();

            if (card == null)
                continue;

            CardData cardData = GameItemRewardManager.instance.GetRandomCardDataByDropTable(dropTableID);

            card.UpdateCardData(cardData);
            card.bShowEffectAreaUI = true;

            card.OnCardClick.AddListener(() => GetCard(cardData));
        }
    }


    void GetCard(CardData newCardData)
    {
        if (RunManager.instance != null && RunManager.instance.player != null)
        {
            RunManager.instance.player.deck.AddCard(newCardData);
        }
        UIManager.instance.ReactivateTempDeactiveUIPanel();
        //이 카드 선택지를 제공한 NPC오브젝트 제거 및 캔버스 제거
        Destroy(rootObject);
        Destroy(gameObject);
    }
}
