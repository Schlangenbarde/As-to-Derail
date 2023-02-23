using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : DoorInteraction
{
    public Transform obstacle;

    Animator animator;

    [SerializeField]
    bool isOpenOnStart;

    private void Awake()
    {
        if (isOpenOnStart) Open();
        animator = GetComponent<Animator>();
        
    }
    public override void Open()
    {
        //obstacle.Translate(obstacle.forward * obstacle.localEulerAngles.z * 2, Space.World);
        animator.SetBool("isOpen", true);
    }

    public override void Close()
    {
        //obstacle.Translate(-obstacle.forward * obstacle.localScale.z * 2, Space.World);
        animator.SetBool("isOpen", false);
    }
}
