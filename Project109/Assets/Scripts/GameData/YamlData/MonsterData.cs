using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Monster/MonsterData")]
public class MonsterData : ScriptableObject, IIdentifiable
{
    public GameObject monsterPrefab;
    public string monsterType;
    public string monsterName;
    public string objectPath;
    public int appearLevel;
    public float hp;
    public float stamina;
    public float staminaRegen;
    public int strength;
    public int armor;

    public string ID => monsterName;
}
