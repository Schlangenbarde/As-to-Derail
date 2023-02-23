using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityFX : MonoBehaviour 
{ 
    private BaseSanity sanity;
    [SerializeField]
    protected float currentSanity;


    protected virtual void Update()
    {
        if (sanity == null) sanity = Game.Get().Player.GetComponent<BaseSanity>();
        else currentSanity = sanity.GetCurrentSanity;
  
    }
}
