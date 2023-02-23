using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Dark;

public class Drag : MonoBehaviour
{
    PlayerControls input;
    InputAction inputMouseMoveAction;

    private void OnEnable()
    {
        inputMouseMoveAction = input.Player.Look;
        inputMouseMoveAction.Enable();
    }
    private void OnDisable()
    {
        inputMouseMoveAction = input.Player.Look;
        inputMouseMoveAction.Disable();
    }

    private void Awake()
    {
        input = new PlayerControls();
    }
    public void DragHandler(BaseEventData data)
    {
        Vector2 pos = inputMouseMoveAction.ReadValue<Vector2>();

        Vector2 size = transform.GetComponent<RectTransform>().sizeDelta;
        Vector2 scale = transform.GetComponent<RectTransform>().localScale;

        transform.GetComponent<RectTransform>().anchoredPosition += pos;

        if (Math.IsBigger(Math.ToPositive(transform.GetComponent<RectTransform>().anchoredPosition), size / 2 * (scale - Vector2.one)))
        {
            transform.GetComponent<RectTransform>().anchoredPosition -= pos;
        }
    }
}
