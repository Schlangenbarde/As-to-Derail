using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SanitySFX : SanityFX
{
      FMOD.Studio.EventInstance heartbeat;
      FMOD.Studio.EventInstance screeching;

      private void Awake()
      {
            heartbeat = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Sanity/Heartbeat");
            screeching = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Sanity/Screeching");
            heartbeat.start();
            screeching.start();
      }

      protected override void Update()
      {
            base.Update();
            ChangeSanitySFX(currentSanity);
      }

      public void ChangeSanitySFX(float sanity)
      {
            float snt = 1 - (sanity / 100);
            heartbeat.setParameterByName("Sanity", snt);
            screeching.setParameterByName("Sanity", snt);
      }


}
