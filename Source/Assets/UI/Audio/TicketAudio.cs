using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketAudio : MonoBehaviour
{
      public void PlaySound(string path)
      {
            FMODUnity.RuntimeManager.PlayOneShot(path, transform.position);
      }
}
