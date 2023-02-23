using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Dark;

public class Phone : MonoBehaviour
{
    [SerializeField]
    Pause pause;

    PlayerControls input;
    InputAction mapInputAction;
    InputAction chatLogInputAction;
    InputAction inventoryInputAction;
    InputAction closeUIInputAction;

    [SerializeField]
    RectTransform background;

    [SerializeField]
    RectTransform buttons;

    Vector2 backgroundSize;

    [SerializeField]
    GameObject map;

    [SerializeField]
    GameObject chatLog;

    [SerializeField]
    GameObject inventory;

    [HideInInspector] public GameObject activePanel;

    bool open = false;

    bool firstTimeMapOpend = true;

    private void OnEnable()
    {
        mapInputAction = input.UI.Map;
        mapInputAction.Enable();

        chatLogInputAction = input.UI.ChatLog;
        chatLogInputAction.Enable();

        inventoryInputAction = input.UI.Inventory;
        inventoryInputAction.Enable();

        closeUIInputAction = input.UI.CloseUI;
        closeUIInputAction.Enable();
    }
    private void OnDisable()
    {
        mapInputAction = input.UI.Map;
        mapInputAction.Disable();

        chatLogInputAction = input.UI.ChatLog;
        chatLogInputAction.Disable();

        inventoryInputAction = input.UI.Inventory;
        inventoryInputAction.Disable();

        closeUIInputAction = input.UI.CloseUI;
        closeUIInputAction.Disable();
    }

    private void Awake()
    {
        input = new PlayerControls();
        backgroundSize = background.sizeDelta;
    }

    private void LateUpdate()
    {
        CheckForInput();
    }

    void CheckForInput()
    {
        //if (mapInputAction.WasPressedThisFrame())
        //{
        //    SwitchToMap();
        //}

        if (chatLogInputAction.WasPressedThisFrame())
        {
            SwitchToChatLog();
        }

        //if (inventoryInputAction.WasPressedThisFrame())
        //{
        //    SwitchToInventory();
        //}

        if (closeUIInputAction.WasPressedThisFrame() && Game.Get().ESC_Active)
        {
            if (activePanel != null)
            {
                Close();
            }
        }
    }

    public void SwitchToMap()
    {
        if (firstTimeMapOpend)
        {
            firstTimeMapOpend = false;
            Game.Get().gameStart.End();
            EventManager.PlayEvent(EventManager.Event.PlayerOpenedMapFirstTime);
        }
        Switch();

        buttons.anchoredPosition = new Vector3(-backgroundSize.x / 4, 0, 0);
        map.GetComponent<RectTransform>().anchoredPosition = new Vector3(-backgroundSize.x / 4, 0, 0);
        background.sizeDelta = new Vector2(backgroundSize.x*1.5f, backgroundSize.y);
        map.SetActive(true);
        activePanel = map;
    }

    public void SwitchToChatLog()
    {
        Switch();
        chatLog.SetActive(true);
        activePanel = chatLog;
    }
    public void SwitchToInventory()
    {
        Switch();
        inventory.SetActive(true);
        activePanel = inventory;
    }

    public void Close()
    {
        Game.Get().PlayerCamera.m_Lens.FieldOfView = 60;
        chatLog.GetComponent<ChatLog>().CleanUpDialog();
        Switch();
        TrySetUI(false);
    }

    void TrySetUI(bool newState)
    {
        if (newState != open)
        {
            SetUI(newState);
        }
    }

    void SetUI(bool newState)
    {
        TransformD.SetChildsActive(transform, newState, new List<GameObject> { map, chatLog, inventory });
        pause.SetTo(newState);
        open = newState;
    }

    void Switch()
    {
        background.sizeDelta = new Vector2(backgroundSize.x, backgroundSize.y);
        map.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        buttons.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        TryCloseActivePanel();
        TrySetUI(true);
    }

    void TryCloseActivePanel()
    {
        if (activePanel != null)
        {
            activePanel.SetActive(false);
            activePanel = null;
        }
    }
}
