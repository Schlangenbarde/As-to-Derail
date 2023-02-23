using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    BaseSanity sanity;

    private void OnEnable()
    {
        CheckForCreatures.monsterInside += IncreaseNegativeSanityByDistance;
        CheckForCreatures.monsterOutside += DecreaseNegativeSanityByDistance;

        CheckForGoodThings.goodThingIsInBackground += IncreasePositiveSanityByDistance;
        CheckForGoodThings.noGoodThing += DecreasePositiveSanityByDistance;

        BaseTrain.playerInsideTrain += IncreaseNegativeSanityByDistance;
        BaseTrain.playerLeftTrain += DecreaseNegativeSanityByDistance;

        Vendingmachine.playerWasOnvending += IncreaseSanity;

        SanityCircle.increaseSanity += IncreaseNegativeSanityByDistance;
        SanityCircle.decreaseSanity += DecreaseNegativeSanityByDistance;
    }

    private void OnDisable()
    {
        CheckForCreatures.monsterInside -= IncreaseNegativeSanityByDistance;
        CheckForCreatures.monsterOutside -= DecreaseNegativeSanityByDistance;

        CheckForGoodThings.goodThingIsInBackground -= IncreasePositiveSanityByDistance;
        CheckForGoodThings.noGoodThing -= DecreasePositiveSanityByDistance;

        BaseTrain.playerInsideTrain -= IncreaseNegativeSanityByDistance;
        BaseTrain.playerLeftTrain -= DecreaseNegativeSanityByDistance;

        Vendingmachine.playerWasOnvending -= IncreaseSanity;

        SanityCircle.increaseSanity -= IncreaseNegativeSanityByDistance;
        SanityCircle.decreaseSanity -= DecreaseNegativeSanityByDistance;
    }
    
    void IncreaseSanity(float value)
    {
        sanity.IncreaseSanity(value);
    }

    void DecreaseNegativeSanityByDistance(float value)
    {
        sanity.DecreaseSanityNegativMultiplicator(value);
    }

    void IncreaseNegativeSanityByDistance(float value)
    {
        sanity.IncreaseSanityNegativMuliplicator(value);
    }

    void DecreasePositiveSanityByDistance(float value)
    {
        sanity.DecreaseSanityPositiveMultiplicator(value);
    }

    void IncreasePositiveSanityByDistance(float value)
    {
        sanity.IncreaseSanityPositiveMuliplicator(value);
    }

}
