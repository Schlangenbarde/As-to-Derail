using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchter : MonoBehaviour
{
    class Switch
    {
        bool value = false;

        public void Activate()
        {
            value = true;
        }

        public bool IsActive()
        {
            return value;
        }
    }

    List<Switch> switches = new List<Switch>();

    private void Awake()
    {
        CreateSwitchSlots(100);
    }

    void CreateSwitchSlots(int length)
    {
        for (int i = 0; i < length; i++)
        {
            switches.Add(new Switch());
        }
    }

    public void ActivateSwitch(int ID)
    {
        switches[ID].Activate();
    }

    public bool IsSwitchActive(int ID)
    {
        return switches[ID].IsActive();
    }
}
