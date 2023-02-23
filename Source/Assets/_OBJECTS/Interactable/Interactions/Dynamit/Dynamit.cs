using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamit : ButtonInteractable
{
    public override void ButtonPressed()
    {
        Destroy(this.gameObject);
    }
}
