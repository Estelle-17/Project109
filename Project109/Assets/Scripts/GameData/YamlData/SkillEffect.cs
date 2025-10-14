using UnityEngine;

[System.Serializable]
public class SkillEffect
{
    public string effectType;
    public float baseValue;
    public string scalingStat;
    public float scalingRatio;
    public string targetMode;
    public int times;
    //Buff or Debuff일 경우 적용될 상태 효과 타입
    public string statusEffectType;
    public int durationTime;
}
