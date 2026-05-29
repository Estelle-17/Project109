using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Card.Types;

/// <summary>
/// 숙련도 업그레이드 UI 생성 및 경험치 정산 규칙을 연동하는 브릿지 클래스.
/// 실제 카드 숙련도 상태 데이터는 각 CardBase 인스턴스가 소유합니다.
/// </summary>
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

    [SerializeField] private GameObject masteryUpgradeUIPrefab;

    void Start()
    {
        // 더 이상 매니저 내부에 개별 카드 숙련도 목록을 유지하지 않습니다. (CardBase로 단일화)
    }

    public CardMasteryStat GetCardMasteryStat(CardBase card)
    {
        return card?.masteryStat;
    }
    
    public Dictionary<string, int> GetCardMasteryUpgrades(CardBase card)
    {
        return card?.activeMasteryUpgrades ?? new Dictionary<string, int>();
    }

    public int GetCurrentMasteryUpgradeLevel(CardBase card, string masteryID)
    {
        return card?.GetMasteryLevel(masteryID) ?? 0;
    }

    public void AddMastery(CardBase card, string masteryID)
    {
        card?.AddMastery(masteryID);
    }

    public void ProcessMasteryUpgrade(CardBase card)
    {
        if (masteryUpgradeUIPrefab == null)
        {
            Debug.LogError("Mastery Upgrade UI Prefab is not assigned!");
            return;
        }

        GameObject uiObj = Instantiate(masteryUpgradeUIPrefab);
        MasteryUpgradeUIHandler uiHandler = uiObj.GetComponent<MasteryUpgradeUIHandler>();
        if (uiHandler != null)
        {
            uiHandler.UIActive();
            uiHandler.CreateMasteryChoices(card);
        }
        else
        {
            Debug.LogError("MasteryUpgradeUIHandler component not found on masteryUpgradeUIPrefab.");
        }
    }

    public void ReportAction(CardMasteryType triggerType, float amount, CardBase card)
    {
        if (card == null) return;

        Debug.Log($"Reporting action {triggerType} for card {card.path} with amount {amount}");

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
            case CardMasteryType.UpgradeCard:
                xpGain = amount;
                break;
        }
        card.AddMasteryPoint(xpGain);
    }
}
