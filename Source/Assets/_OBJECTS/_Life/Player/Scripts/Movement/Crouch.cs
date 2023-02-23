using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class Crouch : MonoBehaviour
{
    Movement movement;

    PlayerControls input;
    InputAction inputCrouchAction;

    private void OnEnable()
    {
        inputCrouchAction = input.Player.Crouch;
        inputCrouchAction.Enable();
    }
    private void OnDisable()
    {
        inputCrouchAction = input.Player.Crouch;
        inputCrouchAction.Disable();
    }

    private void Awake()
    {
        movement = GetComponent<Movement>();
        input = new PlayerControls();
    }

    public void Update()
    {
        CheckCrouchInput();
    }

    public void CheckCrouchInput()
    {
        if (inputCrouchAction.WasPressedThisFrame())
        {
            StartCrouch();
        }

        if (inputCrouchAction.WasReleasedThisFrame())
        {
            EndCrouch();
        }
    }


    public void StartCrouch()
    {
        movement.AddMovementSpeedMultiplier(0.5f);
        movement.SetHeight(1f);
    }

    public void EndCrouch()
    {
        movement.AddMovementSpeedMultiplier(2);
        movement.SetHeight(2f);
    }
}
