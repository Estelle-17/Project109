using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Effects", menuName = "CardEffect/Damage")]
public class DamageEffect : CardEffect
{
    [Header("Effect Settings")]
    [SerializeField] private float damamgeMultiplier = 1.0f;
    [SerializeField] private float strengthMultiplier = 1.0f;
    [SerializeField] private float critChance = 0.0f;
    [SerializeField] private float critDamageMultiplier = 1.5f;
    [SerializeField] private TargetType targetType = TargetType.Target;

    public override void ApplyEffect(GameObject player, GameObject target, CardStat cardStat)
    {
        //데미지 계산
        for(int i = 0; i < cardStat.times; i++)
        {
            float damage = cardStat.amount * damamgeMultiplier;
        }

        //TakeDamage 호출
    }
}