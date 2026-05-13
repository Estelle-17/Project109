using EventStructs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 진정한 의미의 '캐릭터 이동 실행' 로직 클래스입니다.
/// 플레이어와 적(Enemy) 모두 이 클래스를 소유(has-a)하여 코루틴 이동을 수행합니다.
/// </summary>
public class CharacterMove
{
    public Character character { private set; get; }

    protected Tile currentTile;
    public float moveSpeed = 50f;
    public float turnSpeed = 600f;

    public CharacterMove(Character character)
    {
        this.character = character;
    }

    public void SetCurrentTile(Tile newTile)
    {
        currentTile = newTile;
    }

    public Tile GetCurrentTile()
    {
        return currentTile;
    }

    /// <summary>
    /// 계산된 경로(movePath)를 기반으로 실제 이동을 수행합니다.
    /// </summary>
    public void MoveAlongPath(List<Tile> movePath, Tile destinationTile)
    {
        if (movePath == null || movePath.Count == 0) return;
        this.character.StartCoroutine(StartMoveCoroutine(movePath, destinationTile));
    }

    private IEnumerator StartMoveCoroutine(List<Tile> movePath, Tile destinationTile)
    {
        // 1. 이동 직전 이벤트 (IOnBeforeMove)
        Vector2Int fromCoord = new Vector2Int(currentTile.GetCoord().column, currentTile.GetCoord().row);
        Vector2Int toCoord = new Vector2Int(destinationTile.GetCoord().column, destinationTile.GetCoord().row);

        MoveInfo beforeInfo = new MoveInfo(this.character, fromCoord, toCoord, MoveFlag.Normal);
        this.character.eventBus?.Invoke<ICharacterEvent>(c => (c as IOnBeforeMove)?.OnBeforeMove(beforeInfo));

        if (beforeInfo.isCanceled)
        {
            yield break;
        }

        if (this.character.currentState != CharacterState.Die)
        {
            this.character.currentState = CharacterState.Move;
        }

        int currentIndex = 0;
        while (currentIndex < movePath.Count)
        {
            // 목표 회전 및 이동
            Vector3 targetPos = movePath[currentIndex].transform.position;
            this.character.transform.position = Vector3.MoveTowards(this.character.transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (targetPos - this.character.transform.position != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetPos - this.character.transform.position);
                this.character.transform.rotation = Quaternion.RotateTowards(this.character.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }

            // 도착 판정
            if (Vector3.Distance(this.character.transform.position, targetPos) < 0.1f)
            {
                currentIndex++;
            }

            yield return null;
        }

        // 이동 완료 후 현재 타일 갱신
        SetCurrentTile(destinationTile);

        if (this.character.currentState != CharacterState.Die)
        {
            this.character.currentState = CharacterState.Idle;
        }

        // 2. 이동 직후 이벤트 (IOnAfterMove)
        Vector2Int currentCoord = new Vector2Int(currentTile.GetCoord().column, currentTile.GetCoord().row);
        MoveInfo afterInfo = new MoveInfo(this.character, currentCoord, currentCoord, MoveFlag.Normal);
        this.character.eventBus?.Invoke<ICharacterEvent>(c => (c as IOnAfterMove)?.OnAfterMove(afterInfo));
    }
}
