using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasteryPointHandler : MonoBehaviour
{
    [SerializeField] private Image masteryPointBar;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private int maxMasteryPoint;

    public void SetMasteryDescription(ActionCardData cardData, int maxPoint)
    {
        //마스터리 설명 UI 업데이트

        valueText.SetText("0 / " + maxPoint);
        descriptionText.SetText(SetMasteryDescription(cardData.cardType));
        masteryPointBar.fillAmount = 0f;

        UpdateMasteryPointUI(cardData.path);
    }

    public void UpdateMasteryPointUI(string cardID)
    {
        //마스터리 포인트 UI 업데이트
        if(!CardMasteryManager.instance)
        {
            Debug.LogWarning("CardMasteryManager.instance is null!");
            return;
        }

        CardMasteryStat masteryStat = CardMasteryManager.instance.GetCardMasteryStat(cardID);
        if(masteryStat != null)
        {
            valueText.SetText(masteryStat.current_mastery_value + " / " + masteryStat.max_mastery_value);
            float fillAmount = masteryStat.max_mastery_value > 0 ? masteryStat.current_mastery_value / masteryStat.max_mastery_value : 0f;
            masteryPointBar.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }

    public void PreviewUpgradeMasteryPointUI(string cardID, int increaseMasteryPoint)
    {
        //마스터리 포인트 UI 업데이트
        if (!CardMasteryManager.instance)
        {
            Debug.LogWarning("CardMasteryManager.instance is null!");
            return;
        }

        CardMasteryStat masteryStat = CardMasteryManager.instance.GetCardMasteryStat(cardID);
        if (masteryStat != null)
        {
            valueText.SetText(masteryStat.current_mastery_value + " + " +
                "<color=#00EB00>" + increaseMasteryPoint + "</color>" +
                " / " +masteryStat.max_mastery_value);

            float fillAmount = masteryStat.max_mastery_value > 0 ? masteryStat.current_mastery_value / masteryStat.max_mastery_value : 0f;
            masteryPointBar.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }

    public string SetMasteryDescription(string cardType)
    {
        switch(cardType)
        {
            case "Attack":
                return "카드 사용: 10\n" +
                       "적 처치: 50\n" +
                       "입힌 피해: 피해 수치";
            case "Skill":
                return "카드 사용: 10\n" +
                       "입힌 피해: 피해 수치\n" +
                       "막은 피해: 막힌 수치\n" +
                       "힐량 : 회복된 체력 수치";
            case "Utility":
                return "카드 사용: 10";
        }

        return "";
    }
}
