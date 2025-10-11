using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EventData", menuName = "Events/EventData")]
public class EventData : ScriptableObject, IIdentifiable
{
    public string eventID;
    public int eventAppearLevel;
    public List<AppearCondition> eventAppearCondition;
    public string eventObjectPath;
    public List<EventStageData> stages;

    public string ID => eventID;
}
