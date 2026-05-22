using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class PlayerMove
{
    private CharacterMove characterMove;

    public List<Tile> canMoveTiles;

    public MapManager battleMap;
    public RoutePathfinding routePathfinding;

    public PlayerMove(CharacterMove characterMove)
    {
        this.characterMove = characterMove;
        this.canMoveTiles = new List<Tile>();

        this.battleMap = RunManager.instance?.currentMap;

        if (this.battleMap != null)
        {
            this.routePathfinding = this.battleMap.transform.GetComponent<RoutePathfinding>();
        }
    }

    public void EnableMove()
    {
        CheckCanMoveTiles();

        if (PlayerInputController.instance != null)
        {
            PlayerInputController.instance.OnTouchClickEvent += OnTargetTileClicked;
        }
    }

    public void DisableMove()
    {
        if (PlayerInputController.instance != null)
        {
            PlayerInputController.instance.OnTouchClickEvent -= OnTargetTileClicked;
        }

        ClearCanMoveTiles();
    }

    public void CheckCanMoveTiles()
    {
        canMoveTiles = battleMap.CheckPlayerMoveTiles(characterMove.GetCurrentTile(), characterMove.character.curCharacterStat.maxTilesPerMove);
    }

    /// <summary>
    /// 이전에 검색하여 얻은 플레이어가 움직일 수 있는 타일 정보들을 초기화
    /// </summary>
    public void ClearCanMoveTiles()
    {
        for (int index = 0; index < canMoveTiles.Count; index++)
        {
            canMoveTiles[index].tileState = TileState.Empty;
            canMoveTiles[index].ChangeEffect();
        }
        canMoveTiles.Clear();
    }

    private void OnTargetTileClicked(Vector2 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Tile targetTile = hit.transform.GetComponent<Tile>();

            // 1. 클릭한 오브젝트가 타일이고
            // 2. 그 타일이 이동 가능한(CanMove) 타일이라면
            if (targetTile != null && targetTile.tileState == TileState.CanMove)
            {
                Debug.Log("이동 목표 타일: " + targetTile.GetCoordToString());

                // 경로 탐색 및 실제 이동 명령
                List<Tile> movePath = routePathfinding.TilePathfinding(characterMove.GetCurrentTile(), targetTile, battleMap.GetTileMap());
                characterMove.MoveAlongPath(movePath, targetTile);

                // 이동 명령을 내렸으므로 더 이상 입력을 받지 않도록 해제
                DisableMove();
            }
        }
    }
}
