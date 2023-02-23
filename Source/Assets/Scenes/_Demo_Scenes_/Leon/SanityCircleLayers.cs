using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityCircleLayers : MonoBehaviour
{
    bool islayerisInside;

    public bool IsPlayerInside => islayerisInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Movement>())
        {
            islayerisInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Movement>())
        {
            islayerisInside = false;
        }
    }
}
