using UnityEngine;
using System.Collections.Generic;

public class GameItemContainer : MonoBehaviour
{
    public static GameItemContainer instance { get; private set; }

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

    //Relics
    private List<RelicData> commonRelicList = new List<RelicData>();
    private List<RelicData> rareRelicList = new List<RelicData>();
    private List<RelicData> uniqueRelicList = new List<RelicData>();


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
    }

    public void InstantiateCardReward(RandomItemPickupType itemPickupType, Vector3 spawnPosition)
    {
        if (rewardObjectPrefab == null)
            return;

        //카드 보상 생성
        RewardNPC rewardNPC = Instantiate(rewardObjectPrefab).GetComponent<RewardNPC>();
        if (rewardNPC != null)
        {
            rewardNPC.SetReward(RewardNPCType.Card, itemPickupType);
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
        RewardNPC relicRewardNPC = Instantiate(rewardObjectPrefab).GetComponent<RewardNPC>();
        if (relicRewardNPC != null)
        {
            relicRewardNPC.SetReward(RewardNPCType.Relic, RandomItemPickupType.CommonToUnique);
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
