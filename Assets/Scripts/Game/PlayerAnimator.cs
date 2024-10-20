using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public static Action _endgame;

    private void OnEnable()
    {
        PlayerInventory._gameOver += Animate;
    }

    private void OnDisable()
    {
        PlayerInventory._gameOver -= Animate;
    }

    private void Animate()
    {
        Debug.Log("Poof");
        animator.SetTrigger("Endgame");
    }

    private void GameOver()
    {
        if (_endgame != null)
            _endgame();
    }


}
