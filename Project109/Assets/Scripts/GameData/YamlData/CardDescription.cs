using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardDescription", menuName = "Description/CardDescription")]
public class CardDescription : ScriptableObject, IIdentifiable
{
    public string path;
    public string cardName;
    public string description;
    public List<ExtraDescription> extraDescriptions;
    public List<MasteryDescription> masteryDescriptions;

    public string ID => path;
}
