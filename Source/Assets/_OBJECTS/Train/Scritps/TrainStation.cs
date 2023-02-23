using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class TrainStation : MonoBehaviour
{
    [SerializeField, Tooltip("Drag the Ticket UI objectr In")]
    private TicketUI ticketUI;

    [SerializeField, Tooltip("Name the Station")]
    protected string indexName;

    [SerializeField, Tooltip("Decide how long the player needs to wait to spawn a Train ")]
    protected float maxWaitTimer;

    [Header("---------------Stations---------------")]
    [SerializeField, Tooltip("Drag the Station in where the Train U1 should go")]
    protected TrainStation targetStationU1;

    [SerializeField, Tooltip("Drag the previos Station in where the Train U1 should go")]
    protected TrainStation PrevStationU1;
    [Space(5)]
    [SerializeField, Tooltip("Drag the Station in where the Train U2 should go")]
    protected TrainStation targetStationU2;
    
    [SerializeField, Tooltip("Drag the previos Station in where the Train U2 should go")]
    protected TrainStation PrevStationU2;

    [Header("---------------Train U1---------------")]
    [SerializeField, Tooltip("Select Waypints on Next line for the Train")]
    protected List<Transform> nextLineUOneList = new List<Transform>();
    
    [SerializeField, Tooltip("Select Waypints on previos line for the Train")]
    protected List <Transform> prevLineUOneList = new List<Transform>();

    [Header("---------------Train U2---------------")]
    [SerializeField, Tooltip("Select Waypints on Next line for the Train")]
    protected List<Transform> nextLineUTwoList = new List<Transform>();

    [SerializeField, Tooltip("Select Waypints on previos line for the Train")]
    protected List<Transform> prevLineUTwoList = new List<Transform>();

    [Header("--------------TrainSpawner--------------")]
    [SerializeField, Tooltip("Drag all train Spawner in")]
    protected List<TrainSpawner> trainSpawnerList = new List<TrainSpawner>();

    public bool isLevelDesignDone;
    
    protected bool isMovingForeward;
    protected bool isPlayerInsideStationTrigger;
    protected bool isNextStationSpawnerSelected = true;

    bool firstTimeIn;

    protected List<TrainStation> targetStaionList = new List<TrainStation>();

    public string IndexName => indexName;
    public List<TrainStation> TargetStaionList => targetStaionList;

    private void Awake()
    {
        
        if (targetStationU1 != null) targetStaionList.Add(targetStationU1);
        if (targetStationU2 != null) targetStaionList.Add(targetStationU2);
        if (PrevStationU1 != null) targetStaionList.Add(PrevStationU1);
        if (PrevStationU2 != null) targetStaionList.Add(PrevStationU2);

    }

    private void Update()
    {
        foreach (var spawner in trainSpawnerList)
        {
            spawner.isLevelDesignDone = isLevelDesignDone;
        }

        if (isPlayerInsideStationTrigger)
        {
            if (isLevelDesignDone)
            {
                ticketUI.EnableBoolInAnimator();
            }
            else ticketUI.DisableBoolInAnimator();
        }
    }

    private void OnTriggerEnter(Collider target)
    {

        if (target.gameObject.GetComponent<TrainUOne>())
        {
            var train = target.gameObject.GetComponent<TrainUOne>();


            if (isMovingForeward) train.SetNextDestination(nextLineUOneList);
            else train.SetNextDestination(prevLineUOneList);
        }

        if (target.gameObject.GetComponent<TrainUTwo>())
        {
            var train = target.gameObject.GetComponent<TrainUTwo>();

            if (isMovingForeward) train.SetNextDestination(nextLineUTwoList);
            else train.SetNextDestination(prevLineUTwoList);
        }

        if (target.gameObject.CompareTag("Player"))
        {
            isPlayerInsideStationTrigger = true;
        }
    }

    private void OnTriggerExit(Collider target)
    {
        foreach (Transform child in target.transform)
        {
            if (child.GetComponent<Movement>())
            {
                isPlayerInsideStationTrigger = false;
            }
        }

        if (target.GetComponent<Movement>())
        {
            isPlayerInsideStationTrigger = false;
        }

        if (target.GetComponent<BaseTrain>())
        {
            foreach (Transform child in target.transform)
            {
                if (child.GetComponent<Movement>())
                {
                    isPlayerInsideStationTrigger = false;
                }
            }
        }
    }

    public void SetDirection(bool dir)
    {
        isMovingForeward = dir;
    }

    public string GetTrain(string destinationName)
    {
        if (targetStationU1.name == destinationName || PrevStationU1.name == destinationName)
        {
            return "U1";
        }
        else if (targetStationU2.name == destinationName || PrevStationU2.name == destinationName)
        {
            return "U2";
        }
        return null;
    }

}
