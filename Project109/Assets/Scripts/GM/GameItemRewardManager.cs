using UnityEngine;
using System.Collections.Generic;
using GameItem.Types;

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
    private List<ActionCardData> uncommonCardList = new List<ActionCardData>();
    private List<ActionCardData> rareCardList = new List<ActionCardData>();
    private List<ActionCardData> uniqueCardList = new List<ActionCardData>();

    RandomItemPicker<ActionCardData> commonCardPicker;
    RandomItemPicker<ActionCardData> uncommonCardPicker;
    RandomItemPicker<ActionCardData> rareCardPicker;
    RandomItemPicker<ActionCardData> uniqueCardPicker;

    //Relics
    private List<RelicData> commonRelicList = new List<RelicData>();
    private List<RelicData> rareRelicList = new List<RelicData>();
    private List<RelicData> uniqueRelicList = new List<RelicData>();
    private List<RelicData> bossRelicList = new List<RelicData>();

    RandomItemPicker<RelicData> commonRelicPicker;
    RandomItemPicker<RelicData> rareRelicPicker;
    RandomItemPicker<RelicData> uniqueRelicPicker;
    RandomItemPicker<RelicData> bossRelicPicker;

    //등급별 확률
    int commonRate;
    int uncommonRate;
    int rareRate;
    int uniqueRate;

    void Start()
    {
        //기본 확률(유물 및 다양한 요소에 의해 변경 가능)
        commonRate = 70;
        rareRate = 25;
        uniqueRate = 5;
    }

    public void ResetCardLists()
    {
        //Card Picker 초기화
        commonCardPicker = new RandomItemPicker<ActionCardData>(commonCardList);
        uncommonCardPicker = new RandomItemPicker<ActionCardData>(uncommonCardList);
        rareCardPicker = new RandomItemPicker<ActionCardData>(rareCardList);
        uniqueCardPicker = new RandomItemPicker<ActionCardData>(uniqueCardList);
    }

    public void ResetRelicLists()
    {
        //Relic Picker 초기화
        commonRelicPicker = new RandomItemPicker<RelicData>(commonRelicList);
        rareRelicPicker = new RandomItemPicker<RelicData>(rareRelicList);
        uniqueRelicPicker = new RandomItemPicker<RelicData>(uniqueRelicList);
        bossRelicPicker = new RandomItemPicker<RelicData>(bossRelicList);
    }

    public void UpdateItemList()
    {
        foreach(ActionCardData data in AssetCacheManager.instance.cardList)
        {
            switch (data.rarity) //1~3
            {
                case 1:
                    commonCardList.Add(data);
                    break;
                case 2:
                    uncommonCardList.Add(data);
                    break;
                case 3:
                    rareCardList.Add(data);
                    break;
                case 4:
                    uniqueCardList.Add(data);
                    break;
            }
        }

        foreach (RelicData data in AssetCacheManager.instance.relicList)
        {
            switch (data.rarity) //1~3
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
                case 4:
                    bossRelicList.Add(data);
                    break;
            }
        }

        //Card Picker 초기화
        commonCardPicker = new RandomItemPicker<ActionCardData>(commonCardList);
        uncommonCardPicker = new RandomItemPicker<ActionCardData>(uncommonCardList);
        rareCardPicker = new RandomItemPicker<ActionCardData>(rareCardList);
        uniqueCardPicker = new RandomItemPicker<ActionCardData>(uniqueCardList);

        //Relic Picker 초기화
        commonRelicPicker = new RandomItemPicker<RelicData>(commonRelicList);
        rareRelicPicker = new RandomItemPicker<RelicData>(rareRelicList);
        uniqueRelicPicker = new RandomItemPicker<RelicData>(uniqueRelicList);
        bossRelicPicker = new RandomItemPicker<RelicData>(bossRelicList);
    }

    public void EraseRelicFromList(RelicData relicData)
    {
        switch (relicData.rarity)
        {
            case 1:
                Debug.Log("Removing common Relic: " + relicData.relicName + ", Count: "+ commonRelicList.Count);
                foreach (RelicData data in commonRelicList)
                {
                    if (data.relicName == relicData.relicName)
                    {
                        commonRelicList.Remove(data);
                        break;
                    }
                }
                commonRelicPicker = new RandomItemPicker<RelicData>(commonRelicList);
                Debug.Log("commonRelicPicker Count: " + commonRelicPicker.Count());
                break;
            case 2:
                Debug.Log("Removing rare Relic: " + relicData.relicName + ", Count: " + rareRelicList.Count);
                foreach (RelicData data in rareRelicList)
                {
                    if (data.relicName == relicData.relicName)
                    {
                        rareRelicList.Remove(data);
                        break;
                    }
                }
                rareRelicPicker = new RandomItemPicker<RelicData>(rareRelicList);
                Debug.Log("rareRelicPicker Count: " + rareRelicPicker.Count());
                break;
            case 3:
                Debug.Log("Removing unique Relic: " + relicData.relicName + ", Count: " + uniqueRelicList.Count);
                foreach (RelicData data in uniqueRelicList)
                {
                    if (data.relicName == relicData.relicName)
                    {
                        uniqueRelicList.Remove(data);
                        break;
                    }
                }
                uniqueRelicPicker = new RandomItemPicker<RelicData>(uniqueRelicList);
                Debug.Log("uniqueRelicPicker Count: " + uniqueRelicPicker.Count());
                break;
            case 4:
                Debug.Log("Removing boss Relic: " + relicData.relicName + ", Count: " + bossRelicList.Count);
                foreach (RelicData data in bossRelicList)
                {
                    if (data.relicName == relicData.relicName)
                    {
                        bossRelicList.Remove(data);
                        break;
                    }
                }
                bossRelicPicker = new RandomItemPicker<RelicData>(bossRelicList);
                Debug.Log("bossRelicPicker Count: " + bossRelicPicker.Count());
                break;
        }
        Debug.Log("Relic Removed from List: " + relicData.relicName);
    }

    public void AddRelicFromList(RelicData relicData)
    {
        switch (relicData.rarity)
        {
            case 1:
                if (commonRelicList.Contains(relicData))
                    return;

                commonRelicList.Add(relicData);
                commonRelicPicker = new RandomItemPicker<RelicData>(commonRelicList);
                break;
            case 2:
                if (rareRelicList.Contains(relicData))
                    return;

                rareRelicList.Add(relicData);
                rareRelicPicker = new RandomItemPicker<RelicData>(rareRelicList);
                break;
            case 3:
                if (uniqueRelicList.Contains(relicData))
                    return;

                uniqueRelicList.Add(relicData);
                uniqueRelicPicker = new RandomItemPicker<RelicData>(uniqueRelicList);
                break;
            case 4:
                if (bossRelicList.Contains(relicData))
                    return;

                bossRelicList.Add(relicData);
                bossRelicPicker = new RandomItemPicker<RelicData>(bossRelicList);
                break;
        }
        Debug.Log("Relic Added to List: " + relicData.relicName);
    }

    public ActionCardData GetRandomCardDataByPickupType(RandomCardPickupType pickupType)
    {
        //랜덤한 숫자 선택
        int pickNumber = Random.Range(1, 101);

        ActionCardData cardData = ScriptableObject.CreateInstance<ActionCardData>();

        //픽업 타입에 따른 확률 조정
        switch (pickupType)
        {
            case RandomCardPickupType.Common:
                commonRate = 100;
                uncommonRate = 0;
                rareRate = 0;
                uniqueRate = 0;
                break;
            case RandomCardPickupType.Uncommon:
                commonRate = 0;
                uncommonRate = 100;
                rareRate = 0;
                uniqueRate = 0;
                break;
            case RandomCardPickupType.Rare:
                commonRate = 0;
                uncommonRate = 0;
                rareRate = 100;
                uniqueRate = 0;
                break;
            case RandomCardPickupType.Unique:
                commonRate = 0;
                uncommonRate = 0;
                rareRate = 0;
                uniqueRate = 100;
                break;
            case RandomCardPickupType.CommonToUncommon:
                commonRate = 60;
                uncommonRate = 40;
                rareRate = 0;
                uniqueRate = 0;
                break;
            case RandomCardPickupType.RareToUnique:
                commonRate = 0;
                uncommonRate = 0;
                rareRate = 80;
                uniqueRate = 20;
                break;
        }

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
        }
        else if (pickNumber > uniqueRate + rareRate && pickNumber <= uniqueRate + rareRate + uncommonRate)
        {
            //uncommon카드들 중 랜덤한 1장 선택
            if (uncommonCardPicker.TryGetNext(out ActionCardData data))
            {
                cardData = data;
            }
            else
            {
                uncommonCardPicker.Reset();
                if (uncommonCardPicker.TryGetNext(out ActionCardData newData))
                {
                    cardData = newData;
                }
            }
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
        }

        return cardData;
    }

    public RelicData GetRandomRelicDataByPickupType(RandomRelicPickupType pickupType)
    {
        //랜덤한 유물 선택 후 등록
        int pickNumber = Random.Range(1, 101);

        RelicData relicData = ScriptableObject.CreateInstance<RelicData>();

        //픽업 타입에 따른 확률 조정
        switch (pickupType)
        {
            case RandomRelicPickupType.Common:
                commonRate = 100;
                rareRate = 0;
                uniqueRate = 0;
                break;
            case RandomRelicPickupType.Rare:
                commonRate = 0;
                rareRate = 100;
                uniqueRate = 0;
                break;
            case RandomRelicPickupType.Unique:
                commonRate = 0;
                rareRate = 0;
                uniqueRate = 100;
                break;
            case RandomRelicPickupType.CommonToUnique:
                commonRate = 60;
                rareRate = 30;
                uniqueRate = 10;
                break;
            case RandomRelicPickupType.CommonToRare:
                commonRate = 70;
                rareRate = 30;
                uniqueRate = 0;
                break;
            case RandomRelicPickupType.RareToUnique:
                commonRate = 0;
                rareRate = 80;
                uniqueRate = 20;
                break;
            case RandomRelicPickupType.Boss:
                //보스 유물은 무조건 보스 유물 중에서 선택
                if (bossRelicPicker.TryGetNext(out RelicData data))
                {
                    relicData = data;
                }
                else
                {
                    bossRelicPicker.Reset();
                    if (bossRelicPicker.TryGetNext(out RelicData newData))
                    {
                        relicData = newData;
                    }
                }
                return relicData;
        }

        if (pickNumber <= uniqueRate)
        {
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
        switch(relicData.rarity)
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

    public void InstantiateItemReward(RewardItemType rewardType, RandomCardPickupType itemPickupType, Vector3 spawnPosition)
    {
        if (rewardObjectPrefab == null)
            return;

        //카드 보상 생성
        RewardItemHandler rewardNPC = Instantiate(rewardObjectPrefab).GetComponent<RewardItemHandler>();
        if (rewardNPC != null)
        {
            rewardNPC.SetReward(rewardType, itemPickupType, RandomRelicPickupType.Common, 0);
            rewardNPC.transform.position = spawnPosition;
        }
        //맵 이동 시 지워질 오브젝트 목록으로 등록
        GameManager.instance.currentSpawnNPCList.Add(rewardNPC.gameObject);
        GameManager.instance.currentSpawnUIList.Add(rewardNPC.GetRewardUI());
    }

    public void InstantiateItemReward(RewardItemType rewardType, RandomRelicPickupType itemPickupType, Vector3 spawnPosition)
    {
        if (rewardObjectPrefab == null)
            return;

        //카드 보상 생성
        RewardItemHandler rewardNPC = Instantiate(rewardObjectPrefab).GetComponent<RewardItemHandler>();
        if (rewardNPC != null)
        {
            rewardNPC.SetReward(rewardType, RandomCardPickupType.Common, itemPickupType, 0);
            rewardNPC.transform.position = spawnPosition;
        }
        //맵 이동 시 지워질 오브젝트 목록으로 등록
        GameManager.instance.currentSpawnNPCList.Add(rewardNPC.gameObject);
        GameManager.instance.currentSpawnUIList.Add(rewardNPC.GetRewardUI());
    }

    public List<ActionCardData> GetCommonCardList() {   return commonCardList;  }
    public List<ActionCardData> GetRareCardList() { return rareCardList; }
    public List<ActionCardData> GetUniqueCardList() { return uniqueCardList; }

    public List<RelicData> GetCommonRelicList() { return commonRelicList; }
    public List<RelicData> GetRareRelicList() { return rareRelicList; }
    public List<RelicData> GetUniqueRelicList() { return uniqueRelicList; }
}
