using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BattleMapScript : MonoBehaviour
{
    [SerializeField]
    public Tile[,] map;

    public Tile prefabTile;
    public int column;
    public int row;
    public int tilePadding;

    /// <summary>
    /// ���ϴ� �� ũ�⿡ �°� Ÿ���� �������ִ� �Լ�
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
                Tile tile = GameObject.Instantiate(prefabTile);
                tile.transform.localPosition = new Vector3(startX + columnIndex * tilePadding, 0, startZ + rowIndex * tilePadding);
                tile.SetCoord(columnIndex, rowIndex);
                map[columnIndex, rowIndex] = tile;
            }
        }
    }

    /// <summary>
    /// ���õ� �÷��̾ �̵��� �� �ִ� Ÿ�ϵ��� ã���ִ� �Լ�
    /// </summary>
    public void CheckPlayerMoveTile(Tile moveStart, int canMoveDistance)
    {
        List<Tile> checkList = new List<Tile>();
        
        Queue<Tile> checkNextTiles = new Queue<Tile>();
        Queue<Tile> checkCurrentTiles = new Queue<Tile>();
        checkCurrentTiles.Enqueue(moveStart);

        int currentDistance = 1;

        while(checkCurrentTiles.Count != 0)
        {
            Tile t = checkCurrentTiles.Dequeue();

            int[] x = { 0, 0, 1, -1 };
            int[] y = { 1, -1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                if (t.GetCoord().column + x[i] >= column || t.GetCoord().row + y[i] >= row || t.GetCoord().column + x[i] < 0 || t.GetCoord().row + y[i] < 0 ||
                    t.tileState != TileState.Empty)
                    continue;

                checkList.Add(t);
                checkNextTiles.Enqueue(t);
            }

            currentDistance++;
        }

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
