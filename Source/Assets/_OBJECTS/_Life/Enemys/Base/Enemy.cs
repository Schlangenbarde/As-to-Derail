using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    public enum State { IDLE, SEARCHING, FOLLOWING, ATTACKING}

    private State state;

    [SerializeField]
    State startState;

    protected Transform target;

    public void ChangeState(State newState)
    {
        switch (state)
        {
            case State.IDLE:
                STOP_IDLE();
                break;
            case State.SEARCHING:
                STOP_SEARCHING();
                break;
            case State.FOLLOWING:
                STOP_FOLLOWING();
                break;
            case State.ATTACKING:
                STOP_ATTACKING();
                break;
        }

        switch (newState)
        {
            case State.IDLE:
                START_IDLE();
                break;
            case State.SEARCHING:
                START_SEARCHING();
                break;
            case State.FOLLOWING:
                START_FOLLOWING();
                break;
            case State.ATTACKING:
                START_ATTACKING();
                break;
        }

        state = newState;
    }

    private void Start()
    {
        state = startState;
        Setup();
    }

    protected virtual void Setup() { }

    private void Update()
    {
        switch (state)
        {
            case State.IDLE:
                UPDATE_IDLE();
                break;
            case State.SEARCHING:
                UPDATE_SEARCHING();
                break;
            case State.FOLLOWING:
                if (target != null)
                {
                    UPDATE_FOLLOWING();
                }
                else
                {
                    Debug.Log("Error: no Target Found on: " + name);
                }
                
                break;
            case State.ATTACKING:
                UPDATE_ATTACKING();
                break;
        }
    }

    protected virtual void UPDATE_IDLE() { }
    protected virtual void UPDATE_SEARCHING() { }
    protected virtual void UPDATE_FOLLOWING() { }
    protected virtual void UPDATE_ATTACKING() { }

    protected virtual void START_IDLE() { }
    protected virtual void START_SEARCHING() { }
    protected virtual void START_FOLLOWING() { }
    protected virtual void START_ATTACKING() { }

    protected virtual void STOP_IDLE() { }
    protected virtual void STOP_SEARCHING() { }
    protected virtual void STOP_FOLLOWING() { }
    protected virtual void STOP_ATTACKING() { }
}
