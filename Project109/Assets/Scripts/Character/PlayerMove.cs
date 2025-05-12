using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    protected Tile currentTile;
    public List<Tile> canMoveTiles;
    [SerializeField]
    private Tile currentTagetTile;

    public BattleMapScript battleMap;
    public RoutePathfinding routePathfinding;
    [SerializeField]
    public List<Tile> movePath;

    public PlayerInputController playerInputController;

    private void Start()
    {
        currentTile = new Tile();
        currentTile.SetCoord(5, 1);

        battleMap = GameObject.FindGameObjectWithTag("BattleMap").GetComponent<BattleMapScript>();

        if(battleMap != null)
        {
            routePathfinding = battleMap.transform.GetComponent<RoutePathfinding>();
        }

        //InputAction Section
        playerInputController = GetComponent<PlayerInputController>();
        if (playerInputController != null)
        {
            playerInputController.playerInputController.Player.Touch.started += CheckToTargetTile_started;
            playerInputController.playerInputController.Player.Touch.performed += CheckToTargetTile_performed;
            playerInputController.playerInputController.Player.Touch.canceled += CheckToTargetTile_canceled;

            playerInputController.OnEnable();
        }
        else
        {
            Debug.LogWarning("InputController is null!");
        }
    }

    /// <summary>
    /// ���� �÷��̾ �ִ� Ÿ��, ������ �� �ִ� �Ÿ��� ������ ���� ��� �κб��� �̵��� �������� Ȯ��
    /// </summary>
    public void CheckCanMoveTiles()
    {
        canMoveTiles = battleMap.CheckPlayerMoveTiles(currentTile, 5);
    }

    /// <summary>
    /// ������ �˻��Ͽ� ���� �÷��̾ ������ �� �ִ� Ÿ�� �������� �ʱ�ȭ
    /// </summary>
    public void ClearCanMoveTiles()
    {
        for (int index = 0; index < canMoveTiles.Count; index++)
        {
            canMoveTiles[index].tileState = TileState.Empty;
        }
        canMoveTiles.Clear();
    }

    public void CheckToTargetTile_started(InputAction.CallbackContext context)
    {
        Vector2 pos = context.ReadValue<Vector2>();

        Debug.Log(pos.ToString());
    }

    public void CheckToTargetTile_performed(InputAction.CallbackContext context)
    {
        Vector2 pos = context.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(pos);

        Debug.DrawRay(Camera.main.transform.localPosition, ray.direction * 100.0f, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            currentTagetTile = hit.transform.GetComponent<Tile>();
            if(currentTagetTile != null)
            {
                Debug.Log(currentTagetTile.GetCoordToString());
            }
        }
    }

    public void CheckToTargetTile_canceled(InputAction.CallbackContext context)
    {
        //Ÿ���� �������� �ʾ��� ��� return
        if (currentTagetTile == null)
            return;

        //�̵� ������ Ÿ���� ��� �÷��̾� �̵�
        Debug.Log("LastTileCoord is : " + currentTagetTile.GetCoordToString());
        if(currentTagetTile.tileState == TileState.CanMove)
        {
            //transform.position = currentTagetTile.transform.position;

            //path�ʱ�ȭ �� �ٽ� Ž��
            movePath.Clear();
            movePath = routePathfinding.TilePathfinding(currentTile, currentTagetTile, battleMap.map);
            StartCoroutine(StartMove());

            currentTile = currentTagetTile;

            CanMoveTileClear();
        }
        Debug.Log("CheckToTargetTile is Canceled");
    }

    public void CanMoveTileClear()
    {
        for(int index = 0; index < canMoveTiles.Count; index++)
        {
            canMoveTiles[index].tileState = TileState.Empty;
            canMoveTiles[index].ChangeEffect();
        }

        canMoveTiles.Clear();
        currentTagetTile = null;
    }

    IEnumerator StartMove()
    {
        int currentIndex = 0;
        float playerSpeed = 1.0f;

        while(currentIndex < movePath.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePath[currentIndex].gameObject.transform.position, playerSpeed);

            if(Vector3.Distance(transform.position, movePath[currentIndex].gameObject.transform.position) < 0.1f)
            {
                currentIndex++;
            }

            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
