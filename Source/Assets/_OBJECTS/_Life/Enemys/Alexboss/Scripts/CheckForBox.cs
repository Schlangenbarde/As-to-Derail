using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Box") return;
        if (other.ToString() != (other.name + " (UnityEngine.BoxCollider)")) return;
        if (other.GetComponent<Gravity>().letOutVelocityDie == true) return;

        Vector3 impuls = Dark.Physics.Vector3Filter(new Vector3(1,0,1), transform.forward * 100 * Time.deltaTime);
        other.GetComponent<Gravity>().GiveImpuls(impuls);
    }
}
