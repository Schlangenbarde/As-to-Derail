using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Dark;

public class MapUI : MonoBehaviour
{
    public string currentStation;
    public string destinationStation;

    public Transform playerDot;
    public Transform destinationDot;

    public Transform stations;

    [SerializeField]
    RectTransform mapUI;

    PlayerControls input;
    InputAction zoomInputAction;

    [SerializeField]
    float zoomPower = 0.1f;

    private void Awake()
    {
        input = new PlayerControls();
    }

    private void OnEnable()
    {
        zoomInputAction = input.UI.MouseZoom;
        zoomInputAction.Enable();
    }

    private void OnDisable()
    {
        zoomInputAction = input.UI.MouseZoom;
        zoomInputAction.Disable();
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(currentStation) && !string.IsNullOrEmpty(destinationStation))
        {
            SetNewRoute(playerDot, currentStation, destinationDot, destinationStation);
        }

        if (zoomInputAction.WasPressedThisFrame())
        {
            Zoom(zoomInputAction.ReadValue<float>());
        }
    }

    void Zoom(float zoom)
    {
        if (zoom > 0)
        {
            ZoomIn();
        }
        if (zoom < 0)
        {
            ZoomOut();
        }

        if (mapUI.localScale.x < 1)
        {
            mapUI.localScale = Vector3.one;
        }
    }

    void ZoomIn()
    {
        Vector2 newScale = mapUI.localScale + Vector3.one * zoomPower;
        mapUI.localScale = newScale;

        if (mapUI.localScale.x < 1)
        {
            mapUI.localScale = Vector3.one;
        }
    }

    void ZoomOut()
    {
        Vector2 newScale = mapUI.localScale - Vector3.one * zoomPower;
        mapUI.localScale = newScale;

        if (mapUI.localScale.x < 1)
        {
            mapUI.localScale = Vector3.one;
        }

        Vector2 size = mapUI.GetComponent<RectTransform>().sizeDelta;
        Vector2 scale = mapUI.GetComponent<RectTransform>().localScale;
        Vector2 max = size / 2 * (scale - Vector2.one);
        Vector2 posTemp = Math.ToPositive(mapUI.anchoredPosition);

        if (Math.IsBigger(posTemp, max))
        {
            if (posTemp.x > max.x)
            {
                if (mapUI.anchoredPosition.x >= 0)
                {
                    mapUI.anchoredPosition = new Vector2(max.x, mapUI.anchoredPosition.y);
                }
                else
                {
                    mapUI.anchoredPosition = new Vector2(-max.x, mapUI.anchoredPosition.y);
                }
                
            }
            if (posTemp.y > max.y)
            {
                if (mapUI.anchoredPosition.y >= 0)
                {
                    mapUI.anchoredPosition = new Vector2(mapUI.anchoredPosition.x, max.y);
                }
                else
                {
                    mapUI.anchoredPosition = new Vector2(mapUI.anchoredPosition.x, -max.y);
                }
            }
        }
    }

    public void SetDot(Transform tf, string newStation)
    {
        foreach (Transform child in stations)
        {
            if (child.name == newStation)
            {
                tf.SetParent(child, false);
                tf.localPosition = Vector3.zero;
                return;
            }
        }
    }

    public void SetNewRoute(Transform tfA, string newStationA, Transform tfB, string newStationB)
    {
        SetDot(tfA, newStationA);
        SetDot(tfB, newStationB);
    }

    public void SetPlayer(string newStation)
    {
        SetDot(playerDot, newStation);
    }

    public void SetDestination(string newStation)
    {
        SetDot(destinationDot, newStation);
    }
}
