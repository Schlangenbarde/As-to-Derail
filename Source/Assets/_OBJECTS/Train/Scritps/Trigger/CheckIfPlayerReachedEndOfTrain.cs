using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayerReachedEndOfTrain : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<BaseTrain>().reachedEndOfTrain = true;
        }
    }
}
