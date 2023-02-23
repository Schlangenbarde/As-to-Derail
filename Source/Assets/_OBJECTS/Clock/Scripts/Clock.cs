using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clock : MonoBehaviour
{
    [SerializeField]
    private GameObject hourHand;
    [SerializeField]
    private GameObject minuteHand;
    [SerializeField]
    private GameObject secondHand;
    void Update()
    {
        hourHand.transform.localEulerAngles = new Vector3(0, 0, System.DateTime.Now.Hour * 30 + System.DateTime.Now.Minute / 6);
        minuteHand.transform.localEulerAngles = new Vector3(0, 0, System.DateTime.Now.Minute * 6);
        secondHand.transform.localEulerAngles = new Vector3(0, 0, System.DateTime.Now.Second * 6);
    }
}
