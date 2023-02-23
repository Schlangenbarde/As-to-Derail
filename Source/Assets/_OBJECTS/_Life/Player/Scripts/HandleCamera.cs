using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HandleCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera playerCam;

    private CinemachineVirtualCamera currentCam;

    private void OnEnable()
    {
        TrainSeat.seatCamAction += SwitchCam;
        BaseTrain.trainStopped += SwitchBackToPlayerCam;
    }

    private void OnDisable()
    {
        TrainSeat.seatCamAction -= SwitchCam;
        BaseTrain.trainStopped -= SwitchBackToPlayerCam;
    }

    private void SwitchCam(CinemachineVirtualCamera incomingCam, Transform lookAt)
    {
        currentCam = incomingCam;
        Game.Get().Player.GetComponent<Transform>().localEulerAngles = lookAt.transform.localEulerAngles;
        currentCam.Priority = 1;
        playerCam.Priority = 0;
        

    }

    public void SwitchBackToPlayerCam()
    {
        currentCam.Priority = 0;
        playerCam.Priority = 1;
    }
}
