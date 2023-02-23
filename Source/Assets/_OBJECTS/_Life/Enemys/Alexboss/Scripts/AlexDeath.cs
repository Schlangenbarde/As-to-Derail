using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexDeath : MonoBehaviour
{
    public DoorInteraction door;

    List<Collider> colliders = new List<Collider>();

    private void Update()
    {
        if (door.doorIsOpen == false)
        {
            foreach (var collider in colliders)
            {
                if (collider.transform.TryGetComponent(out AlexBoss alex))
                {
                    StartCoroutine(GameEnd());
                }
            }
        }

    }

    bool dieing = false;
    IEnumerator GameEnd()
    {
        if (dieing == false)
        {
            yield return new WaitForSeconds(2);
            EventManager.PlayEvent(EventManager.Event.GameEnd);
            dieing = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        foreach (var collider in colliders)
        {
            if (collider == other)
            {
                return;
            }
        }

        colliders.Add(other);
    }
}
