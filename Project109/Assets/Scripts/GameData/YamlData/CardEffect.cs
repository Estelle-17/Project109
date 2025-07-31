using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CardEffect
{
    public List<SkillEffect> effects;
    public List<SkillCondition> conditions;
    public List<CardFeature> features;
}
