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
    private Vector2Int coord;
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
        coord = new Vector2Int(column, row);
    }

    public Vector2Int GetCoord()
    {
        return coord;
    }

    public string GetCoordToString()
    {
        return coord.x + ", " + coord.y;
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