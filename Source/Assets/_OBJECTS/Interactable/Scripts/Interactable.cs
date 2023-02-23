using UnityEngine;
using System;
using NLua;

public class Interactable : MonoBehaviour
{
    Transform oldParent;
    public Transform OldParent => oldParent;

    protected Lua lua = new Lua();

    [TextArea(6, 50)]
    public string interactionDefinition;

    Interaction action;

    [SerializeField]
    Enemy enemyInteract;

    private void Awake()
    {
        action = GetComponent<Interaction>();
        oldParent = transform.parent;

        lua.LoadCLRPackage();
        lua["Interact"] = (Action)ClassInteraction;

        lua["self"] = gameObject;
        lua["cam"] = Camera.main;
        lua["ERROR"] = (Action<string>)Debug.Log;

        if (enemyInteract != null)
        {
            lua["IDLE"] = Enemy.State.IDLE;
            lua["SEARCH"] = Enemy.State.SEARCHING;
            lua["CHASE"] = Enemy.State.FOLLOWING;
            lua["ATTACK"] = Enemy.State.ATTACKING;
            lua["ChangeState"] = (Action<Enemy.State>)enemyInteract.ChangeState;
        }
        
    }

    public void Interact()
    {
        if (string.IsNullOrEmpty(interactionDefinition))
        {
            BaseInteraction();
            return;
        }
        lua.DoString(interactionDefinition);
    }

    void BaseInteraction()
    {
        Debug.Log("This is a " + name);
    }

    void ClassInteraction()
    {
        if(action == null)
        {
            BaseInteraction();
            return;
        }

        action.Do();
    }
}