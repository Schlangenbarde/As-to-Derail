using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    PlayerControls input;
    InputAction inputLookAction;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private bool lockCamera = true;

    public float rotationSpeed = 5;

    float xRotation = 0f;

    [SerializeField]
    Tooltips tooltips;
    RaycastHit hit;

    [SerializeField]
    LayerMask layer;

    private void OnEnable()
    {
        inputLookAction = input.Player.Look;
        inputLookAction.Enable();
    }
    private void OnDisable()
    {
        inputLookAction = input.Player.Look;
        inputLookAction.Disable();
    }

    private void Awake()
    {
        if (lockCamera)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        input = new PlayerControls();
    }
    [HideInInspector] public bool active = true;

    void Update()
    {
        if (active)
        {
            Vector3 playerRotation = new Vector3(0, inputLookAction.ReadValue<Vector2>().x, 0);
            playerRotation *= rotationSpeed;
            transform.Rotate(playerRotation);

            xRotation -= inputLookAction.ReadValue<Vector2>().y * rotationSpeed;
            xRotation = Mathf.Clamp(xRotation, -90f, 70f);
        }

        playerCamera.transform.localEulerAngles = new Vector3(xRotation, 0, 0);

        if (active)
        {
            //Tooltip Raycast

            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 5.0f, layer))
            {
                //tooltips.changeTip(0);   

                if (hit.collider.gameObject.GetComponent<Dialog>())
                {
                    tooltips.changeTip(1);
                    tooltips.hit(true);
                }
                else if (hit.collider.gameObject.GetComponent<TrainSeat>())
                {
                    tooltips.changeTip(2);
                    tooltips.hit(true);
                }
                else if (hit.collider.gameObject.GetComponent<Ticket>())
                {
                    tooltips.changeTip(3);
                    tooltips.hit(true);
                }
                else if (hit.collider.gameObject.GetComponent<Interactable>())
                {
                    tooltips.changeTip(0);
                    tooltips.hit(true);
                }
                else
                {
                    tooltips.hit(false);
                }
            }
            else
            {
                tooltips.hit(false);
            }
        }
    }
}
