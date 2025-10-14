using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CardEffect
{    
    public List<EffectArea> effectArea;
    public int useStamina;
    public List<SkillEffect> effects;
    public List<SkillCondition> conditions;
    public List<CardFeature> features;
}
