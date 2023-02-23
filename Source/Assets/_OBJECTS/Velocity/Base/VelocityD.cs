using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityD : MonoBehaviour
{
    /*GroundCheck*/
    protected bool onGround = false;
    public Transform groundPoint;

    /*Velocity*/
    protected Vector3 velocity;
    
    protected virtual void Move(Vector3 vel)
    {
        velocity = vel;

        if (!gravityOn) return;
        velocity += new Vector3(0, fallVelocity * Time.deltaTime, 0);
    }

    public Vector3 GetVelocity => velocity;

    /*Gravity*/
    public bool gravityOn = false;

    protected float fallVelocity = 0f;

    public float GetFallVelocity => fallVelocity;

    private float maxFallVelocity = 100f;

    protected void SetMaxFallSpeed(float max)
    {
        maxFallVelocity = max;
    }

    private void FixedUpdate()
    {
        if (!gravityOn) return;

        if (!onGround) fallVelocity += Game.Get().WorldGravity * Time.deltaTime;

        if (fallVelocity >= maxFallVelocity) fallVelocity = maxFallVelocity;
    }

    protected void UpdateOnGround()
    {
        if (!gravityOn) return;

        Collider[] colliders = Physics.OverlapSphere(groundPoint.position, 0.1f, Game.Get().groundLayer);

        foreach (var collider in colliders)
        {
            if (collider.transform != transform && fallVelocity < 0)
            {
                onGround = true;
                fallVelocity = -2f;
                return;
            }
        }
        onGround = false;
    }

    protected Vector3 GetGroundVelocity()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out VelocityD v))
            {
                return v.GetVelocity;
            }
        }
        return Vector3.zero;
    }

}
