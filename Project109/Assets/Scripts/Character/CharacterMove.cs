using EventInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ì§„ì •???˜ë???'ìºë¦­???´ë™ ?¤í–‰' ë¡œì§ ?´ë˜?¤ì…?ˆë‹¤.
/// ?Œë ˆ?´ì–´?€ ??Enemy) ëª¨ë‘ ???´ë˜?¤ë? ?Œìœ (has-a)?˜ì—¬ ì½”ë£¨???´ë™???˜í–‰?©ë‹ˆ??
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
    /// ê³„ì‚°??ê²½ë¡œ(movePath)ë¥?ê¸°ë°˜?¼ë¡œ ?¤ì œ ?´ë™???˜í–‰?©ë‹ˆ??
    /// </summary>
    public void MoveAlongPath(List<Tile> movePath, Tile destinationTile)
    {
        if (movePath == null || movePath.Count == 0) return;
        this.character.StartCoroutine(StartMoveCoroutine(movePath, destinationTile));
    }

    private IEnumerator StartMoveCoroutine(List<Tile> movePath, Tile destinationTile)
    {
        // 1. ?´ë™ ì§ì „ ?´ë²¤??(IOnBeforeMove)
        Vector2Int fromCoord = new Vector2Int(currentTile.GetCoord().column, currentTile.GetCoord().row);
        Vector2Int toCoord = new Vector2Int(destinationTile.GetCoord().column, destinationTile.GetCoord().row);

        MoveInfo beforeInfo = new MoveInfo(this.character, fromCoord, toCoord, EventFlag.MoveFlag.Normal);
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
            // ëª©í‘œ ?Œì „ ë°??´ë™
            Vector3 targetPos = movePath[currentIndex].transform.position;
            this.character.transform.position = Vector3.MoveTowards(this.character.transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (targetPos - this.character.transform.position != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetPos - this.character.transform.position);
                this.character.transform.rotation = Quaternion.RotateTowards(this.character.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }

            // ?„ì°© ?ì •
            if (Vector3.Distance(this.character.transform.position, targetPos) < 0.1f)
            {
                currentIndex++;
            }

            yield return null;
        }

        // ?´ë™ ?„ë£Œ ???„ì¬ ?€??ê°±ì‹ 
        SetCurrentTile(destinationTile);

        if (this.character.currentState != CharacterState.Die)
        {
            this.character.currentState = CharacterState.Idle;
        }

        // 2. ?´ë™ ì§í›„ ?´ë²¤??(IOnAfterMove)
        Vector2Int currentCoord = new Vector2Int(currentTile.GetCoord().column, currentTile.GetCoord().row);
        MoveInfo afterInfo = new MoveInfo(this.character, currentCoord, currentCoord, EventFlag.MoveFlag.Normal);
        this.character.eventBus?.Invoke<ICharacterEvent>(c => (c as IOnAfterMove)?.OnAfterMove(afterInfo));
    }
}

