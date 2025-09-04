using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject, IIdentifiable
{
    public GameObject characterObject;
    public string classType;
    public string characterName;
    public string assetPath;
    public int level;
    public string description;
    public float hp;
    public float stamina;
    public float staminaRegen;
    public int strength;
    public int armor;
    public List<string> startRelic;
    public List<StartCard> startCards;

    public string ID => characterName;
}
