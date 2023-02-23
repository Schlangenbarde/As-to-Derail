using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : VelocityD
{
      PlayerControls input;
      InputAction inputMoveAction;
      InputAction inputSprintAction;
      CharacterController controller;
      Animator animator;
      public CharacterController GetController => controller;

      [HideInInspector]
      public bool acceptInput = true;

      [SerializeField]
      private float movementSpeed = 5;

      [SerializeField]
      private float SprintMultiplier = 1.5f;

      [SerializeField]
      GameObject cameraParent;

      float currentMovementSpeed;

      private float speedMultiplier = 1;

      private Vector3 extraVelocity;

      [SerializeField]
      float maxFallSpeed = 2.5f;

      [SerializeField]
      float playerHeight;

      public void AddMovementSpeedMultiplier(float speed)
      {
            speedMultiplier *= speed;
      }

      private void OnEnable()
      {
            inputSprintAction = input.Player.Sprint;
            inputSprintAction.Enable();
            inputMoveAction = input.Player.Move;
            inputMoveAction.Enable();

            animator = GetComponent<Animator>();
      }
      private void OnDisable()
      {
            inputSprintAction = input.Player.Sprint;
            inputSprintAction.Disable();
            inputMoveAction = input.Player.Move;
            inputMoveAction.Disable();
      }

      public void SetHeight(float newHeight)
      {
            controller.height = newHeight;
            groundPoint.position = transform.position - new Vector3(0, controller.height / 2, 0);

      }

      private void Awake()
      {
            currentMovementSpeed = movementSpeed;
            input = new PlayerControls();
            controller = GetComponent<CharacterController>();
            SetMaxFallSpeed(maxFallSpeed);
            SetHeight(playerHeight);
      }

      private void Update()
      {
            UpdateOnGround();

            if (inputSprintAction.WasPressedThisFrame()) currentMovementSpeed = movementSpeed * SprintMultiplier;
            if (inputSprintAction.WasReleasedThisFrame()) currentMovementSpeed = movementSpeed;

            Vector2 input_move_direction = inputMoveAction.ReadValue<Vector2>();
            Vector3 direction = transform.forward * input_move_direction.y + transform.right * input_move_direction.x;

            Vector3 vel = Vector3.zero;
            vel += direction * currentMovementSpeed * speedMultiplier * Time.deltaTime;
            vel += GetGroundVelocity();
            vel += GetJumpVelocity();

            CameraFeedback(vel);
            Move(vel);
      }

      Vector3 GetJumpVelocity()
      {
            return extraVelocity * Time.deltaTime;
      }

      public void AddJumpVelocity(float plusVelocity)
      {
            fallVelocity += plusVelocity;
      }

      public bool IsOnGround()
      {
            return onGround;
      }

      protected override void Move(Vector3 vel)
      {
            base.Move(vel);
            controller.Move(velocity);
      }

      public void CameraFeedback(Vector3 vel)
      {
            vel.x *= 10;
            vel.z *= 10;

            vel = Dark.Math.ToPositive(vel);

            if (!inputMoveAction.WasPressedThisFrame())
            {
                  animator.SetFloat("speed", 0f, 0.1f, Time.deltaTime);
                  // did you forget an "return;" ?
            }


            if (vel.x >= vel.z)
            {
                  animator.SetFloat("speed", vel.x, 0.5f, Time.deltaTime);
            }
            else
            {
                  animator.SetFloat("speed", vel.z, 0.5f, Time.deltaTime);
            }
            //cam.GetComponent<Camera>().fieldOfView = cameraFov*1.5f;

      }

}
