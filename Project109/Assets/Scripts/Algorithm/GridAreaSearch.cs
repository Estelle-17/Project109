using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class GridAreaSearch
{
    public List<Vector2Int> GetGridArea(int mapSize, int targetX, int targetY, int minDist, int maxDist)
    {
        List<Vector2Int> results = new List<Vector2Int>();

        int startX = Mathf.Max(0, targetX - maxDist);
        int endX = Mathf.Min(mapSize - 1, targetX + maxDist);
        int startY = Mathf.Max(0, targetY - maxDist);
        int endY = Mathf.Min(mapSize - 1, targetY + maxDist);

        for(int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                //상하좌우 이동 제한일 때 거리 계산 진행
                int distance = Mathf.Abs(targetX - x) + Mathf.Abs(targetY - y);

                if (distance >= minDist && distance <= maxDist)
                {
                    results.Add(new Vector2Int(x, y));
                }
            }
        }

        return results;
    }
}
