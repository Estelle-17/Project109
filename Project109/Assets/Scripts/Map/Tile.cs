using UnityEngine;

public enum TileState
{
    Empty,
    Full,
    CanMove,
    Trap,
    Obstacle
}

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Coord coord;
    public Vector2Int position; // 그리드 좌표 (x, y)
    public TileState tileState;
    public GameObject canMoveAreaTextureObject;

    private void Start()
    {
        if (transform.childCount > 0)
        {
            canMoveAreaTextureObject = transform.GetChild(0).gameObject;
            if (canMoveAreaTextureObject != null)
            {
                canMoveAreaTextureObject.SetActive(false);
            }
        }
    }

    public void SetCoord(int column, int row)
    {
        coord = new Coord(column, row);
    }

    public Coord GetCoord()
    {
        return coord;
    }

    public string GetCoordToString()
    {
        return coord.column + ", " + coord.row;
    }

    /// <summary>
    /// 현재 타일 상태에 따른 색 설정
    /// </summary>
    public void ChangeEffect()
    {
        if (canMoveAreaTextureObject != null)
        {
            if (tileState == TileState.CanMove)
            {
                canMoveAreaTextureObject.SetActive(true);
            }
            else
            {
                canMoveAreaTextureObject.SetActive(false);
            }
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