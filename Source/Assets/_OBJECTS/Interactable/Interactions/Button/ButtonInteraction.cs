using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : Interaction
{
    [SerializeField]
    private ButtonInteractable interactable;

    public override void Do()
    {
        //Play Animation...
        interactable.ButtonPressed();
    }
}
