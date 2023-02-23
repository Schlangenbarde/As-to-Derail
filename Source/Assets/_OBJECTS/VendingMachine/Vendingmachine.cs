using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendingmachine : Interaction
{
    [SerializeField, Tooltip("Define the Amount the vendingMachine has")]
    private int maxAmount;

    [SerializeField, Tooltip("Decide How much the Sanity Increase")]
    private float sanityValue;
    private int currentAmount;

    public static Action<float> playerWasOnvending;

    private void Awake()
    {
        currentAmount = maxAmount;
    }

    public override void Do()
    {
        if (currentAmount > 0)
        {
            playerWasOnvending?.Invoke(sanityValue);
            currentAmount--;
            Debug.Log("Yo du hast eine Cola getrunken Grüße Fabian");
        }
        else Debug.Log("Empty");
    }
}
