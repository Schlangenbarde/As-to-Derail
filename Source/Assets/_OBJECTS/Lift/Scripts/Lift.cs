using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Lift : ButtonInteractable
{
    [SerializeField, Tooltip("Select the higher floor")]
    private Transform upperFloor;
    
    [SerializeField, Tooltip("Select the lower floor")]
    private Transform lowerFloor;

    [SerializeField, Tooltip("Change the speed of the lift")]
    private float speed;

    private bool isOnLowerFloor = true;
    private bool isMoving;

    private CharacterController characterController;
    private GameObject player;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        if (isMoving)
        {
            if (isOnLowerFloor)
            {
                MoveTo(upperFloor);
                if (player)
                {
                    player.GetComponent<CharacterController>().enabled = false;
                    if (CheckForEnd(upperFloor))
                    {
                        isMoving = false;
                        isOnLowerFloor = false;
                        if (player) player.GetComponent<CharacterController>().enabled = true;
                    }
                }
                else
                {
                    if (CheckForEnd(upperFloor))
                    {
                        isMoving = false;
                        isOnLowerFloor = false;
                    }
                }
            }
            else
            {
                MoveTo(lowerFloor);
                if (player)
                {
                    player.GetComponent<CharacterController>().enabled = false;
                    if (CheckForEnd(lowerFloor))
                    {
                        isMoving = false;
                        isOnLowerFloor = true;
                        if (player) player.GetComponent<CharacterController>().enabled = true;
                    }
                }
                else
                {
                    if (CheckForEnd(lowerFloor))
                    {
                        isMoving = false;
                        isOnLowerFloor = true;
                    }
                }
            }
        }
    }

    public override void ButtonPressed()
    {
        if (!isMoving) ActivateLift();
    }

    private void ActivateLift()
    {
        isMoving = true;
    }

    private void MoveTo(Transform targetFloor)
    {
        float x = targetFloor.position.y - transform.position.y;
        characterController.Move(new Vector3(0, x, 0).normalized * speed * Time.deltaTime);
    }

    bool CheckForEnd(Transform targetFloor)
    {
        float maxDistance = Vector3.Distance(new Vector3(0, targetFloor.position.y, 0), new Vector3(0, transform.position.y, 0));
        if (maxDistance < 1)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
            player = null;
        }
    }
}
