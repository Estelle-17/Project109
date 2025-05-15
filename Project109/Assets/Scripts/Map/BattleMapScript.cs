using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BattleMapScript : MonoBehaviour
{
    [SerializeField]
    public Tile[,] map;

    public GameObject prefabTile;
    public int column;
    public int row;
    public int tilePadding;

    /// <summary>
    /// 원하는 맵 크기에 맞게 타일을 생성해주는 함수
    /// </summary>
    public void TileCreate()
    {
        int startX = -(column / 2 * tilePadding) + (tilePadding / 2);
        int startZ = -(row / 2 * tilePadding) + (tilePadding / 2);

        map = new Tile[column, row];
        for (int columnIndex = 0; columnIndex < column; columnIndex++)
        {
            for(int rowIndex = 0; rowIndex < row; rowIndex++)
            {
                Tile tile = GameObject.Instantiate(prefabTile).transform.GetComponent<Tile>();
                tile.transform.localPosition = new Vector3(startX + columnIndex * tilePadding, 0.01f, startZ + rowIndex * tilePadding);
                tile.transform.parent = transform;
                tile.SetCoord(columnIndex, rowIndex);
                map[columnIndex, rowIndex] = tile;
            }
        }

        map[5, 2].tileState = TileState.Obstacle;
        map[4, 2].tileState = TileState.Obstacle;
        map[6, 2].tileState = TileState.Obstacle;
    }

    /// <summary>
    /// 선택된 플레이어가 이동할 수 있는 타일들을 찾아주는 함수
    /// </summary>
    public List<Tile> CheckPlayerMoveTiles(Tile moveStart, int canMoveDistance)
    {
        List<Tile> checkList = new List<Tile>();
        
        Queue<Tile> checkNextTiles = new Queue<Tile>();
        Queue<Tile> checkCurrentTiles = new Queue<Tile>();
        checkCurrentTiles.Enqueue(moveStart);

        for (int currentDistance = 0; currentDistance < canMoveDistance; currentDistance++)
        {
            while (checkCurrentTiles.Count != 0)
            {
                Tile t = checkCurrentTiles.Dequeue();

                int[] dirX = { 0, 0, 1, -1 };
                int[] dirY = { 1, -1, 0, 0 };

                for (int i = 0; i < 4; i++)
                {
                    int x = t.GetCoord().column + dirX[i];
                    int y = t.GetCoord().row + dirY[i];

                    //맵을 넘어가거나 비어있지 않을 경우 제외
                    if (x >= column || y >= row || x < 0 || y < 0 || map[x, y].tileState != TileState.Empty)
                        continue;

                    //플레이어 위치일 경우 제외
                    if (map[x, y].GetCoord().column == moveStart.GetCoord().column && map[x, y].GetCoord().row == moveStart.GetCoord().row)
                        continue;

                    Debug.Log(map[x, y].GetCoordToString() + " OK");
                    map[x, y].tileState = TileState.CanMove;
                    map[x, y].ChangeEffect();

                    checkList.Add(map[x, y]);
                    checkNextTiles.Enqueue(map[x, y]);
                }
            }

            checkCurrentTiles = new Queue<Tile>(checkNextTiles);
            Debug.Log("현재 계산해야 할 타일 갯수 : " + checkCurrentTiles.Count);
            checkNextTiles.Clear();
        }

        return checkList;
    }

    public bool mapCreateTest;
    private void Update()
    {
        if(mapCreateTest)
        {
            mapCreateTest = false;
            TileCreate();
        }
    }
}
