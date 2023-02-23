using System;
using UnityEngine;

public class Ticket : ButtonInteractable
{
    [SerializeField]
    private TrainStation station;
    public static Action ticketCollected;

    public override void ButtonPressed()
    {
        station.isLevelDesignDone = true;
        ticketCollected?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Movement>())
        {
            station.isLevelDesignDone = true;
            ticketCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
