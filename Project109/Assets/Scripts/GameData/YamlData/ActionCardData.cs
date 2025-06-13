using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ActionCardData", menuName = "ActionCard/ActionCardData")]
public class ActionCardData : ScriptableObject
{
    public string className;
    public string cardName;
    public int level;
    public List<EffectArea> effectArea;
    public int useStamina;
    public List<CardEffect> cardEffects;
    public List<SkillEffect> effects;
    public List<SkillEffect> upgradeEffects;
    public List<SkillCondition> conditions;
}
