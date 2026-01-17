using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

    private Dictionary<string, Dictionary<string, int>> masteryUpgradeList;

    void Start()
    {
        masteryUpgradeList = new Dictionary<string, Dictionary<string, int>>();
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

    ///작동 확인용 임시 코드
    public GameObject cardMasteryUIPrefab;

    public void OpenCardMasteryTestUI(ActionCardData testCardData)
    {
        GameObject uiObj = Instantiate(cardMasteryUIPrefab);
        MasteryUpgradeUIHandler uiHandler = uiObj.GetComponent<MasteryUpgradeUIHandler>();
        if (uiHandler != null)
        {
            uiHandler.CreateMasteryChoices(testCardData);
        }
        else
        {
            Debug.LogError("MasteryUpgradeUIHandler component not found on cardMasteryUIPrefab.");
        }
    }
}
