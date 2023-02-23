using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOptions : MonoBehaviour
{

    public void OpenOptions()
    {
        if (Game.Get().Player != null)
        {
            Game.Get().Player.GetComponent<Pause>().SetTo(true);
        }
        
        CanvasGroup c = GetComponent<CanvasGroup>();
        c.interactable = true;
        c.blocksRaycasts = true;
        c.alpha = 1;
    }

    public void CloseOptions()
    {
        if (Game.Get().Player != null)
        {
            Game.Get().Player.GetComponent<Pause>().SetTo(false);
        }

        CanvasGroup c = GetComponent<CanvasGroup>();
        c.interactable = false;
        c.blocksRaycasts = false;
        c.alpha = 0;
    }
}
