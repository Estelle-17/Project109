using UnityEngine;
using System.Collections.Generic;

public class CharacterAnimation : MonoBehaviour
{
    Animator animator;
    
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
       
        PlayerMove playerMove = GetComponent<PlayerMove>();
        if (playerMove)
        {
            playerMove.OnPlayerStartMove += StartPlayerMove;
            playerMove.OnPlayerStopMove += StopPlayerMove;
        }
    }

    private void StartPlayerMove()
    {
        animator.SetBool("IsMove", true);
    }

    private void StopPlayerMove()
    {
        animator.SetBool("IsMove", false);
    }
}
