using UnityEngine;

public enum CellType
{
    Floor,          //바닥
    Wall,           //벽
    PlayerSpawn,    //플레이어 스폰 가능 지점
    EnemySpawn,     //적 스폰 가능 지점
    NPCSpawn,       //NPC 스폰 가능 지점
    RandomTrapMarker, //  무작위 함정 자리.
    RandomObstacleMarker, // 무작위 장애물 자리.
    FixedTrap,        // 고정된 특정 함정
    FixedObstacle     // 고정된 특정 장애물
}

[System.Serializable]
public class CellData
{
    public Vector2Int position;
    public CellType cellType;

    public string fixedId; // 고정된 함정이나 장애물의 고유 ID
}
