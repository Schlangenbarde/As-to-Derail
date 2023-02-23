using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class Jump : MonoBehaviour
{
    Movement movement;

    PlayerControls input;
    InputAction inputJumpAction;

    public float jumpHeight = 2f;
    bool inJump = false;

    private void OnEnable()
    {
        inputJumpAction = input.Player.Jump;
        inputJumpAction.Enable();
    }
    private void OnDisable()
    {
        inputJumpAction = input.Player.Jump;
        inputJumpAction.Disable();
    }

    private void Awake()
    {
        movement = GetComponent<Movement>();
        input = new PlayerControls();
    }

    public void Update()
    {
        if (inputJumpAction.WasPressedThisFrame() && movement.IsOnGround())
        {
            StartJump();
        }

        if (false == movement.IsOnGround() && movement.GetVelocity.y < 0 && inJump)
        {
            EndJump();
        }
    }
    public void StartJump()
    {
        float jumpVelocity = Mathf.Sqrt(jumpHeight * -2 * Game.Get().WorldGravity);

        movement.AddJumpVelocity(jumpVelocity);
        movement.AddMovementSpeedMultiplier(0.9f);
        inJump = true;
    }
    public void EndJump()
    {
        float jumpVelocity = Mathf.Sqrt(jumpHeight * -2 * Game.Get().WorldGravity);

        movement.AddJumpVelocity(-jumpVelocity);
        movement.AddMovementSpeedMultiplier(1f/0.9f);
        inJump = false;
    }
}

