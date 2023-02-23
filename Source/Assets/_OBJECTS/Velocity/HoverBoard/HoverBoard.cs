using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBoard : VelocityD
{
    [SerializeField]
    float speed = 1;

    private void Update()
    {

        Vector3 vel = new Vector3(1, 0, 0);
        vel *= Time.deltaTime * speed;
        Move(vel);
    }

    protected override void Move(Vector3 vel)
    {
        base.Move(vel);
        GetComponent<CharacterController>().Move(vel);
    }
}
