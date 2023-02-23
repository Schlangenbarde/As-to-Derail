using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiesUI : MonoBehaviour
{
    Animator animator;
    bool test;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        BaseSanity.playerIsGoingToDie += StartAnimation;
        BaseSanity.playerDied += SwitchBool;
    }

    private void OnDisable()
    {
        BaseSanity.playerIsGoingToDie -= StartAnimation;
        BaseSanity.playerDied -= SwitchBool;
    }
    private void StartAnimation()
    {
        animator.SetBool("Start", true);
    }

    void SwitchBool()
    {
        animator.SetBool("Start", false);

    }

}
