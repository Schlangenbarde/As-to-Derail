using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrain : BaseTrain
{
    [SerializeField]
    private GameObject firstPrefeab;

    [SerializeField]
    private GameObject secondPrefab;


    public static Action playerWentIn;
    private bool firstTrain = true;

    private bool notInit = true;

    private void OnEnable()
    {
        playerWentIn += ChangeFirstTrainCheck;
    }

    private void OnDisable()
    {
        playerWentIn += ChangeFirstTrainCheck;
    }


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        if (isMoving)
        {
            InitPrefab();
            
        }
    }

    void InitPrefab()
    {
        if (firstTrain && notInit)
        {
            Instantiate(firstPrefeab, transform.position, Quaternion.identity);
            playerWentIn?.Invoke();
            notInit = false;
        }
        else if (!firstTrain && notInit)
        {
            Instantiate(secondPrefab, transform.position, Quaternion.identity);
            notInit = false;
        }
    }

    private void ChangeFirstTrainCheck()
    {
        firstTrain = false;
    } 
}
