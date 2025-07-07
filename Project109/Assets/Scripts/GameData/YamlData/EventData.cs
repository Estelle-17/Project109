using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EventData", menuName = "Events/EventData")]
public class EventData : ScriptableObject, IIdentifiable
{
    public int eventAppearLevel;
    public List<AppearCondition> eventAppearCondition;
    public string eventName;
    public string eventDescription;
    public List<Choice_RelicAndCard> choices;

    public string ID => eventName;
}
