using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private float debugRadius = 5.0f;

    [SerializeField]
    private Color gizmoColor = Color.red;

    [SerializeField]
    public bool isSetEnabled = true;



    private void OnDrawGizmos()
    {
        if (isSetEnabled)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, debugRadius);
        }

    }


}
