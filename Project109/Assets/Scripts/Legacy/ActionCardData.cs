#if false
using UnityEngine;
using System.Collections.Generic;
using GameItem.Types;

[CreateAssetMenu(fileName = "ActionCardData", menuName = "ActionCard/ActionCardData")]
public class ActionCardData : ScriptableObject, IIdentifiable
{
    public Sprite cardTexture;
    public string path;
    public string className;
    public string cardName;
    public bool bIsUpgradeCard;
    public int rarity;
    public int stamina;
    public string cardType;
    public string targetType;
    public int targetMinDistance;
    public int targetMaxDistance;
    public List<EffectArea> additionalEffectAreaList;
    public List<int> amountList;
    public string texturePath;
    public string upgradeCardPath;
    public int maxMasteryPoint;
    public List<int> specificProperties;

    public bool isEvolved;
    public EvolveType evolveType;

    public bool isUpgrade;

    public string ID => path;
    public int runtimeID;
}
#endif