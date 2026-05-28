using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice_Data
{
    public string description;
    public string nextStageID;
    public List<Choice_UseItem> useItems;
    public List<Choice_GetItem> getItems;

    public List<CardBase> randomLoseCard;
    public List<RelicData> randomLoseRelic;
}
