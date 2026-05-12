using UnityEngine;

[CreateAssetMenu(fileName = "RelicData", menuName = "Relic/RelicData")]
public class RelicData : ScriptableObject, IIdentifiable
{
    public Sprite relicTexture;
    public string classType;
    public string relicName;
    public string texturePath;
    public string dataPath;
    public int rarity;
    public bool canUpgrade;
    public string description;
    public string upgradeDescription;

    public string ID => relicName;
}
