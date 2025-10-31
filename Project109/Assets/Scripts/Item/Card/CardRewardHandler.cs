using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum RandomItemPickupType
{
    Common,
    Rare,
    Unique,
    CommonToUnique,
    CommonToRare,
    RareToUnique
}

public class CardRewardHandler : UIPanelBase
{
    [SerializeField] private List<ActionCardHandler> cardList;

    public GameObject rewardNPCObject;
    public GameObject cardObjectPrefab;
    [SerializeField] private Transform cardSpawnTransform;

    RandomItemPicker<ActionCardData> commonCardPicker;
    RandomItemPicker<ActionCardData> rareCardPicker;
    RandomItemPicker<ActionCardData> uniqueCardPicker;

    //등급별 확률
    int commonRate;
    int rareRate;
    int uniqueRate;

    void Start()
    {
        //기본 확률(유물 및 다양한 요소에 의해 변경 가능)
        commonRate = 70;
        rareRate = 25;
        uniqueRate = 5;
    }

    public void SettingCards(RandomItemPickupType pickupType, int rewardCardCount)
    {
        int pickNumber;

        commonCardPicker = new RandomItemPicker<ActionCardData>(GameItemContainer.instance.GetCommonCardList());
        rareCardPicker = new RandomItemPicker<ActionCardData>(GameItemContainer.instance.GetRareCardList());
        uniqueCardPicker = new RandomItemPicker<ActionCardData>(GameItemContainer.instance.GetUniqueCardList());

        for (int count = 0; count < rewardCardCount; count++)
        {
            ActionCardHandler card = Instantiate(cardObjectPrefab, cardSpawnTransform).GetComponent<ActionCardHandler>();

            if(card == null)
                continue;

            //랜덤한 숫자 선택
            pickNumber = Random.Range(1, 101);

            ActionCardData cardData = new ActionCardData();
            if (pickNumber <= uniqueRate)
            {
                //unique카드들 중 랜덤한 1장 선택
                if (uniqueCardPicker.TryGetNext(out ActionCardData data))
                {
                    cardData = data;
                }
                else
                {
                    uniqueCardPicker.Reset();
                    if (uniqueCardPicker.TryGetNext(out ActionCardData newData))
                    {
                        cardData = newData;
                    }
                }

                card.UpdateActionCardData(cardData);
            }
            else if (pickNumber > uniqueRate && pickNumber <= uniqueRate + rareRate)
            {
                //rare카드들 중 랜덤한 1장 선택
                if (rareCardPicker.TryGetNext(out ActionCardData data))
                {
                    cardData = data;
                }
                else
                {
                    rareCardPicker.Reset();
                    if (rareCardPicker.TryGetNext(out ActionCardData newData))
                    {
                        cardData = newData;
                    }
                }
                card.UpdateActionCardData(cardData);
            }
            else
            {
                //common카드들 중 랜덤한 1장 선택
                if (commonCardPicker.TryGetNext(out ActionCardData data))
                {
                    cardData = data;
                }
                else
                {
                    commonCardPicker.Reset();
                    if (commonCardPicker.TryGetNext(out ActionCardData newData))
                    {
                        cardData = newData;
                    }
                }
                card.UpdateActionCardData(cardData);
            }

            card.OnCardClick.AddListener(() => GetCard(cardData));
        }
    }


    void GetCard(ActionCardData newCardData)
    {
        CardDeckManager.instance.AddCard(newCardData);
        //이 카드 선택지를 제공한 NPC오브젝트 제거 및 캔버스 제거
        Destroy(rewardNPCObject);
        Destroy(gameObject);
    }
    
}
