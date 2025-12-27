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
    public int rarity;
    public int stamina;
    public string cardType;
    public string targetType;
    public List<string> effectArea;
    public string texturePath;
    public string evolvedCardPath;
    public string upgradeCardPath;

    public bool isEvolved;
    public EvolveType evolveType;

    public bool isUpgrade;

    public string ID => path;
    public int runtimeID;
}
