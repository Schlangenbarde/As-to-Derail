using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dark;
using FMODUnity;

public class Dialog : EnemyState
{
    public OptionPanel startPanel;

    public Sprite profilePic;

    bool inDialog = false;
    bool inRotation = false;
    bool inZoom = false;
    bool running = false;

    public float rotateSpeed = 10f;
    public float zoomSpeed = 1f;
    public float targetZoom = 30f;

    [HideInInspector]
    public OptionPanel lastDialogPoint;

    [HideInInspector]
    public OptionPanel endingPanel;

    [HideInInspector]
    public bool dialogFinished = false;

    [HideInInspector]
    public string finishedText;

    [HideInInspector]
    public EventReference finishedAudio;

    [SerializeField]
    Transform facePoint;

    private void Start()
    {
        endingPanel = ScriptableObject.CreateInstance<OptionPanel>();
    }

    public void SetDialogStatus(bool status)
    {
        inDialog = status;
    }

    public override void Do()
    {
        Vector3 playerPosition = Game.Get().Player.transform.position;

        if (inDialog && !Dark.Physics.AIsInRangeOfB(playerPosition, transform.position, 6))
        {
            print("StopingDialogBecauseOfRange");

            Game.Get().chatLog.StopDialogAbruptly();
            inRotation = false;
            inDialog = false;
            inZoom = false;
            running = false;
            return;
        }

        if (inRotation)
        {
            print("InRotation");

            float targetRotationY = Game.Get().Player.transform.localEulerAngles.y + 180f;
            float currentRotationY = transform.localEulerAngles.y;

            float distance = Math.ToPositive(targetRotationY - currentRotationY);

            if (distance > 360)
            {
                distance -= 360;
            }

            if (distance > 1f)
            {
                float newY = Mathf.LerpAngle(currentRotationY, targetRotationY, rotateSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, newY, transform.eulerAngles.z);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetRotationY, transform.eulerAngles.z);
                inRotation = false;
            }
        }
        if (inZoom)
        {
            print("InZoom");

            Game.Get().Player.GetComponent<Pause>().SetTo(true);
            float currentZoom = Game.Get().PlayerCamera.m_Lens.FieldOfView;

            float distance = Math.ToPositive(targetZoom - currentZoom);

            if (distance > 360)
            {
                distance -= 360;
            }

            if (distance > 1f)
            {
                float newValue = Mathf.LerpAngle(currentZoom, targetZoom, zoomSpeed * Time.deltaTime);
                Game.Get().PlayerCamera.m_Lens.FieldOfView = newValue;
            }
            else
            {
                inZoom = false;
                Game.Get().PlayerCamera.m_Lens.FieldOfView = targetZoom;
            }
        }

        if (!inZoom && inDialog && !running)
        {
            /*Start Dialog*/
            Game.Get().chatLog.StartDialog(profilePic, gameObject);
            running = true;
        }
    }
    public override void StartState()
    {
        print("StartState");

        StartDialog();
    }

    public override void Stop()
    {
        Game.Get().chatLog.StopDialogAbruptly();
    }

    public void StartDialog()
    {
        print("StartDialog");
        inRotation = true;
        inZoom = true;
        inDialog = true;
        running = false;
        Camera.main.transform.LookAt(facePoint);
    }
}
