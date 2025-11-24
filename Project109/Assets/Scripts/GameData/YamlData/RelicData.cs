using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RelicData", menuName = "Relic/RelicData")]
public class RelicData : ScriptableObject, IIdentifiable
{
    public Sprite relicTexture;
    public string classType;
    public string relicName;
    public string texturePath;
    public string dataPath;
    public int level;
    public string description;
    public List<RelicCondition> conditions;
    public List<RelicEffect> effects;

    public string ID => relicName;
}
