using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NLua;
using System;
using FMODUnity;

[CreateAssetMenu(fileName = "NewOption", menuName = "New/Option")]
public class Option : ScriptableObject
{
    public OptionPanel nextPanel;

    [TextArea(1, 2)]
    public string previewOptionText;

    [TextArea(5,30)]
    public string optionText;

    public EventReference optionAudioClip;

    [TextArea(5, 30)]
    public string awnserText;

    public EventReference awnserAudioClip;

    GameObject owner;

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }


    Lua lua = new Lua();

    [TextArea(5, 30)]
    public string ableToAccesDefinition;

    [TextArea(5, 30)]
    public string consequenzDefinition;

    public bool AbleToAcces()
    {
        SetupLua();

        if(string.IsNullOrEmpty(ableToAccesDefinition)) return true;

        lua.DoString(ableToAccesDefinition);
        bool acces = (bool)lua["acces"];
        return acces;
    }

    public void Consequenz()
    {
        SetupLua();
        lua.DoString(consequenzDefinition);
    }

    void SetupLua()
    {
        lua.LoadCLRPackage();
        lua["acces"] = true;
        lua["player"] = Game.Get().Player;
        lua["print"] = (Action<string>)Debug.Log;
        lua["kill"] = (Action<GameObject>)Destroy;
        lua["IncreaseSanity"] = (Action<float>)Game.Get().Player.GetComponent<BaseSanity>().IncreaseSanity;
        lua["DecreaseSanity"] = (Action<float>)Game.Get().Player.GetComponent<BaseSanity>().DecreaseSanity;

        if (Game.Get().gameStart != null)
        {
            lua["StartGame"] = (Action)Game.Get().gameStart.StartGame;
        }

        lua["IDLE"] = Enemy.State.IDLE;
        lua["SEARCH"] = Enemy.State.SEARCHING;
        lua["CHASE"] = Enemy.State.FOLLOWING;
        lua["ATTACK"] = Enemy.State.ATTACKING;

        if (owner.TryGetComponent(out Enemy enemy))
        {
            lua["ChangeState"] = (Action<Enemy.State>)enemy.ChangeState;
        }

        lua["PlayerHoldsItem"] = (Func<string, bool>)Game.Get().Player.GetComponent<Interact>().PlayerHasItemWithNameInHands;
        lua["PlayerDestroyItem"] = (Action)Game.Get().Player.GetComponent<Interact>().DestroyTheItemThePlayerIsHolding;
        lua["ActivateSwitch"] = (Action<int>)Game.Get().switcher.ActivateSwitch;
        lua["IsSwitched"] = (Func<int, bool>)Game.Get().switcher.IsSwitchActive;
    }
}
