using UnityEngine;

public class RewardListUIHandler : UIPanelBase
{
    public GameObject rewardItemPrefab;
    public Transform rewardItemSpawnTransform;

    void Start()
    {

    }

    public void AddRewardItem(ItemRewardUIType rewardType, RandomCardPickupType cardType, RandomRelicPickupType reilcType, int value)
    {
        ItemRewardUIHandler item = Instantiate(rewardItemPrefab, rewardItemSpawnTransform).GetComponent<ItemRewardUIHandler>();

        if(item != null)
        {
            item.SetReward(rewardType, cardType, reilcType, value);
        }
    }
}
