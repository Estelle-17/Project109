using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ActionCardData", menuName = "ActionCard/ActionCardData")]
public class ActionCardData : ScriptableObject, IIdentifiable
{
    public Sprite cardTexture;
    public string className;
    public string cardName;
    public string texturePath;
    public string dataPath;
    public int level;
    public CardEffect defaultEffects;
    public CardEffect upgradeEffects;
    public bool isUpgrade;

    public string ID => cardName;
    public int runtimeID;
}
