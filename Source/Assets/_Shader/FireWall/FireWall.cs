using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().material.SetVector("_Tiling", new Vector2(transform.localScale.z, transform.localScale.y));
    }

    private void OnTriggerEnter(Collider other)
    {
        EventManager.PlayEvent(EventManager.Event.PlayerBurning);
    }

    private void OnTriggerExit(Collider other)
    {
        EventManager.PlayEvent(EventManager.Event.PlayerStopBurning);
    }
}
