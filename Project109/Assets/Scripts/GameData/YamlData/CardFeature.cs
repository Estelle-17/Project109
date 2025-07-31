using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardFeature
{
    public string cardFeatureType;
    public List<SkillEffect> chainEffect;
    public List<SkillCondition> chainCondition;
}
