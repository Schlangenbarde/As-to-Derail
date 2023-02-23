using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlexUI : MonoBehaviour
{
    public GameObject UI;
    public TextMeshProUGUI textL;
    public Transform lightsParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == Game.Get().Player.transform)
        {
            UI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == Game.Get().Player.transform)
        {
            UI.SetActive(false);
        }
    }

    int GetActiveChildCount()
    {
        int activeCount = 0;

        foreach (Transform child in lightsParent)
        {
            if (child.GetComponent<Waypoint>().isSetEnabled)
            {
                activeCount++;
            }
        }
        return activeCount;
    }
    private void Update()
    {
        textL.text = GetActiveChildCount() + "/" + lightsParent.childCount;
    }
}
