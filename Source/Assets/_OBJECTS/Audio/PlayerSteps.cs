using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerSteps : MonoBehaviour
{

      [SerializeField]
      LayerMask layerMask;
      RaycastHit hit;

      [SerializeField]
      float material;

      private void OnDrawGizmos()
      {
            Gizmos.DrawRay(transform.position, Vector3.down);
      }

      public bool on = false;
      public void PlayPlayerSteps()
      {
        if (on)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, layerMask))
            {
                switch (hit.collider.gameObject.tag)
                {
                    case "Concrete":
                        material = 0;
                        break;
                    case "Steel":
                        material = 1;
                        break;
                    case "Gravel":
                        material = 2;
                        break;
                    case "Wet":
                        material = 3;
                        break;
                    default:
                        break;
                }

            }

            FMOD.Studio.EventInstance step = RuntimeManager.CreateInstance("event:/Player/Steps/Steps");
            step.setParameterByName("Material", material);
            step.start();
            step.release();
        }
      }
}
