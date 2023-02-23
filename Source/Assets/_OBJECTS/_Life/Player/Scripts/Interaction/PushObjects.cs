using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushObjects : MonoBehaviour
{
    PlayerControls input;
    InputAction inputInteract;

    Camera shootCamera;

    private void OnEnable()
    {
        inputInteract = input.Player.Push;
        inputInteract.Enable();
    }
    private void OnDisable()
    {
        inputInteract = input.Player.Push;
        inputInteract.Disable();
    }
    private void Awake()
    {
        shootCamera = GetComponent<Interact>().shootCamera;
        input = new PlayerControls();
    }

    Transform currentPushObject = null;

    void CheckInteraction()
    {
        if (inputInteract.WasPressedThisFrame())
        {
            if (currentPushObject != null)
            {
                currentPushObject.GetComponent<Moveable>().EndMoving();
                GetComponent<Movement>().AddMovementSpeedMultiplier(2f);

                currentPushObject = null;
                return;
            }


            Vector3 direction = shootCamera.transform.forward;
            Ray raycast = new Ray(shootCamera.transform.position, direction);

            int layerMask = ~LayerMask.GetMask("Player");
            if (Physics.Raycast(raycast, out RaycastHit hit, 100f, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.TryGetComponent(out Moveable moveScript))
                {
                    if (moveScript.moveable)
                    {
                        StartPush(hit.transform);
                        moveScript.StartMoving(transform);
                    }
                }
            }
        }
    }

    private void Update()
    {
        CheckInteraction();
    }
    void StartPush(Transform pushObject)
    {
        currentPushObject = pushObject;
        GetComponent<Movement>().AddMovementSpeedMultiplier(0.5f);
    }
}
