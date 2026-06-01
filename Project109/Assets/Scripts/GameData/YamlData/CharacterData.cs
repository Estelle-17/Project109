using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject, IIdentifiable
{
    public GameObject characterObject;
    public string classType;
    public string characterName;
    public string assetPath;
    public int level;
    public string description;
    public CharacterStat characterStat;
    public string ID => characterName;
}
