using UnityEngine;

public enum RewardItemType
{
    Card,
    Relic
}

public class RewardItemHandler : MonoBehaviour
{
    [SerializeField] private GameObject cardRewardPrefab;
    [SerializeField] private GameObject relicRewardPrefab;

    private GameObject currentRewardUIObject;

    void Start()
    {
        
    }

    public void SetReward(RewardItemType npcType, RandomCardPickupType cardType, RandomRelicPickupType reilcType, int value)
    {
        switch (npcType)
        {
            case RewardItemType.Card:
                currentRewardUIObject = Instantiate(cardRewardPrefab);
                CardRewardHandler cardRewardHandler = currentRewardUIObject.GetComponent<CardRewardHandler>();
                cardRewardHandler.SettingCards(cardType, PlayerDataManager.instance.GetPlayerStat().reward_Card_Count);    //보여줄 아이템의 수는 상황에 따라 변경 가능
                cardRewardHandler.rootObject = this.gameObject;
                DisableRewardUI();
                break;
            case RewardItemType.Relic:
                currentRewardUIObject = Instantiate(relicRewardPrefab);
                RelicRewardHandler relicRewardHandler = currentRewardUIObject.GetComponent<RelicRewardHandler>();
                relicRewardHandler.SettingRelics(reilcType, PlayerDataManager.instance.GetPlayerStat().reward_Card_Count);    //보여줄 아이템의 수는 상황에 따라 변경 가능
                relicRewardHandler.rootObject = this.gameObject;
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
