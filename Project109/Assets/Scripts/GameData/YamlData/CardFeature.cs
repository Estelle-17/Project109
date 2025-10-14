using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardFeature
{
    public string cardFeatureType;
    public float featureValue;

    public int chainStaminaCost;
    public List<SkillEffect> chainEffect;
    public List<SkillCondition> chainCondition;
}
