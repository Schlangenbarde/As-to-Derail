using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailBarrier : MonoBehaviour
{
    [SerializeField]
    BoxCollider nonTriggerCollider;

    [SerializeField]
    BoxCollider TriggerCollider;

    private void OnEnable()
    {
        BaseTrain.playerInsideTrain += test;
    }

    private void OnDisable()
    {
        BaseTrain.playerInsideTrain -= test;
    }

    private void Awake()
    {
        nonTriggerCollider.enabled = false;
    }

    public void SelfDestroy()
    {
         nonTriggerCollider.enabled = false;
        TriggerCollider.enabled = false;
    }

    void test(float xc)
    {
        TriggerCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Movement>())
        {
            nonTriggerCollider.enabled = true;

        }
    }

}
