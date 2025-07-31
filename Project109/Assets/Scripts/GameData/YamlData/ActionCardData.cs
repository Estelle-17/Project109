using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ActionCardData", menuName = "ActionCard/ActionCardData")]
public class ActionCardData : ScriptableObject, IIdentifiable
{
    public Texture2D cardTexture;
    public string className;
    public string cardName;
    public string texturePath;
    public int level;
    public int useStamina;
    public List<EffectArea> effectArea;
    public CardEffect defaultEffects;
    public CardEffect upgradeEffects;
    public int upgradeCount;

    public string ID => cardName;
    public int runtimeID;
}
