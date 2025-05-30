using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RelicData", menuName = "Relic/RelicData")]
public class RelicData : ScriptableObject
{
    public string classType;
    public string relicName;
    public int level;
    public List<RelicCondition> conditions;
    public List<RelicEffect> effects;
}
