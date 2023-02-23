using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager
{
    public enum Event{ PlayerNegativImpulse, PlayerTookDamage, PlayerOpenedMapFirstTime, PlayerBurning, PlayerStopBurning, GameEnd};

    public static Action PlayerNegativImpulse_Event;
    public static Action PlayerTookDamage_Event;
    public static Action PlayerOpenedMapFirstTime_Event;
    public static Action PlayerBurning_Event;
    public static Action PlayerStopBurning_Event;
    public static Action GameEnd_Event;


    public static void PlayEvent(Event ev)
    {
        switch (ev)
        {
            case Event.PlayerNegativImpulse:
                PlayerNegativImpulse_Event?.Invoke();
                Debug.LogWarning("PlayerNegativImpulse_Event");
                break;
            case Event.PlayerTookDamage:
                PlayerTookDamage_Event?.Invoke();
                Debug.LogWarning("PlayerTookDamage");
                break;
            case Event.PlayerOpenedMapFirstTime:
                PlayerOpenedMapFirstTime_Event?.Invoke();
                Debug.LogWarning("PlayerOpenedMapFirstTime");
                break;
            case Event.PlayerBurning:
                PlayerBurning_Event?.Invoke();
                Debug.LogWarning("PlayerBurning");
                break;
            case Event.PlayerStopBurning:
                PlayerStopBurning_Event?.Invoke();
                Debug.LogWarning("PlayerStopBurning");
                break;
            case Event.GameEnd:
                GameEnd_Event?.Invoke();
                Debug.LogWarning("GameEnd");
                break;
        }
    }
}
