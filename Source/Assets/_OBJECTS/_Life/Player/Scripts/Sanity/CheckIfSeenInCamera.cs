using System;
using UnityEngine;

public class CheckIfSeenInCamera : MonoBehaviour
{
    [SerializeField, Tooltip("Decide if this Object is a Monster which can increase the negative Sanity Multiplicator")]
    bool monster;

    public static Action<GameObject> monsterIsVisable;
    public static Action<GameObject> monsterIsNotVisable;

    public static Action<GameObject> goodThingsIsVisable;
    public static Action<GameObject> goodThingsIsNotVisable;



    bool IsVis(Camera cam)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        var point = transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0 )
            {
                return false;
            }

        }
        return true;
    }

    private void Update()
    {
        if (IsVis(Camera.main) && monster)
        {
            monsterIsVisable?.Invoke(gameObject);
        }
        else if (!IsVis(Camera.main) && monster)
        {
            monsterIsNotVisable?.Invoke(gameObject);
        }

        if (IsVis(Camera.main) && !monster)
        {
            goodThingsIsVisable?.Invoke(gameObject);
        }
        else if (!IsVis(Camera.main) && !monster)
        {
            goodThingsIsNotVisable?.Invoke(gameObject);
        }
    }
}
