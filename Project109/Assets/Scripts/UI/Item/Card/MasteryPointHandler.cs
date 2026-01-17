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

    public void SetMasteryDescription(string cardType, int maxPoint)
    {
        //마스터리 설명 UI 업데이트

        valueText.SetText("0 / " + maxPoint);
        descriptionText.SetText(SetMasteryDescription(cardType));
        masteryPointBar.fillAmount = 0f;

        maxMasteryPoint = maxPoint;
    }

    public void UpdateMasteryPointUI(int currentPoint)
    {
        //마스터리 포인트 UI 업데이트
        valueText.SetText(currentPoint + " / " + maxMasteryPoint);
        float fillAmount = maxMasteryPoint > 0 ? (float)currentPoint / maxMasteryPoint : 0f;
        masteryPointBar.fillAmount = Mathf.Clamp01(fillAmount);
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
