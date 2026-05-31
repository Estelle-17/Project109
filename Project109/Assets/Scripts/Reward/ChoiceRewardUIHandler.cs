using UnityEngine;
using GameItem.Types;

public class ChoiceRewardUIHandler : MonoBehaviour, IInteractable
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
                CardRewardPanel cardRewardHandler = currentRewardUIObject.GetComponent<CardRewardPanel>();
                cardRewardHandler.SettingCards(cardType, RunManager.instance.player.playerStat.reward_Card_Count);    //보여줄 아이템의 수는 상황에 따라 변경 가능
                cardRewardHandler.rootObject = this.gameObject;
                DisableRewardUI();
                break;
            case RewardItemType.Relic:
                currentRewardUIObject = Instantiate(relicRewardPrefab);
                RelicRewardPanel relicRewardHandler = currentRewardUIObject.GetComponent<RelicRewardPanel>();
                relicRewardHandler.SettingRelics(reilcType, RunManager.instance.player.playerStat.reward_Card_Count);    //보여줄 아이템의 수는 상황에 따라 변경 가능
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

    public bool RequiresCameraFocus => true;

    public void OnInteract()
    {
        EnableRewardUI();
    }
}
