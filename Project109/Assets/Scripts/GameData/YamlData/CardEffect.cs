using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect
{
    public string cardEffectType;
    public List<SkillEffect> chainEffect;
    public List<SkillCondition> chainCondition;
}
