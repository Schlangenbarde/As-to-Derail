using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSwitch : ButtonInteractable
{
    [SerializeField]
    private GameObject[] lamps;
    [SerializeField]
    private Waypoint[] waypoints;
    [SerializeField]
    private GameObject button;
    private bool status = true;

    public override void ButtonPressed()
    {
        status = !status;
        foreach (GameObject lamp in lamps)
        {
            lamp.GetComponent<Light>().enabled = status;
            //Debug.Log("Lamp was switched");
        }
        foreach (Waypoint wayPoint in waypoints)
        {
            wayPoint.isSetEnabled = status;
        }
        button.GetComponent<Light>().enabled = status;
    }
}
