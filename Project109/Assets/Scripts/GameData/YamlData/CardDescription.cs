using UnityEngine;

[CreateAssetMenu(fileName = "CardDescription", menuName = "Description/CardDescription")]
public class CardDescription : ScriptableObject, IIdentifiable
{
    public string path;
    public string cardName;
    public string description;

    public string ID => path;
}
