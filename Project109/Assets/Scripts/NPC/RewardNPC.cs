using UnityEngine;

public enum RewardNPCType
{ 
    Card,
    Relic
}

public class RewardNPC : MonoBehaviour
{
    [SerializeField] private GameObject cardRewardPrefab;
    [SerializeField] private GameObject relicRewardPrefab;

    private GameObject currentRewardUIObject;

    void Start()
    {
        
    }

    public void SetReward(RewardNPCType npcType, RandomItemPickupType itemPickupType)
    {
        switch (npcType)
        {
            case RewardNPCType.Card:
                currentRewardUIObject = Instantiate(cardRewardPrefab);
                CardRewardHandler cardRewardHandler = currentRewardUIObject.GetComponent<CardRewardHandler>();
                cardRewardHandler.SettingCards(itemPickupType, PlayerDataManager.instance.GetPlayerStat().reward_Card_Count);    //보여줄 아이템의 수는 상황에 따라 변경 가능
                cardRewardHandler.rewardNPCObject = this.gameObject;
                DisableRewardUI();
                break;
            case RewardNPCType.Relic:
                currentRewardUIObject = Instantiate(relicRewardPrefab);
                RelicRewardHandler relicRewardHandler = currentRewardUIObject.GetComponent<RelicRewardHandler>();
                relicRewardHandler.SettingRelics(itemPickupType, PlayerDataManager.instance.GetPlayerStat().reward_Card_Count);    //보여줄 아이템의 수는 상황에 따라 변경 가능
                relicRewardHandler.rewardNPCObject = this.gameObject;
                DisableRewardUI();
                break;
        }
    }

    public GameObject GetRewardUI() {  return currentRewardUIObject; }

    public void EnableRewardUI()
    {
        currentRewardUIObject.SetActive(true);
    }

    public void DisableRewardUI()
    {
        currentRewardUIObject.SetActive(false);
    }
}
