using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStationTrigger : MonoBehaviour
{
    [SerializeField]
    TrainStation trainStation;

    [SerializeField]
    bool platformIsOnLeftSide;

    private void OnTriggerEnter(Collider target)
    {

        if (target.gameObject.GetComponent<BaseTrain>())
        {
            trainStation.SetDirection(true);
            target.gameObject.GetComponent<BaseTrain>().SetPlatformSide(platformIsOnLeftSide);
        }
    }


}
