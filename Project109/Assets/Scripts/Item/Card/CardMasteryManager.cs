using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Card.Types;

public class CardMasteryManager : MonoBehaviour
{
    public static CardMasteryManager instance { get; private set; }

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

    private Dictionary<string, CardMasteryStat> cardMasteryList;
    private Dictionary<string, Dictionary<string, int>> masteryUpgradeList;

    [SerializeField] private GameObject masteryUpgradeUIPrefab;

    void Start()
    {
        masteryUpgradeList = new Dictionary<string, Dictionary<string, int>>();
        cardMasteryList = new Dictionary<string, CardMasteryStat>();

        //숙련도 시스템이 적용된 카드 데이터 추가
        if (AssetCacheManager.instance != null)
        {
            foreach (ActionCardData cardData in AssetCacheManager.instance.cardList)
            {
                if(cardData.maxMasteryPoint > 0)
                {
                    CardMasteryStat masteryStat = new CardMasteryStat
                    {
                        mastery_level = 0,
                        max_mastery_value = cardData.maxMasteryPoint,
                        current_mastery_value = 0,
                        mastery_increaseValue_on_level = cardData.maxMasteryPoint / 2.0f
                    };
                    cardMasteryList.Add(cardData.path, masteryStat);
                }
            }
        }
    }

    public CardMasteryStat GetCardMasteryStat(string cardID)
    {
        if (cardMasteryList.ContainsKey(cardID))
        {
            return cardMasteryList[cardID];
        }
        return null;
    }
    
    public Dictionary<string, int> GetCardMasteryUpgrades(string cardID)
    {
        if (masteryUpgradeList.ContainsKey(cardID))
        {
            return masteryUpgradeList[cardID];
        }
        return new Dictionary<string, int>();
    }

    public void ProcessMasteryUpgrade(ActionCardData cardData)
    {
        if(masteryUpgradeUIPrefab == null)
        {
            Debug.LogError("Mastery Upgrade UI Prefab is not assigned!");
            return;
        }

        GameObject uiObj = Instantiate(masteryUpgradeUIPrefab);
        MasteryUpgradeUIHandler uiHandler = uiObj.GetComponent<MasteryUpgradeUIHandler>();
        if (uiHandler != null)
        {
            uiHandler.UIActive();
            uiHandler.CreateMasteryChoices(cardData);
        }
        else
        {
            Debug.LogError("MasteryUpgradeUIHandler component not found on masteryUpgradeUIPrefab.");
        }
    }

    public int GetCurrentMasteryUpgradeLevel(string cardID, string masteryID)
    {
        if (masteryUpgradeList.ContainsKey(cardID) &&
            masteryUpgradeList[cardID].ContainsKey(masteryID))
        {
            return masteryUpgradeList[cardID][masteryID];
        }
        return 0;
    }

    public void AddMastery(string cardID, string masteryID)
    {
        if (!masteryUpgradeList.ContainsKey(cardID))
        {
            masteryUpgradeList.Add(cardID, new Dictionary<string, int>());
        }
        if (!masteryUpgradeList[cardID].ContainsKey(masteryID))
        {
            masteryUpgradeList[cardID].Add(masteryID, 1);
        }
        else
        {
            masteryUpgradeList[cardID][masteryID]++;
        }

        //마스터리 레벨 및 수치 갱신
        cardMasteryList[cardID].mastery_level++;
        cardMasteryList[cardID].current_mastery_value -= cardMasteryList[cardID].max_mastery_value;
        cardMasteryList[cardID].max_mastery_value += cardMasteryList[cardID].mastery_increaseValue_on_level;

        Debug.Log($"Mastery {masteryID} added to card {cardID}. Mastery Count: {masteryUpgradeList[cardID][masteryID]}");
    }

    public List<MasteryDescription> GetRandomMasteryOption(ActionCardData targetCardData, int optionCount)
    {
        List<MasteryDescription> masteryOptions = new List<MasteryDescription>();

        if (AssetCacheManager.instance.TryGetCardDescription(targetCardData.path, out CardDescription description))
        {
            foreach (MasteryDescription mastery in description.masteryDescriptions)
            {
                //현재 마스터리의 레벨 확인
                int currentUpgradeCount = GetCurrentMasteryUpgradeLevel(targetCardData.path, mastery.path);

                //만렙이 아닌 마스터리는 후보군으로 추가
                if (currentUpgradeCount < mastery.maxUpgradeCount || mastery.maxUpgradeCount == 0)
                {
                    masteryOptions.Add(mastery);
                }
            }
        }
        else
        {
            Debug.LogError($"Card description not found for card path: {targetCardData.path}");
        }

        return masteryOptions.OrderBy(x => Random.value).Take(optionCount).ToList();
    }

    public void ReportAction(CardMasteryType triggerType, float amount, ActionCardData cardData)
    {
        if(cardData == null)
        {
            return;
        }

        Debug.Log($"Reporting action {triggerType} for card {cardData.path} with amount {amount}");

        AddMasteryPoint(triggerType, amount, cardData);
    }

    //숙련도 포인트 추가 함수
    private void AddMasteryPoint(CardMasteryType triggerType, float amount, ActionCardData cardData)
    {
        //숙련도 포인트 추가 로직
        float xpGain = 0;

        switch (triggerType)
        {
            case CardMasteryType.UseCard:
                xpGain = 10;
                break;
                case CardMasteryType.DealDamage:
                xpGain = amount;
                break;
                case CardMasteryType.GuardDamage:
                xpGain = amount;
                break;
                case CardMasteryType.KillEnemy:
                xpGain = 50;
                break;
                case CardMasteryType.Heal:
                xpGain = amount * 1.5f;
                break;
                case CardMasteryType.DrawCard:
                xpGain = 5;
                break;
        }

        if (cardMasteryList.ContainsKey(cardData.path))
        {
            cardMasteryList[cardData.path].current_mastery_value += xpGain;
            Debug.Log($"Added {xpGain} mastery points to card {cardData.name}. Current mastery points: {cardMasteryList[cardData.path].current_mastery_value}/{cardMasteryList[cardData.path].max_mastery_value}");
            
            //숙련도 업그레이드가 가능한지 확인
            if(cardMasteryList[cardData.path].current_mastery_value >= cardMasteryList[cardData.path].max_mastery_value)
            {
                //마스터리 업그레이드 UI 표시
                ProcessMasteryUpgrade(cardData);
            }
        }
    }
}
