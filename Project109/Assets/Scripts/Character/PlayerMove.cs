using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] protected Tile currentTile;
    public List<Tile> canMoveTiles;
    [SerializeField] private Tile currentTagetTile;

    public BattleMapManager battleMap;
    public RoutePathfinding routePathfinding;

    [SerializeField] public List<Tile> movePath;

    private int touchPerformedCount;
    private bool isCameraMove;

    [SerializeField] float playerSpeed = 50f;
    [SerializeField] float turnSpeed = 600f;

    public PlayerInputController playerInputController;

    public event Action OnPlayerStartMove;
    public event Action OnPlayerStopMove;

    private void Start()
    {
        currentTile = new Tile();
        currentTile.SetCoord(5, 1);

        battleMap = GameObject.FindGameObjectWithTag("BattleMap").GetComponent<BattleMapManager>();

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
        canMoveTiles = battleMap.CheckPlayerMoveTiles(currentTile, 10);
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

        touchPerformedCount = 0;
        isCameraMove = false;

        Debug.Log(pos.ToString());
    }

    public void CheckToTargetTile_performed(InputAction.CallbackContext context)
    {
        Vector2 pos = context.ReadValue<Vector2>();

        //마우스를 통해 일정 이상 카메라 이동이 되었다면 이동 금지
        touchPerformedCount++;
        if (touchPerformedCount >= 10)
        {
            isCameraMove = true;
        }

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
        //타일을 선택하지 않았거나 카메라 이동이 이루어졌을 경우 return
        if (currentTagetTile == null || isCameraMove)
            return;

        //이동 가능한 타일일 경우 플레이어 이동
        Debug.Log("LastTileCoord is : " + currentTagetTile.GetCoordToString());
        if(currentTagetTile.tileState == TileState.CanMove)
        {
            //transform.position = currentTagetTile.transform.position;

            //path초기화 후 다시 탐색
            movePath.Clear();
            movePath = routePathfinding.TilePathfinding(currentTile, currentTagetTile, battleMap.GetTileMap());
            StartCoroutine(StartMove());

            //플레이어가 이동을 시작했을 경우 함수 실행
            OnPlayerStartMove?.Invoke();

            currentTile = currentTagetTile;

            CanMoveTileClear();
            playerInputController.OnDisable();
        }
        //playerInputController.OnDisable();
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

        while(currentIndex < movePath.Count)
        {
            //목표로 이동
            transform.position = Vector3.MoveTowards(transform.position, movePath[currentIndex].transform.position, playerSpeed * Time.deltaTime);

            //갈 위치를 기반으로 Quaternion계산 후 회전
            //LookRotation에 0,0,0값이 들어가지 않도록 if문 추가
            if (movePath[currentIndex].transform.position - transform.position != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movePath[currentIndex].transform.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }

            if(Vector3.Distance(transform.position, movePath[currentIndex].gameObject.transform.position) < 0.1f)
            {
                currentIndex++;
            }

            yield return new WaitForFixedUpdate();
        }

        //플레이어가 목적지에 도착했을 경우 함수 실행
        OnPlayerStopMove?.Invoke();

        yield return null;
    }
}
