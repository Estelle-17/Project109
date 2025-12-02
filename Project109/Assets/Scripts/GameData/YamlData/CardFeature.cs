using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardFeature
{
    public string cardFeatureType;
    public string description;
    public float featureValue;
    public List<CardConditionalEffect> bonusEffect;

    public int chainStaminaCost;
    public List<SkillEffect> chainEffect;
    public List<SkillCondition> chainCondition;
}
