using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainParent : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private float trainVelocity;

    bool isMoving;

    public float getTrainVelocity => trainVelocity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = GetComponentInChildren<Transform>().position;
        if (isMoving)
        {
            trainVelocity += Time.deltaTime * 0.1f;
            trainVelocity = Mathf.Clamp(trainVelocity, 0, 1 );
        }
        else
        {
            trainVelocity -= Time.deltaTime * 0.1f;
            trainVelocity = Mathf.Clamp(trainVelocity, 0, 1);
        }
    }


    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    void OpenDorsAtEndOfAnimation()
    {
        GetComponentInChildren<BaseTrain>().OpenTrainDoors();
        animator.enabled = false;
    }

    public void GotTeleportetToPlayer()
    {
        animator.enabled = true;

        GetComponentInChildren<BaseTrain>().shouldMoveAwayFromTrainStation = false;
        animator.SetTrigger("Start");
    }
    
    public void test()
    {
        isMoving = true;
    }

    public void NegaTest()
    {
        isMoving = false;
    }
}
