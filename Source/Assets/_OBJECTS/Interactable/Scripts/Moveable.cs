using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dark;

public class Moveable : MonoBehaviour
{
      [HideInInspector]
      public bool moveable = false;

      public float checkBoxSizeExtra;
      public BoxCollider checkBox;

      private void Awake()
      {
            checkBox.size = Vector3.one + Math.ReverseDivVector(checkBoxSizeExtra, transform.localScale);
      }

      private void OnTriggerEnter(Collider other)
      {
            if (other.transform == Game.Get().Player.transform)
            {
                  moveable = true;
            }
      }

      private void OnTriggerExit(Collider other)
      {
            if (other.transform == Game.Get().Player.transform)
            {
                  moveable = false;
            }
      }

      public void StartMoving(Transform mover)
      {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Pickup", transform.position);
            transform.parent = mover;
      }

      public void EndMoving()
      {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Putdown", transform.position);
            transform.parent = null;
      }
}
