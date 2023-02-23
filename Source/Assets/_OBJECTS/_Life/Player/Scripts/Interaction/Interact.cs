using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    PlayerControls input;
    InputAction inputInteract;

    public Camera shootCamera;

    [SerializeField]
    Hands hands;

    private void OnEnable()
    {
        inputInteract = input.Player.Interact;
        inputInteract.Enable();
    }
    private void OnDisable()
    {
        inputInteract = input.Player.Interact;
        inputInteract.Disable();
    }
    private void Awake()
    {
        input = new PlayerControls();
    }

    public void Update()
    {
        if (inputInteract.WasPressedThisFrame())
        {
            Vector3 direction = shootCamera.transform.forward;
            Ray raycast = new Ray(shootCamera.transform.position, direction);

            int layerMask = ~LayerMask.GetMask("Player");
            if (Physics.Raycast(raycast, out RaycastHit hit, 10f, layerMask))
            {
                if (hands.ItemInHands != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    hands.Drop(hit, raycast);
                    return;
                }

                hit.transform.GetComponent<Interactable>()?.Interact();
            }
        }
    }

    public void HoldItem(GameObject item)
    {
        hands.Hold(item);
    }

    public bool PlayerHasItemWithNameInHands(string itemName)
    {
        if (hands.ItemInHands == null) return false;
        return (hands.ItemInHands.GetComponent<Pickupable>().itemName == itemName);
    }

    public void DestroyTheItemThePlayerIsHolding()
    {
        hands.UseItem();
    }
}
