using UnityEngine;

public class RewardBoxManager : MonoBehaviour
{
    [SerializeField] private GameObject rewardBoxModelPrefab;

    public GameObject rewardListUIPrefab;
    private RewardListUIHandler rewardListUI;

    void Start()
    {
        
    }

    public void SetRewardItemList()
    {
        rewardListUI = Instantiate(rewardListUIPrefab).GetComponent<RewardListUIHandler>();

        if(rewardListUI != null)
        {
            //아이템 보상의 재화 수량은 데이터로 정해질 예정
            switch(GameManager.instance.currentIncountNode.incountType)
            {
                case IncountType.Battle:
                    rewardListUI.AddRewardItem(ItemRewardUIType.Gold, RandomItemPickupType.Common, 60);
                    rewardListUI.AddRewardItem(ItemRewardUIType.MemorySharp, RandomItemPickupType.Common, 2);
                    rewardListUI.AddRewardItem(ItemRewardUIType.Card, RandomItemPickupType.CommonToUnique, 1);
                    break;
                case IncountType.Elite:
                    rewardListUI.AddRewardItem(ItemRewardUIType.Gold, RandomItemPickupType.Common, 80);
                    rewardListUI.AddRewardItem(ItemRewardUIType.MemorySharp, RandomItemPickupType.Common, 5);
                    rewardListUI.AddRewardItem(ItemRewardUIType.Card, RandomItemPickupType.CommonToUnique, 1);
                    rewardListUI.AddRewardItem(ItemRewardUIType.Relic, RandomItemPickupType.CommonToUnique, 1);
                    break;
                case IncountType.Boss:
                    rewardListUI.AddRewardItem(ItemRewardUIType.Gold, RandomItemPickupType.Common, 100);
                    rewardListUI.AddRewardItem(ItemRewardUIType.MemorySharp, RandomItemPickupType.Common, 15);
                    rewardListUI.AddRewardItem(ItemRewardUIType.Card, RandomItemPickupType.Unique, 1);
                    rewardListUI.AddRewardItem(ItemRewardUIType.Relic, RandomItemPickupType.Unique, 1);
                    break;
                default:
                    break;
            }
        }

        rewardListUI.gameObject.SetActive(false);
    }

    public void EnableRewardListUI()
    {
        rewardListUI.UIActive();
    }

    public void DisableRewardListUI()
    {
        rewardListUI.UIDeactive();
    }
}