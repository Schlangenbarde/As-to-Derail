using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketUI : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //Ticket.ticketCollected += EnableBoolInAnimator;
        //TrainSeat.playerGotOnSeat += DisableBoolInAnimator;
    }

    private void OnDisable()
    {
       // Ticket.ticketCollected -= EnableBoolInAnimator;
       // TrainSeat.playerGotOnSeat -= DisableBoolInAnimator;
    }

    public void EnableBoolInAnimator()
    {
        animator.SetBool("hasTicket", true);
    }

    public void DisableBoolInAnimator()
    {
        animator.SetBool("hasTicket", false);
    }
}
