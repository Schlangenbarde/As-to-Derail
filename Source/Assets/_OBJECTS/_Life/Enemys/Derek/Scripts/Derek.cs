using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Dark;
using System.IO;

#region Editor
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Derek))]
class DerekEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Derek derek = (Derek)target;

        if (derek.generic)
        {
            EditorGUILayout.LabelField("Mesh");
            derek.mesh = (SkinnedMeshRenderer)EditorGUILayout.ObjectField(derek.mesh, typeof(SkinnedMeshRenderer), true);
        }
    }
}
#endif
#endregion

[RequireComponent(typeof(NavMeshAgent))]
public class Derek : Enemy
{
    NavMeshAgent nav;

    [SerializeField]
    EnemyState idle;
    [SerializeField]
    Animator animator;

    [SerializeField]
    float chaseRange = 20f;

    [SerializeField]
    float blindChaseTime = 5f;

    float currentBlindChaseTime = 0f;

    public bool generic = false;


    [HideInInspector]
    public SkinnedMeshRenderer mesh;

    protected override void Setup() 
    {
        nav = GetComponent<NavMeshAgent>();
        target = Game.Get().Player.transform;

        /*Lua-Addons*/
        lua["StartDialog"] = (Action)StartDialog;

        if (generic)
        {
            if (mesh == null)
            {
                Debug.LogError("Error no Mesh Set in Enemy: \"" + name + "\"\n ID[" + GetInstanceID() + "]");
            }
            else
            {
                Game.Get().textureLoader.SetTexture(mesh);
            }

            OptionPanel[] optionPanels;
            string path = "_GenericDialog/_Start";
            optionPanels = Resources.LoadAll<OptionPanel>(path);

            if (optionPanels.Length == 0)
            {
                Debug.LogError("Couldn't find any Generic-Dialogs");
            }
            else
            {
                int rI = UnityEngine.Random.Range(0, optionPanels.Length - 1);
                GetComponent<Dialog>().startPanel = optionPanels[rI];
            }
        }
    }

    /*Lua-Addon-Functions*/
    void StartDialog()
    {
        idle.StartState();
    }

    protected override void UPDATE_IDLE()
    {
        idle.Do();
    }

    protected override void UPDATE_SEARCHING()
    {
        if (true == Dark.Physics.AIsInRangeOfB(target.position, transform.position, chaseRange))
        {
            ChangeState(State.FOLLOWING);
        }
    }

    protected override void UPDATE_FOLLOWING()
    {
        nav.SetDestination(target.position);

        if (false == Dark.Physics.AIsInRangeOfB(transform.position, target.position, chaseRange))
        {
            Dark.Physics.RayCastInfo info = Dark.Physics.RayCastFromAToB(transform.position, target.position, 100f);

            if (info == null)
            {
                currentBlindChaseTime += Time.deltaTime;
                if (currentBlindChaseTime >= blindChaseTime)
                {
                    currentBlindChaseTime = 0f;
                    ChangeState(State.SEARCHING);
                }
            }

            if (info.hit.transform.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                currentBlindChaseTime += Time.deltaTime;
                if (currentBlindChaseTime >= blindChaseTime)
                {
                    currentBlindChaseTime = 0f;
                    ChangeState(State.SEARCHING);
                }
            }
        }
    }

    protected override void UPDATE_ATTACKING()
    {

    }

    protected override void START_IDLE()
    {
        animator.SetTrigger("IDLE");
    }
    protected override void START_SEARCHING()
    {
        animator.SetTrigger("IDLE");
    }
    protected override void START_FOLLOWING()
    {
        animator.SetTrigger("Chase");
    }
    protected override void START_ATTACKING()
    {

    }




    protected override void STOP_IDLE() 
    {
        idle.Stop();
    }
    protected override void STOP_SEARCHING() { }
    protected override void STOP_FOLLOWING() 
    {
        nav.SetDestination(transform.position);
        
    }
    protected override void STOP_ATTACKING() { }

}
