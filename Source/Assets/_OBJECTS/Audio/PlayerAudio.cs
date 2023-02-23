using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
      [SerializeField]
      private PlayerSteps playerSteps;

      public void PlayPlayerSteps()
      {
            playerSteps.PlayPlayerSteps();
      }

}
