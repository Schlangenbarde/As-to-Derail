using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : ButtonInteractable
{
    [SerializeField]
    bool doorStartState = false;

    private void Awake()
    {
        doorIsOpen = doorStartState;
    }

    [HideInInspector]
    public bool doorIsOpen = false;
    public override void ButtonPressed()
    {
        switch (doorIsOpen)
        {
            case false:
                Open();
                break;
            case true:
                Close();
                break;
        }
        doorIsOpen = !doorIsOpen;
    }

    public virtual void Open() { }
    public virtual void Close() { }

}
