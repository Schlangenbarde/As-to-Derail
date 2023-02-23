using UnityEngine;
using System.Collections.Generic;
using System;

public class CheckForCreatures : MonoBehaviour
{
    Dictionary<GameObject, int> creatures = new Dictionary<GameObject, int>();

    public static Action<float> monsterInside;
    public static Action<float> monsterOutside;



    private void OnEnable()
    {
        CheckIfSeenInCamera.monsterIsVisable += CheckIn;
        CheckIfSeenInCamera.monsterIsNotVisable += CheckOut;
    }

    private void OnDisable()
    {
        CheckIfSeenInCamera.monsterIsNotVisable -= CheckOut;
        CheckIfSeenInCamera.monsterIsVisable -= CheckIn;
    }

    private void CheckIn(GameObject targetObj)
    {

        if (!creatures.ContainsKey(targetObj)) // Check if the GameObject already exist in the dictonary
        {
            // Check if something is infront of the GameObject
            if (Physics.Raycast(transform.position, (targetObj.transform.position - Camera.main.transform.position).normalized, out RaycastHit hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
            {
                // Check if the hit is the same as the targetObj
                if (hit.collider.gameObject == targetObj)
                {
                    creatures.Add(targetObj, CheckForDistance(hit.distance) + CheckForAngle(targetObj.transform.position - Camera.main.transform.position));
                    ApplySanityMultiplierByDistance(creatures[targetObj]);
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
                    if (creatures[targetObj] != CheckForDistance(hit.distance) + CheckForAngle(targetObj.transform.position - Camera.main.transform.position))
                    {
                        RemoveCurrentSanityAmount(creatures[targetObj]);

                        creatures[targetObj] = CheckForDistance(hit.distance) + CheckForAngle(targetObj.transform.position - Camera.main.transform.position);

                        ApplySanityMultiplierByDistance(creatures[targetObj]);
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
                monsterInside?.Invoke(3.5f); 
                break;

            case 3:
                monsterInside?.Invoke(4);
                break;

            case 4:
                monsterInside?.Invoke(5);
                break;

            case 5:
                monsterInside?.Invoke(6.5f);
                break;

            case 6:
                monsterInside?.Invoke(10); // short distance mid screen
                break;

            default:
                break;
        }
    }

    private void CheckOut(GameObject targetObj)
    {
        if (creatures.ContainsKey(targetObj))
        {
            RemoveCurrentSanityAmount(creatures[targetObj]);
            creatures.Remove(targetObj);
        }
    }

    private void RemoveCurrentSanityAmount(int currentAmount)
    {
        switch (currentAmount)
        {     
            // long Distance not mid screen
            case 2:
                monsterOutside?.Invoke(3.5f);
                break;

            case 3:
                monsterOutside?.Invoke(4);
                break;

            case 4:
                monsterOutside?.Invoke(5);
                break;

            case 5:
                monsterOutside?.Invoke(6.5f);
                break;

            case 6:
                monsterOutside?.Invoke(10);// short distance mid screen
                break;

            default:
                break;
        }
    }


}
