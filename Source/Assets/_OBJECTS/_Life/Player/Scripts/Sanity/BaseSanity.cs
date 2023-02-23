using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSanity : MonoBehaviour
{

    public static Action playerDied;
    public static Action playerIsGoingToDie;


    private float maxSanity = 0;

    [SerializeField]
    private float currentSanity;

    [SerializeField]
    GameObject vCamBrain;

    private float negativeMultiplicator = 0;

    private float positiveMultiplicator = 3;

    public float GetCurrentSanity => currentSanity;
    public float GetPosMultiplicator => positiveMultiplicator;
    public float GetNegMultiplicator => negativeMultiplicator;

    float killTimer = 10;

    private void Awake()
    {
        currentSanity = maxSanity;
    }

    private void Update()
    {
        UpdateSanity();
    }


    public void IncreaseSanity(float value)
    {
        currentSanity -= value;
    }

    public void DecreaseSanity(float value)
    {
        currentSanity += value;
    }

    public void IncreaseSanityNegativMuliplicator(float value)
    {
        negativeMultiplicator += value;
        negativeMultiplicator = Mathf.Clamp(negativeMultiplicator, 0, 20);
    }

    public void DecreaseSanityNegativMultiplicator(float value)
    {
        negativeMultiplicator -= value;
        negativeMultiplicator = Mathf.Clamp(negativeMultiplicator, 0, 20);
    }

    public void IncreaseSanityPositiveMuliplicator(float value)
    {
        positiveMultiplicator += value;
        positiveMultiplicator = Mathf.Clamp(positiveMultiplicator, 1, 20);
    }

    public void DecreaseSanityPositiveMultiplicator(float value)
    {
        positiveMultiplicator -= value;
        positiveMultiplicator = Mathf.Clamp(positiveMultiplicator, 1, 20);
    }

    public void ResetSanityNegativeMultiplicator()
    {
        negativeMultiplicator = 0;
    }

    public void ResetSanityPositiveMultiplicator()
    {
        positiveMultiplicator = 1;
    }

    private void UpdateSanity()
    {
        currentSanity += 1 * negativeMultiplicator * Time.deltaTime;
        currentSanity -= 1 * positiveMultiplicator * Time.deltaTime;

        CheckForDeathBySanity();

        currentSanity = Mathf.Clamp(currentSanity, 0, 100);
    }

    private void CheckForDeathBySanity()
    {
        if (currentSanity >= 100) StartDeathTimer();
        else killTimer = 10;
    }

    private void StartDeathTimer()
    {
        if (killTimer > 0 && killTimer > 1) killTimer -= Time.deltaTime;
        else if (killTimer < 1 && killTimer > 0)
        {
            playerIsGoingToDie?.Invoke();
            killTimer -= Time.deltaTime;
        }
        else
        {
            KillPlayer();
        }
    }

    private void ResetSanity()
    {
        ResetSanityNegativeMultiplicator();
        ResetSanityPositiveMultiplicator();
        currentSanity = 0;
        killTimer = 10;
    }

    private void KillPlayer()
    {
        playerDied?.Invoke();
        vCamBrain.SetActive(false);
        Game.Get().Player.GetComponent<Health>().Die();
        Game.Get().Player.GetComponent<Transform>().parent = null;
        Game.Get().Player.GetComponent<HandleCamera>().SwitchBackToPlayerCam();
        ResetSanity();
        vCamBrain.SetActive(true);


    }
}
