using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    PlayerControls input;
    InputAction inputPlayerEscape;

    private void Awake()
    {
        input = new PlayerControls();
    }

    private void OnEnable()
    {
        inputPlayerEscape = input.UI.CloseUI;
        inputPlayerEscape.Enable();
    }
    private void OnDisable()
    {
        inputPlayerEscape = input.UI.CloseUI;
        inputPlayerEscape.Disable();
    }

    bool open = false;

    public Transform buttons;
    public Transform options;
    public void Open(Transform t)
    {
        CanvasGroup c = t.GetComponent<CanvasGroup>();
        c.alpha = 1;
        c.blocksRaycasts = true;
        c.interactable = true;
    }

    public void Close(Transform t)
    {
        CanvasGroup c = t.GetComponent<CanvasGroup>();
        c.alpha = 0;
        c.blocksRaycasts = false;
        c.interactable = false;
    }

    public void SwitchToOptions()
    {
        Close(buttons);
        Open(options);
    }

    public void Quit()
    {
        Game.Quit();
    }

    public void SwitchToMainMenu()
    {
        Game.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (inputPlayerEscape.WasPressedThisFrame() && Game.Get().ESC_Active)
        {
            if (Game.Get().chatLog.phone.activePanel == null)
            {
                OpenCloseSwitch();
            }
        }
    }

    public void OpenCloseSwitch()
    {
        open = !open;
        switch (open)
        {
            case true:
                Game.Get().Player.GetComponent<Pause>().SetTo(true);
                Open(buttons);
                break;
            case false:
                Game.Get().Player.GetComponent<Pause>().SetTo(false);
                Close(buttons);
                break;
        }
    }
}
