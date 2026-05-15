using UnityEngine;
using System;

[System.Serializable]
public class PlayerStat
{
    public event Action<int> OnGoldChanged;

    private int _inGame_Currency_Gold;
    public int inGame_Currency_Gold 
    { 
        get => _inGame_Currency_Gold; 
        set 
        { 
            _inGame_Currency_Gold = value; 
            OnGoldChanged?.Invoke(_inGame_Currency_Gold); 
        } 
    }
    public event Action<int> OnMemorySharpChanged;

    private int _inGame_Currency_MemorySharp;
    public int inGame_Currency_MemorySharp 
    { 
        get => _inGame_Currency_MemorySharp; 
        set 
        { 
            _inGame_Currency_MemorySharp = value; 
            OnMemorySharpChanged?.Invoke(_inGame_Currency_MemorySharp); 
        } 
    }
    public int mapFloorCheck_Start_Length { get; set; }
    public int mapFloorCheck_Length { get; set; }
    public int mapReveal_Random_Count { get; set; }
    public int reward_Card_Count {  get; set; }
    public int reward_Relic_Count { get; set; }
    public int mastery_Choice_Count { get; set; }
    public int Upgrade_MasteryPoint_Value { get; set; }
}