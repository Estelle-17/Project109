using UnityEngine;

[System.Serializable]
public class CellData
{
    public Vector2Int position;

    // 3개의 레이어 정보 (Addressables Key 또는 ID를 저장)
    public string terrainID; // 지형 (예: "Dirt", "Grass", "Wall")
    public string objectID;  // 장애물 (예: "SpikeTrap", "WoodenBox")
    public string eventID;   // 이벤트 (예: "PlayerSpawn", "Enemy_Goblin")

    public CellData(Vector2Int pos)
    {
        position = pos;
        terrainID = "Empty";
        objectID = "Empty";
        eventID = "Empty";
    }
}
