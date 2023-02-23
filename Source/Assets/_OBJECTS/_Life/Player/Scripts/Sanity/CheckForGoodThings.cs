using UnityEngine;
using System.Collections.Generic;
using System;

public class CheckForGoodThings : MonoBehaviour
{
    Dictionary<GameObject, int> nonCreatures = new Dictionary<GameObject, int>();

    public static Action<float> goodThingIsInBackground;
    public static Action<float> noGoodThing;

    private void OnEnable()
    {
        CheckIfSeenInCamera.goodThingsIsVisable += CheckIn;
        CheckIfSeenInCamera.goodThingsIsNotVisable += CheckOut;
    }

    private void OnDisable()
    {
        CheckIfSeenInCamera.goodThingsIsVisable -= CheckIn;
        CheckIfSeenInCamera.goodThingsIsNotVisable -= CheckOut;
    }

    private void CheckIn(GameObject targetObj)
    {
        if (!nonCreatures.ContainsKey(targetObj)) // Check if the GameObject already exist in the dictonary
        {
            // Check if something is infront of the GameObject
            if (Physics.Raycast(transform.position, (targetObj.transform.position - Camera.main.transform.position).normalized, out RaycastHit hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
            {
                // Check if the hit is the same as the targetObj
                if (hit.collider.gameObject == targetObj)
                {
                    nonCreatures.Add(targetObj, CheckForDistance(hit.distance) + CheckForAngle(targetObj.transform.position - Camera.main.transform.position));
                    ApplySanityMultiplierByDistance(nonCreatures[targetObj]);
                }

            }
        }
        else
        {
            // Check if something is infront of the GameObject
            if (Physics.Raycast(transform.position, (targetObj.transform.position - Camera.main.transform.position).normalized, out RaycastHit hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
            {
                // Check if the hit is the same as the targetObj
                if (hit.collider.gameObject == targetObj)
                {
                    if (nonCreatures[targetObj] != CheckForDistance(hit.distance) + CheckForAngle(targetObj.transform.position - Camera.main.transform.position))
                    {
                        RemoveCurrentSanityAmount(nonCreatures[targetObj]);

                        nonCreatures[targetObj] = CheckForDistance(hit.distance) + CheckForAngle(targetObj.transform.position - Camera.main.transform.position);

                        ApplySanityMultiplierByDistance(nonCreatures[targetObj]);
                    }

                }
                else
                {
                    CheckOut(targetObj);
                    return;
                }

            }
        }
    }

    private int CheckForDistance(float distance)
    {
        if (distance >= 75) // How far is the Object
        {
            return 1;
        }
        else if (distance < 75 && distance >= 25)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    private int CheckForAngle(Vector3 targetDir)
    {
        float horizontalAngle = Vector3.Angle(Camera.main.transform.forward, Camera.main.transform.forward + new Vector3(targetDir.x, 0, targetDir.z).normalized);
        if (horizontalAngle > 17) // how big is the angle
        {
            return 1;
        }
        else if (horizontalAngle < 17 && horizontalAngle > 7)
        {

            return 2;
        }
        else
        {

            return 3;
        }
    }

    private void ApplySanityMultiplierByDistance(int distanceAmount)
    {
        switch (distanceAmount)
        {
             // long Distance not mid screen
            case 2:
                goodThingIsInBackground?.Invoke(.5f);
                break;

            case 3:
                goodThingIsInBackground?.Invoke(1);
                break;

            case 4:
                goodThingIsInBackground?.Invoke(2);
                break;

            case 5:
                goodThingIsInBackground?.Invoke(3.5f);
                break;

            case 6:
                goodThingIsInBackground?.Invoke(7); // short distance mid screen
                break;

            default:
                break;
        }
    }

    private void CheckOut(GameObject targetObj)
    {
        if (nonCreatures.ContainsKey(targetObj))
        {
            RemoveCurrentSanityAmount(nonCreatures[targetObj]);
            nonCreatures.Remove(targetObj);
        }
    }

    private void RemoveCurrentSanityAmount(int currentAmount)
    {
        switch (currentAmount)
        {
            // long Distance not mid screen
            case 2:
                noGoodThing?.Invoke(.5f);
                break;

            case 3:
                noGoodThing?.Invoke(1);
                break;

            case 4:
                noGoodThing?.Invoke(2);
                break;

            case 5:
                noGoodThing?.Invoke(3.5f);
                break;

            case 6:
                noGoodThing?.Invoke(7);// short distance mid screen
                break;

            default:
                break;
        }
    }


}
