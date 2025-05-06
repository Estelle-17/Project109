using UnityEngine;

public enum TileState
{
    Empty,
    Full,
    CanMove,
    Obstacle
}

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Coord coord;
    public TileState tileState;
    public GameObject canMoveAreaColor;

    public void SetCoord(int column, int row)
    {
        coord = new Coord(column, row);
    }

    public Coord GetCoord()
    {
        return coord;
    }

    /// <summary>
    /// 현재 타일 상태에 따른 색 설정
    /// </summary>
    public void ChangeEffect()
    {
        if(tileState == TileState.CanMove)
        {
            canMoveAreaColor.SetActive(true);
        }
        else
        {
            canMoveAreaColor.SetActive(false);
        }
    }
}

[System.Serializable]
public class Coord
{
    public int column;
    public int row;

    public Coord(int column, int row)
    {
        this.column = column;
        this.row = row;
    }
}