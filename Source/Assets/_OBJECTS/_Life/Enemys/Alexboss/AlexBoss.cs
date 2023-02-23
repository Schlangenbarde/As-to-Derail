using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlexBoss : Enemy
{
    [SerializeField]
    Transform wayPointsParent;

    List<Waypoint> waypoints = new List<Waypoint>();

    [SerializeField]
    int currentWaypoint = -1;

    NavMeshAgent nav;

    [SerializeField]
    float maxWaitTime;

    float currentWaitTime = 0;

    float hasWaited = 0f;

    [SerializeField]
    float chaseRange = 20f;
    [SerializeField]
    float blindChaseTime = 5f;

    float currentBlindChaseTime = 0f;


    protected override void Setup()
    {
        foreach (Transform child in wayPointsParent)
        {
            waypoints.Add(child.GetComponent<Waypoint>());
        }

        nav = GetComponent<NavMeshAgent>();
        NextWaypoint();
    }

    protected override void UPDATE_SEARCHING()
    {
        if (nav.remainingDistance < 0.1f)
        {
            hasWaited += Time.deltaTime;
            if (hasWaited >= currentWaitTime)
            {
                hasWaited = 0;
                currentWaitTime = Random.Range(0, maxWaitTime);
                NextWaypoint();
            }
        }

        if (Dark.Physics.AIsInRangeOfB(transform.position, Game.Get().Player.transform.position, 3f))
        {
            ChangeState(State.FOLLOWING);
        }

    }

    protected override void UPDATE_FOLLOWING()
    {
        nav.SetDestination(target.position);

        if (false == Dark.Physics.AIsInRangeOfB(transform.position, target.position, chaseRange))
        {
            Dark.Physics.RayCastInfo info = Dark.Physics.RayCastFromAToB(transform.position, target.position, 100f);

            if (info == null)
            {
                currentBlindChaseTime += Time.deltaTime;
                if (currentBlindChaseTime >= blindChaseTime)
                {
                    currentBlindChaseTime = 0f;
                    ChangeState(State.SEARCHING);
                }
            }

            if (info.hit.transform.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                currentBlindChaseTime += Time.deltaTime;
                if (currentBlindChaseTime >= blindChaseTime)
                {
                    currentBlindChaseTime = 0f;
                    ChangeState(State.SEARCHING);
                }
            }
        }
    }

    bool HasAPoint()
    {
        foreach (Waypoint waypoint in waypoints)
        {
            if (waypoint.isSetEnabled)
            {
                return true;
            }
        }
        return false;
    }

    void NextWaypoint()
    {
        if (HasAPoint())
        {
            IncreaseWaypoint();
            if (waypoints[currentWaypoint].isSetEnabled == true)
            {
                SetDestination();
            }
            else
            {
                NextWaypoint();
            }
        }

    }

    void IncreaseWaypoint()
    {
        currentWaypoint ++;

        if (currentWaypoint > waypoints.Count-1)
        {
            currentWaypoint = 0;
        }

    }

    void SetDestination()
    {
        if (waypoints[currentWaypoint].isSetEnabled == true)
        {
            Vector3 target = waypoints[currentWaypoint].transform.position;
            nav.SetDestination(target);
        }
    }

    protected override void STOP_SEARCHING()
    {
        hasWaited = 0;

        /*Preference: "Actiavting this will do that the Enemy is staying still for a tick befor following you"*/
        //nav.SetDestination(transform.position);
    }

    protected override void STOP_FOLLOWING()
    {
        target = null;
        nav.SetDestination(transform.position);
    }

    protected override void START_FOLLOWING()
    {
        target = Game.Get().Player.transform;
    }

}


