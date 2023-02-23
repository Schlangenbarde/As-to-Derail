using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraVFX : MonoBehaviour
{
    [SerializeField]
    Animator cameraAnimator;

    PlayerControls controls;
    InputAction inputAction;
    //float cameraFov;

    float rotFrameNow = 0f;
    float rotFrameLast = 0f;
    float inputRotation = 0f;

    private void Awake()
    {
        //cameraAnimator = GetComponent<Animator>(); 
        // cameraFov = cam.GetComponent<Camera>().fieldOfView;
        controls = new PlayerControls();
    }
    private void OnEnable()
    {
        inputAction = controls.Player.Look;
        inputAction.Enable();
    }
    private void OnDisable()
    {
        inputAction = controls.Player.Look;
        inputAction.Disable();
    }

    public void MovementVFX(Vector3 vel)
    {
        vel.x = System.Math.Abs(vel.x) * 10;
        vel.z = System.Math.Abs(vel.z) * 10;

        if (vel == Vector3.zero)
        {
            cameraAnimator.SetFloat("speed", 0f, 0.1f, Time.deltaTime);

            //cam.GetComponent<Camera>().fieldOfView = cameraFov;
        }
        else
        {
            if (vel.x >= vel.z)
            {
                cameraAnimator.SetFloat("speed", vel.x, 0.5f, Time.deltaTime);
            }
            else
            {
                cameraAnimator.SetFloat("speed", vel.z, 0.5f, Time.deltaTime);
            }
            //cam.GetComponent<Camera>().fieldOfView = cameraFov*1.5f;
        }


        inputRotation = inputAction.ReadValue<Vector2>().x;
        rotFrameNow += inputRotation * Time.deltaTime;
        //rotFrameNow = inputAction.ReadValue<Vector2>().x;
        //rotFrameNow = Game.Get().Player.transform.rotation.y;

        if (rotFrameNow > rotFrameLast)
        {
            cameraAnimator.SetFloat("tilt", 1f * 100 * Mathf.Abs(rotFrameNow - rotFrameLast), 1f, Time.deltaTime);
        }
        else if (rotFrameNow < rotFrameLast)
        {
            cameraAnimator.SetFloat("tilt", -1f * 100 * Mathf.Abs(rotFrameNow - rotFrameLast), 1f, Time.deltaTime);
        }
        else
        {
            cameraAnimator.SetFloat("tilt", 0f, 0.1f, Time.deltaTime);
        }
        rotFrameLast = rotFrameNow;



    }
}
