using UnityEngine;
using GameItem.Types;

public class RewardBoxManager : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject rewardBoxModelPrefab;

    public GameObject rewardListUIPrefab;
    private RewardListUIHandler rewardListUI;

    void Start()
    {
        
    }

    public void SetRewardItemList()
    {
        if(RunManager.instance.currentIncountNode == null)
        {
            return;
        }

        rewardListUI = Instantiate(rewardListUIPrefab).GetComponent<RewardListUIHandler>();

        if(rewardListUI != null)
        {
            //아이템 보상의 재화 수량은 데이터로 정해질 예정
            switch(RunManager.instance.currentIncountNode.incountType)
            {
                case IncountType.Battle:
                    rewardListUI.AddRewardItem(ItemRewardUIType.Gold, RandomCardPickupType.Common, RandomRelicPickupType.Common, 60);
                    rewardListUI.AddRewardItem(ItemRewardUIType.MemorySharp, RandomCardPickupType.Common, RandomRelicPickupType.Common, 2);
                    rewardListUI.AddRewardItem(ItemRewardUIType.Card, RandomCardPickupType.CommonToUncommon, RandomRelicPickupType.Common, 1);
                    break;
                case IncountType.Elite:
                    rewardListUI.AddRewardItem(ItemRewardUIType.Gold, RandomCardPickupType.Common, RandomRelicPickupType.Common, 80);
                    rewardListUI.AddRewardItem(ItemRewardUIType.MemorySharp, RandomCardPickupType.Common, RandomRelicPickupType.Common, 5);
                    rewardListUI.AddRewardItem(ItemRewardUIType.Card, RandomCardPickupType.CommonToUncommon, RandomRelicPickupType.Common, 1);
                    rewardListUI.AddRewardItem(ItemRewardUIType.Relic, RandomCardPickupType.Common, RandomRelicPickupType.CommonToUnique, 1);
                    break;
                case IncountType.Boss:
                    rewardListUI.AddRewardItem(ItemRewardUIType.Gold, RandomCardPickupType.Common, RandomRelicPickupType.Common, 150);
                    rewardListUI.AddRewardItem(ItemRewardUIType.MemorySharp, RandomCardPickupType.Common, RandomRelicPickupType.Common, 10);
                    rewardListUI.AddRewardItem(ItemRewardUIType.Card, RandomCardPickupType.CommonToUncommon, RandomRelicPickupType.Common, 1);
                    break;
                default:
                    break;
            }
        }

        rewardListUI.gameObject.SetActive(false);
    }
    
    public GameObject GetRewardListUI()
    {
        return rewardListUI.gameObject;
    }

    public void EnableRewardListUI()
    {
        rewardListUI?.UIActive();
    }

    public void DisableRewardListUI()
    {
        rewardListUI?.UIDeactive();
    }

    public bool RequiresCameraFocus => true;

    public void OnInteract()
    {
        EnableRewardListUI();
    }
}