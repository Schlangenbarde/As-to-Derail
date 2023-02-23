using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainIdleSounds : MonoBehaviour
{
    [SerializeField]
    private TrainParent trainParent;
    [SerializeField]
    private Animator animator;
    private float blend;

    private bool hasAnimator = false;
    FMOD.Studio.EventInstance trainEngine;

    void Awake()
    {
        trainEngine = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/TrainEngine");
        trainEngine.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(trainEngine, GetComponent<Transform>(), GetComponent<Rigidbody>());

        if (animator != null)
        {
            hasAnimator = true;
        }
    }

    void Update()
    {
        blend = trainParent.getTrainVelocity;
        trainEngine.setParameterByName("EngineSpeed", blend * 2);
        if (hasAnimator)
        {
            UpdateAnimator(blend);
        }
    }

    void UpdateAnimator(float b)
    {
        animator.SetFloat("Blend", b);
    }
}
