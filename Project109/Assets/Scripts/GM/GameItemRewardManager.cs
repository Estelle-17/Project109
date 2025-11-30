using UnityEngine;
using System.Collections.Generic;

public class GameItemRewardManager : MonoBehaviour
{
    public static GameItemRewardManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject rewardObjectPrefab;

    //Cards
    private List<ActionCardData> commonCardList = new List<ActionCardData>();
    private List<ActionCardData> rareCardList = new List<ActionCardData>();
    private List<ActionCardData> uniqueCardList = new List<ActionCardData>();

    RandomItemPicker<ActionCardData> commonCardPicker;
    RandomItemPicker<ActionCardData> rareCardPicker;
    RandomItemPicker<ActionCardData> uniqueCardPicker;

    //Relics
    private List<RelicData> commonRelicList = new List<RelicData>();
    private List<RelicData> rareRelicList = new List<RelicData>();
    private List<RelicData> uniqueRelicList = new List<RelicData>();

    RandomItemPicker<RelicData> commonRelicPicker;
    RandomItemPicker<RelicData> rareRelicPicker;
    RandomItemPicker<RelicData> uniqueRelicPicker;

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

    public void UpdateItemList()
    {
        foreach(ActionCardData data in AssetCacheManager.instance.cardList)
        {
            switch (data.level) //1~3
            {
                case 1:
                    commonCardList.Add(data);
                    break;
                case 2:
                    rareCardList.Add(data);
                    break;
                case 3:
                    uniqueCardList.Add(data);
                    break;
            }
        }

        foreach (RelicData data in AssetCacheManager.instance.relicList)
        {
            switch (data.level) //1~3
            {
                case 1:
                    commonRelicList.Add(data);
                    break;
                case 2:
                    rareRelicList.Add(data);
                    break;
                case 3:
                    uniqueRelicList.Add(data);
                    break;
            }
        }

        //Card Picker 초기화
        commonCardPicker = new RandomItemPicker<ActionCardData>(commonCardList);
        rareCardPicker = new RandomItemPicker<ActionCardData>(rareCardList);
        uniqueCardPicker = new RandomItemPicker<ActionCardData>(uniqueCardList);

        //Relic Picker 초기화
        commonRelicPicker = new RandomItemPicker<RelicData>(commonRelicList);
        rareRelicPicker = new RandomItemPicker<RelicData>(rareRelicList);
        uniqueRelicPicker = new RandomItemPicker<RelicData>(uniqueRelicList);
}

    public ActionCardData GetRandomCardDataByPickupType(RandomItemPickupType pickupType)
    {
        //랜덤한 숫자 선택
        int pickNumber = Random.Range(1, 101);

        ActionCardData cardData = ScriptableObject.CreateInstance<ActionCardData>();
        if (pickNumber <= uniqueRate)
        {
            uniqueCardPicker.Reset();

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
        }
        else if (pickNumber > uniqueRate && pickNumber <= uniqueRate + rareRate)
        {
            rareCardPicker.Reset();

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
        }
        else
        {
            commonCardPicker.Reset();

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
        }

        return cardData;
    }

    public RelicData GetRandomRelicDataByPickupType(RandomItemPickupType pickupType)
    {
        //랜덤한 유물 선택 후 등록
        int pickNumber = Random.Range(1, 101);

        RelicData relicData = ScriptableObject.CreateInstance<RelicData>();
        if (pickNumber <= uniqueRate)
        {
            uniqueRelicPicker.Reset();

            //unique유물들 중 랜덤한 1장 선택
            if (uniqueRelicPicker.TryGetNext(out RelicData data))
            {
                relicData = data;
            }
            else
            {
                uniqueRelicPicker.Reset();
                if (uniqueRelicPicker.TryGetNext(out RelicData newData))
                {
                    relicData = newData;
                }
            }
        }
        else if (pickNumber > uniqueRate && pickNumber <= uniqueRate + rareRate)
        {
            rareRelicPicker.Reset();

            //rare유물들 중 랜덤한 1장 선택
            if (rareRelicPicker.TryGetNext(out RelicData data))
            {
                relicData = data;
            }
            else
            {
                rareRelicPicker.Reset();
                if (rareRelicPicker.TryGetNext(out RelicData newData))
                {
                    relicData = newData;
                }
            }
        }
        else
        {
            commonRelicPicker.Reset();

            //common유물들 중 랜덤한 1장 선택
            if (commonRelicPicker.TryGetNext(out RelicData data))
            {
                relicData = data;
            }
            else
            {
                commonRelicPicker.Reset();
                if (commonRelicPicker.TryGetNext(out RelicData newData))
                {
                    relicData = newData;
                }
            }
        }

        return relicData;
    }

    public void RemoveObtainedRelicByPlayer(RelicData relicData)
    {
        switch(relicData.level)
        {
            case 1:
                commonRelicList.Remove(relicData);
                commonRelicPicker = new RandomItemPicker<RelicData>(commonRelicList);
                break;
            case 2:
                rareRelicList.Remove(relicData);
                rareRelicPicker = new RandomItemPicker<RelicData>(rareRelicList);
                break;
            case 3:
                uniqueRelicList.Remove(relicData);
                uniqueRelicPicker = new RandomItemPicker<RelicData>(uniqueRelicList);
                break;
        }
    }

    public void InstantiateCardReward(RandomItemPickupType itemPickupType, Vector3 spawnPosition)
    {
        if (rewardObjectPrefab == null)
            return;

        //카드 보상 생성
        RewardItemHandler rewardNPC = Instantiate(rewardObjectPrefab).GetComponent<RewardItemHandler>();
        if (rewardNPC != null)
        {
            rewardNPC.SetReward(RewardItemType.Card, itemPickupType, 0);
            rewardNPC.transform.position = spawnPosition;
        }
        //맵 이동 시 지워질 오브젝트 목록으로 등록
        GameManager.instance.currentSpawnNPCList.Add(rewardNPC.gameObject);
        GameManager.instance.currentSpawnUIList.Add(rewardNPC.GetRewardUI());
    }

    public void InstantiateRelicReward(RandomItemPickupType itemPickupType, Vector3 spawnPosition)
    {
        if (rewardObjectPrefab == null)
            return;

        //유물 보상 생성
        RewardItemHandler relicRewardNPC = Instantiate(rewardObjectPrefab).GetComponent<RewardItemHandler>();
        if (relicRewardNPC != null)
        {
            relicRewardNPC.SetReward(RewardItemType.Relic, itemPickupType, 0);
            relicRewardNPC.transform.position = spawnPosition;
        }
        //맵 이동 시 지워질 오브젝트 목록으로 등록
        GameManager.instance.currentSpawnNPCList.Add(relicRewardNPC.gameObject);
        GameManager.instance.currentSpawnUIList.Add(relicRewardNPC.GetRewardUI());
    }


    public List<ActionCardData> GetCommonCardList() {   return commonCardList;  }
    public List<ActionCardData> GetRareCardList() { return rareCardList; }
    public List<ActionCardData> GetUniqueCardList() { return uniqueCardList; }

    public List<RelicData> GetCommonRelicList() { return commonRelicList; }
    public List<RelicData> GetRareRelicList() { return rareRelicList; }
    public List<RelicData> GetUniqueRelicList() { return uniqueRelicList; }
}
