using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SanityCircle : MonoBehaviour
{
    [SerializeField]
    List<SanityCircleLayers> layers = new List<SanityCircleLayers>();

    int counter;
    int sanityAmount;
    int lastCounter;
    public static Action<float> increaseSanity;
    public static Action<float> decreaseSanity;



    private void Update()
    {
        counter = 0;
        foreach (var layer in layers)
        {
            if (layer.IsPlayerInside) counter++;
            else counter--;
            counter = Mathf.Clamp(counter, 0, 3);
        }

        if (lastCounter != counter)
        {
            switch (counter)
            {
                case 0:
                    RemoveAmount(sanityAmount);
                    sanityAmount = 0;
                    break;

                case 1:
                    RemoveAmount(sanityAmount);
                    sanityAmount = 2;
                    AddAmount(sanityAmount);
                    break;

                case 2:
                    RemoveAmount(sanityAmount);
                    sanityAmount = 3;
                    AddAmount(sanityAmount);
                    break;

                case 3:
                    RemoveAmount(sanityAmount);
                    sanityAmount = 4;
                    AddAmount(sanityAmount);
                    break;

                default:
                    break;
            }
        }
        lastCounter = counter;
    }

    void AddAmount(int amount)
    {
        increaseSanity?.Invoke(amount);
    }

    void RemoveAmount(int amount)
    {
        decreaseSanity?.Invoke(amount);
    }
}
