using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dark;

[RequireComponent(typeof(CharacterController))]
public class Gravity : VelocityD
{
    CharacterController cc;
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    protected override void Move(Vector3 vel)
    {
        base.Move(vel);
        cc.Move(velocity);
    }

    [HideInInspector]
    public Vector3 outVelocity = Vector3.zero;

    public void GiveImpuls(Vector3 impuls)
    {
        outVelocity = impuls;
        startVec3 = outVelocity;

        startTypes = Math.GetType(outVelocity);
        lastStartTypes = startTypes;

        letOutVelocityDie = true;
    }

    public void StopImpuls()
    {
        outVelocity = Vector3.zero;
        startVec3 = Vector3.zero;
        startTypes = Vector3.zero;
        lastStartTypes = Vector3.zero;

        letOutVelocityDie = false;
    }

    [HideInInspector]
    public bool letOutVelocityDie = false;

    float flightTime = 2f;

    Vector3 startVec3;

    Vector3 lastStartTypes;
    Vector3 startTypes;

    private void Update()
    {
        UpdateOnGround();
        if (cc.enabled == true)
        {
            Vector3 vel = Vector3.zero;
            vel += outVelocity;
            Move(vel);
        }

        if (letOutVelocityDie == true)
        {
            outVelocity -= outVelocity * Time.deltaTime/flightTime;

            outVelocity.x -= Math.GetType(outVelocity.x) * startVec3.x * Time.deltaTime / flightTime;
            outVelocity.y -= Math.GetType(outVelocity.y) * startVec3.y * Time.deltaTime / flightTime;
            outVelocity.z -= Math.GetType(outVelocity.z) * startVec3.z * Time.deltaTime / flightTime;

            if (lastStartTypes.x != startTypes.x)
            {
                outVelocity.x = 0;
                startVec3.x = 0;
            }

            if (lastStartTypes.y != startTypes.y)
            {
                outVelocity.y = 0;
                startVec3.y = 0;
            }

            if (lastStartTypes.z != startTypes.z)
            {
                outVelocity.z = 0;
                startVec3.z = 0;
            }

            if (outVelocity == Vector3.zero)
            {
                letOutVelocityDie = false;
            }
        }
    }

    public void Ativate()
    {
        gravityOn = true;
    }

    public void Deactivate()
    {
        gravityOn = false;
        fallVelocity = 0;
    }
}
