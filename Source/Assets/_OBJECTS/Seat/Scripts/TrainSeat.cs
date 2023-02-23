using UnityEngine;
using Cinemachine;
using System;

public class TrainSeat : ButtonInteractable
{
    [SerializeField]
    private CinemachineVirtualCamera seatCamera;
    [SerializeField]
    private Transform lookAt;


    public static Action<CinemachineVirtualCamera, Transform> seatCamAction;
    public static Action playerGotOnSeat;

    private void OnEnable()
    {
        BaseSanity.playerDied += EnableSelf;
        TrainSeat.playerGotOnSeat += DisableSelf;
        BaseTrain.playerLeftTrainAfterStop += EnableSelf;
    }

    private void OnDisable()
    {
        BaseSanity.playerDied -= DisableSelf;
        TrainSeat.playerGotOnSeat -= DisableSelf;
        BaseTrain.playerLeftTrainAfterStop -= EnableSelf;
    }

    public override void ButtonPressed()
    {
        seatCamAction?.Invoke(seatCamera, transform);
        playerGotOnSeat?.Invoke();
    }

    void DisableSelf()
    {
        GetComponent<Collider>().enabled = false;
    }

    void EnableSelf()
    {
        GetComponent<Collider>().enabled = true;
    }
}
