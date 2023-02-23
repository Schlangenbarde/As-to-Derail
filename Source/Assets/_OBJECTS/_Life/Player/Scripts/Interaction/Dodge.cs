using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dodge : MonoBehaviour
{
    PlayerControls input;
    InputAction inputDodgeAction;
    CharacterController controller;

    private void Awake()
    {
        input = new PlayerControls();
    }
    private void OnEnable()
    {
        inputDodgeAction = input.Player.Dodge;
        inputDodgeAction.Enable();
    }
    private void OnDisable()
    {
        inputDodgeAction = input.Player.Dodge;
        inputDodgeAction.Disable();
    }

    private void Update()
    {
        if (inputDodgeAction.WasPressedThisFrame())
        {
            Vector3 direction = Camera.main.transform.forward;
            Ray raycast = new Ray(Camera.main.transform.position, direction);

            int layerMask = LayerMask.GetMask("Enemy");
            if (Physics.Raycast(raycast, out RaycastHit hit, 1f, layerMask))
            {
                PlayerAnimator animator = GetComponent<PlayerAnimator>();

                animator.StartAnimation(PlayerAnimator.PlayerAnimations.DODGE);

                //Player Moves Trough Enemy
                //Enemy Plays Animation
            }
        }
    }
}
