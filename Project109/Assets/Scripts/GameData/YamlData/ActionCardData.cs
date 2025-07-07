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
    public List<EffectArea> effectArea;
    public int useStamina;
    public List<CardEffect> cardEffects;
    public List<SkillEffect> effects;
    public List<SkillEffect> upgradeEffects;
    public List<SkillCondition> conditions;

    public string ID => cardName;
}
