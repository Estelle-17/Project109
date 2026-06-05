using UnityEngine;

[CreateAssetMenu(fileName = "RewardChestData", menuName = "Dialogue/RewardChestData")]
public class RewardChestData : InteractableData
{
    [Header("보상 상자 추가 데이터")]
    public RewardData rewardData;             // 보상 상자일 때 획득 가능한 보상 기획 데이터
}
