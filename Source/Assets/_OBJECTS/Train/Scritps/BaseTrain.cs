using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(CharacterController))]
public class BaseTrain : MonoBehaviour
{
    [SerializeField, Tooltip("Add Train Data")]
    protected TrainData trainData;

    [SerializeField,Tooltip("Add Doors from train which are on left side")]
    protected List<TrainDoors> leftSideDoors = new List<TrainDoors>();

    [SerializeField, Tooltip("Add Doors from train which are on Right side")]
    protected List<TrainDoors> rightSideDoors = new List<TrainDoors>();

    [SerializeField, Tooltip("Change Speed of Train")]
    protected int speed;

    [SerializeField, Tooltip("How fast can the train be")]
    protected float maxVelocity;
    
    [SerializeField, Tooltip("Decide how much time the Train need to start to the next waypoint")]
    protected float maxWaitTimer;

    [SerializeField, Tooltip("How high are the chances to spawn a Encounter"), Range(0f, 1f)]
    protected float encounterSpawnChance;

    [SerializeField, Tooltip("Position of the Spawner for encounter")]
    protected Transform encounterSpawnPoint;

    [HideInInspector]
    public bool initStart;

    public static Action<float> playerInsideTrain;
    public static Action<float> playerLeftTrain;
    public static Action playerLeftTrainAfterStop;
    public static Action trainStopped;

    [SerializeField]
    private LayerMask enemyLayer;
    protected List<Transform> nextDestination;
    List<Transform> SavedDestination;
    protected GameObject player;

    protected CharacterController characterController;

    protected bool isMoving;
    protected float waitTimer;

    protected float currentSpeed;

    public bool isLastStation;

    bool trainPlatformIsOnLeftSide;

    [HideInInspector]
    public bool reachedEndOfTrain = false;

    private bool gotTeleported; 

    float timer = 7;
    float moveTimer = 5;
    float trainVel;
    public bool shouldMoveAwayFromTrainStation;

    protected virtual void Awake()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
        
        isMoving = false;
        waitTimer = 1;

    }

    private void OnEnable()
    {
        TrainSeat.playerGotOnSeat += StartTrain;
        BaseSanity.playerDied += CloseTrainDoors;
        BaseSanity.playerDied += ReactToPlayerDead;
    }

    private void OnDisable()
    {
        BaseSanity.playerDied -= CloseTrainDoors;   
        TrainSeat.playerGotOnSeat -= StartTrain;
        BaseSanity.playerDied -= ReactToPlayerDead;
    }

    float timerBeforStart = 5;

    protected virtual void Update()
    {
        if (player != null)
        {


            if (initStart)
            {
                trainVel = 0;
                CloseTrainDoors();
                if (timerBeforStart > 0) timerBeforStart -= Time.deltaTime;
                else
                {
                    GetComponentInParent<TrainParent>().test();
                    player.GetComponent<CharacterController>().enabled = false;

                    if (timer > 0) timer -= Time.deltaTime;
                    else
                    {
                        playerInsideTrain?.Invoke(8);

                        nextDestination = SavedDestination;
                        SavedDestination = null;
                        TeleportToWaypoint();
                        gotTeleported = true;
                        initStart = false;
                        isMoving = true;
                        waitTimer = maxWaitTimer;
                        timer = 7;
                        timerBeforStart = 5;
                    }
                }
                
            }

            if (isMoving)
            {
                if (CheckForEnd(nextDestination[0].position.x, nextDestination[0].position.z))
                {
                    GetComponentInParent<TrainParent>().NegaTest();
                    if (waitTimer < 0)
                    {
                        OpenTrainDoors();
                        
                        isMoving = false;
                        player.GetComponent<CharacterController>().enabled = true;
                        trainStopped?.Invoke();
                    }
                    else waitTimer -= Time.deltaTime;
                }
            }
        }
        else
        {

            if (shouldMoveAwayFromTrainStation)
            {
                gotTeleported = false;
                if (moveTimer > 9)
                {
                    CloseTrainDoors();
                    moveTimer -= Time.deltaTime;

                }
                else if (moveTimer > 0 && moveTimer < 9)
                {
                    trainVel += 10 * Time.deltaTime;
                    characterController.Move(Vector3.forward * trainVel * Time.deltaTime);
                    moveTimer -= Time.deltaTime;
                }
            }
        }
        #region oldCode
        //if (isMoving && nextDestination != null)
        //{

        //    if (player != null)
        //    {
        //        player.GetComponent<CharacterController>().enabled = false;
        //    }



        //    //if (firstPhase)
        //    //{
        //    //    if (isLastStation && player == null)
        //    //    {
        //    //        if (applySpeed) ApplyTrainSpeed();
        //    //        else ReduceTrainSpeed();
        //    //        MoveToFirstWaypoint();
        //    //    }
        //    //    else if (!isLastStation)
        //    //    {
        //    //        if (applySpeed) ApplyTrainSpeed();
        //    //        else ReduceTrainSpeed();
        //    //        MoveToFirstWaypoint();
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    if (applySpeed) ApplyTrainSpeed();
        //    //    else ReduceTrainSpeed();
        //    //    MoveToStation();
        //    //}
        //}
        #endregion


    }

    protected void StartTrain()
    {
        if (player != null) initStart = true;
    }

    private void TeleportToWaypoint()
    {
        transform.position = new Vector3(nextDestination[0].position.x, transform.position.y, nextDestination[0].position.z);
    }


    //protected virtual Vector3 MoveToFirstWaypoint()
    //{
    //    float x = nextDestination[0].transform.position.x - transform.position.x;
    //    float z = nextDestination[0].transform.position.z - transform.position.z;
    //    characterController.Move(new Vector3(x, 0, z).normalized * currentSpeed * Time.deltaTime);
    //    if (CheckForEnd(nextDestination[0].transform.position.x, nextDestination[0].transform.position.z))
    //    {
    //        TeleportToWaypoint();
    //    }
    //    return new Vector3(x, 0, z).normalized * currentSpeed * Time.deltaTime;
    //}

    //protected virtual void TeleportToWaypoint()
    //{
    //    savedSpeedbevorTeleport = currentSpeed;
    //    Vector3 pos = nextDestination[1].transform.position;
    //    if (player != null)
    //    {
    //        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    //    }
    //    else
    //    {
    //       GetComponentInParent<TrainParent>().SelfDestroy();
    //    }
    //    firstPhase = false;
    //}

    //protected virtual Vector3 MoveToStation()
    //{
    //    if (waitTimerToLastStop < 0)
    //    {

    //        float x = nextDestination[2].transform.position.x - transform.position.x;
    //        float z = nextDestination[2].transform.position.z - transform.position.z;
    //        characterController.Move(new Vector3(x, 0, z).normalized * currentSpeed * Time.deltaTime);

    //        if (CheckForEnd(nextDestination[2].transform.position.x, nextDestination[2].transform.position.z))
    //        {
    //            player.GetComponent<CharacterController>().enabled = true;
    //            isMoving = false;
    //            currentSpeed = 0;
    //            firstPhase = true;
    //            OpenTrainDoors();

    //        }

    //        return new Vector3(x, 0, z).normalized * currentSpeed * Time.deltaTime;
    //    }
    //    else
    //    {
    //        waitTimerToLastStop -= Time.deltaTime;
    //    }

    //    currentSpeed = savedSpeedbevorTeleport;
    //    return Vector3.zero;

    //}


    protected bool CheckForEnd(float targetPosX, float targetPosZ)
    {
        float maxDistance = Vector3.Distance(new Vector3(targetPosX, 0, targetPosZ), new Vector3(transform.position.x, 0, transform.position.z));
        if (maxDistance < 1)
        {
            return true;
        }
        return false;
    }

    protected void CloseTrainDoors()
    {
        if (trainPlatformIsOnLeftSide)
        {
            foreach (var door in leftSideDoors)
            {
                door.Close();
            }
        }
        else
        {
            foreach (var door in rightSideDoors)
            {
                door.Close();
            }
        }
    }

    public void OpenTrainDoors()
    {
        if (trainPlatformIsOnLeftSide)
        {
            foreach (var door in leftSideDoors)
            {
                door.Open();
            }
        }
        else
        {
            foreach (var door in rightSideDoors)
            {
                door.Open();
            }
        }
    }

    private void ReactToPlayerDead()
    {
        if (player != null)
        {
            player.transform.parent = null;
            player = null;
        }
        if (gotTeleported)
        {
            CloseTrainDoors();
            shouldMoveAwayFromTrainStation = true;
            moveTimer = 5;
        }
    }

    protected void ApplyTrainSpeed()
    {
        currentSpeed += speed * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 1, maxVelocity);
    }

    protected void ReduceTrainSpeed()
    {
        currentSpeed -= speed *2  * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 20, maxVelocity);
    }

    //protected void TimeForNextStart()
    //{
    //    if (waitTimer > 0)
    //    {
    //        waitTimer -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        initStart = true;
    //        waitTimer = maxWaitTimer;
    //    }
    //}

    //protected void InitEncounter()
    //{
    //    foreach (var encounter in trainData.Encounter)
    //    {
    //        float rndNumber = UnityEngine.Random.Range(0, 100);

    //        if (rndNumber / 100 < encounterSpawnChance)
    //        {
    //            Instantiate(encounter, encounterSpawnPoint);
    //        }
          
    //    }
    //}

    public void SetPlatformSide(bool value)
    {
        trainPlatformIsOnLeftSide = value;
    }
    public virtual void SetNextDestination(List<Transform> targetPos)
    {
        SavedDestination = targetPos;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && player == null)
        {
            player = other.gameObject;
            player.transform.parent = transform;
        }

        if (other.gameObject.layer == enemyLayer)
        {
            other.gameObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isMoving)
        {

            playerLeftTrain?.Invoke(8);
            playerLeftTrainAfterStop?.Invoke();
            player.transform.parent = null;
            player = null;
            if (gotTeleported)
            {
                shouldMoveAwayFromTrainStation = true;
                moveTimer = 15;
            } 
        }
        if (other.gameObject.layer == enemyLayer)
        {
            other.gameObject.transform.parent = null;
        }

    }

}
