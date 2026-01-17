using UnityEngine;

[System.Serializable]
public class PlayerStat
{
    public int inGame_Currency_Gold { get; set; }
    public int inGame_Currency_MemorySharp { get; set; }
    public int mapFloorCheck_Start_Length { get; set; }
    public int mapFloorCheck_Length { get; set; }
    public int mapReveal_Random_Count { get; set; }
    public int reward_Card_Count {  get; set; }
    public int reward_Relic_Count { get; set; }
    public int mastery_Choice_Count { get; set; }
}