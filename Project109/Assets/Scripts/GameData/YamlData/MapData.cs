using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MapData", menuName = "Map/MapData")]
public class MapData : ScriptableObject, IIdentifiable
{
    public string mapName;
    public string dataPath;

    [Header("Base Layout (0: Walkable, 1: Wall)")]
    public List<string> baseLayout;

    [Header("Base Layout (N: NPC, O: Obstacle, etc.)")]
    public List<MapVariation> variations;

    public string ID => mapName;
}
