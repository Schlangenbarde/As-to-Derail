using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionZone : MonoBehaviour
{
    Movement playerMovement;
    CharacterController playerController;
    BaseSanity playerSanity;

    Collider _collider;

    bool playerEntered = false;
    bool playerIn = false;
    bool playerExited = false;


    private void Start()
    {
        _collider = GetComponent<Collider>();
        playerMovement = Game.Get().Player.GetComponent<Movement>();
        playerController = playerMovement.GetController;
        playerSanity = Game.Get().Player.GetComponent<BaseSanity>();
    }
    private void Enter()
    {
        playerMovement.AddMovementSpeedMultiplier(0.2f);
        playerSanity.IncreaseSanityNegativMuliplicator(10f);
    }

    private void Exit()
    {
        playerMovement.AddMovementSpeedMultiplier(5f);
        playerSanity.DecreaseSanityNegativMultiplicator(10f);
    }

    private void Update()
    {
        if(_collider.bounds.Intersects(playerController.bounds) && !playerIn)
        {
            playerEntered = true;
        }

        if (!_collider.bounds.Intersects(playerController.bounds) && playerIn)
        {
            playerExited = true;
        }

        if (playerEntered)
        {
            playerEntered = false;
            playerIn = true;

            Enter();
        }

        if (playerExited)
        {
            playerExited = false;
            playerIn = false;

            Exit();
        }
    }
}
