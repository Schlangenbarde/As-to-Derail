using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    PlayerControls input;
    InputAction pauseInputAction;
    bool pause = false;
    [SerializeField] Look playerLook;

    [SerializeField]
    private List<GameObject> parentObjectsOfScriptsToPause;

    [SerializeField]
    private List<MonoBehaviour> ignoreList;

    private void OnEnable()
    {
        pauseInputAction = input.Player.Pause;
        pauseInputAction.Enable();
    }
    private void OnDisable()
    {
        pauseInputAction = input.Player.Pause;
        pauseInputAction.Disable();
    }

    private void Awake()
    {
        input = new PlayerControls();
    }

    private void Update()
    {
        if (pauseInputAction.WasPressedThisFrame())
        {
            SetTo(!pause);
        }
    }

    public void SetTo(bool state)
    {
        parentObjectsOfScriptsToPause.ForEach(go => SetScriptEnabled(go, !state));
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        pause = state;
    }

    public void SetToButIgnorCursor(bool state)
    {
        parentObjectsOfScriptsToPause.ForEach(go => SetScriptEnabled(go, !state));
        pause = state;
    }

    private bool Ignore(MonoBehaviour component)
    {
        if (component != this)
        {
            foreach (var ignoreComponent in ignoreList)
            {
                if (component == ignoreComponent)
                {
                    return true;
                }
            }
        }
        return false;
    }

    float lastLookValue;
    private void SetScriptEnabled(GameObject _object, bool setTo)
    {
        foreach (var component in _object.GetComponents<MonoBehaviour>())
        {
            if (false == Ignore(component))
            {
                component.enabled = setTo;
            }
        }
        if (setTo == true)
        {
            playerLook.rotationSpeed = lastLookValue;
        }
        else
        {
            lastLookValue = playerLook.rotationSpeed;
            playerLook.rotationSpeed = 0;
        }
    }
}
