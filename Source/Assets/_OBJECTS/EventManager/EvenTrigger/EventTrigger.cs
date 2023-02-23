using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class EventTrigger : MonoBehaviour
{
    public EventManager.Event triggerEvent;
    private void OnTriggerEnter(Collider other)
    {
        EventManager.PlayEvent(triggerEvent);
    }
    public GameObject darkRayPrefab;
    void Test()
    {
        GameObject go = Game.Spawn(darkRayPrefab, transform.position);
        Transform target = go.transform.GetChild(0);
        target.position = Game.Get().Player.transform.position;
        target.parent = Game.Get().Player.transform;
        print("Test");
    }
    private void OnEnable()
    {
        EventManager.PlayerTookDamage_Event += Test;
    }

    private void OnDisable()
    {
        EventManager.PlayerTookDamage_Event -= Test;
    }
}
