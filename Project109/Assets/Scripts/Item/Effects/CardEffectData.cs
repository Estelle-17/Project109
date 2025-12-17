using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardEffectData")]
public class CardEffectData : ScriptableObject
{
    [SerializeReference] public CardStat cardStat;
    [SerializeReference] public List<CardEffect> cardEffects;

    public List<string> effectArea;

    public void Use(GameObject target)
    {
        foreach (var effect in cardEffects)
        {
            //effect.ApplyEffect(target);
        }
    }
}
