using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EventData", menuName = "Events/EventData")]
public class EventData : ScriptableObject
{
    public int eventAppearLevel;
    public List<AppearCondition> eventAppearCondition;
    public string eventName;
    public string eventDescription;
    public List<Choice_RelicAndCard> choices;
}
