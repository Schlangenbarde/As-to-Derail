using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrainDoors : ButtonInteractable
{
    [SerializeField, Tooltip("Decide if the train door's are open or closed at start. true is Open")]
    private bool isOpenOnStart;

    Animator animator;

    bool isOpen;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        isOpen = isOpenOnStart;
        if (isOpen)
        {
            Open();
        }
    }

    public void Open()
    {
        animator.SetBool("DoorOpen", true);
        isOpen = true;
    }

    public void Close()
    {
        animator.SetBool("DoorOpen", false);
        isOpen = false;
    }

    public override void ButtonPressed()
    {
        if (isOpen) Close();
        else Open();
        
    }



}


