using UnityEngine;

[CreateAssetMenu(menuName = "NewCardStat")]
public class CardStat : ScriptableObject
{
    [Header("Base Stats")]
    public EffectType effectType;
    public BuffType buffType;
    public DebuffType debuffType;
    public float amount;
    public int effectDuration;
    public int times;
    public TargetType targetType;
}
