using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    [SerializeField]
    private TrainCollection trainCollection;

    [SerializeField, Tooltip("Decide which Train should Spawn here")]
    private bool shouldBeTrainUOne;

    [SerializeField, Tooltip("How long should it take to spawn a Train")]
    private float maxWaitTimer;

    [SerializeField, Tooltip("Is this the lastStation")]
    private bool lastStation;

    [SerializeField, Tooltip("Drag the Station rail barrier in")]
    private RailBarrier railBarrier;

    private GameObject train;

    [HideInInspector]
    public bool isLevelDesignDone;
    private float currentWaitTimer;
    private bool isPlayerInside;
    private bool isTrainInside;

    private void Awake()
    {
        currentWaitTimer = maxWaitTimer;
        if (!lastStation)
        {
            if (shouldBeTrainUOne) train = trainCollection.trainUOne;
            else train = trainCollection.trainUTwo;
        }
    }

    private void Update()
    {
        if (isPlayerInside && !lastStation && !isTrainInside && isLevelDesignDone)
        {
            if (currentWaitTimer < 0)
            {
                ResetWaitTimer();
                train.GetComponent<TrainParent>().GotTeleportetToPlayer();
                train.transform.position = transform.position;
                train.transform.localEulerAngles = transform.localEulerAngles;
                railBarrier.SelfDestroy();
            }
            else
            {
                currentWaitTimer -= Time.deltaTime;
            }
        }
    }

    private void ResetWaitTimer()
    {
        int x = Random.Range(0, 5);
        currentWaitTimer = maxWaitTimer + x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
        if (other.gameObject.GetComponent<BaseTrain>())
        {
            if (lastStation) other.gameObject.GetComponent<BaseTrain>().isLastStation = true;
            isTrainInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
        if (other.gameObject.GetComponent<BaseTrain>())
        {
            isTrainInside = false;
            foreach (Transform child in other.transform)
            {
                if (child.gameObject.GetComponent<Movement>()) isPlayerInside = false;
            }
        }

    }
}
