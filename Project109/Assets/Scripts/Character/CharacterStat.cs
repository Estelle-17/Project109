using UnityEngine;

[System.Serializable]
public class CharacterStat
{
    public float maxHp { get; set; }
    public float curHp { get; set; }
    public float maxStamina { get; set; }
    public float curStamina { get; set; }
    public float staminaRegen { get; set; }
    public int strength { get; set; }
    public int armor { get; set; }
}
