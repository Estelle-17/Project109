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

            playerInputController.OnDisable();
        }
        else
        {
            Debug.LogWarning("InputController is null!");
        }
    }

    /// <summary>
    /// 현재 플레이어가 있는 타일, 움직일 수 있는 거리를 가지고 맵의 어느 부분까지 이동이 가능한지 확인
    /// </summary>
    public void CheckCanMoveTiles()
    {
        canMoveTiles = battleMap.CheckPlayerMoveTiles(currentTile, 5);
        playerInputController.OnEnable();
    }

    /// <summary>
    /// 이전에 검색하여 얻은 플레이어가 움직일 수 있는 타일 정보들을 초기화
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
        //타일을 선택하지 않았을 경우 return
        if (currentTagetTile == null)
            return;

        //이동 가능한 타일일 경우 플레이어 이동
        Debug.Log("LastTileCoord is : " + currentTagetTile.GetCoordToString());
        if(currentTagetTile.tileState == TileState.CanMove)
        {
            //transform.position = currentTagetTile.transform.position;

            //path초기화 후 다시 탐색
            movePath.Clear();
            movePath = routePathfinding.TilePathfinding(currentTile, currentTagetTile, battleMap.map);
            StartCoroutine(StartMove());

            currentTile = currentTagetTile;

            CanMoveTileClear();
        }
        playerInputController.OnDisable();
        //Debug.Log("CheckToTargetTile is Canceled");
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
