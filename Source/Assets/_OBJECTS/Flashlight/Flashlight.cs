using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

      [SerializeField]
      private Look look;
      [SerializeField]
      private Waypoint waypoint;
      [SerializeField]
      private GameObject lightCone;


      [SerializeField]
      float distance;

      public Transform target;
      public float turnSpeed = .01f;
      Quaternion rotGoal;
      Vector3 direction;

      void Update()
      {
            var ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance))
            {
                  waypoint.transform.position = hit.point;
                  //StartCoroutine(UpdatePoint(hit));
            }


            direction = (target.position - transform.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            lightCone.transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
      }

      private void OnDrawGizmos()
      {
            Gizmos.DrawRay(transform.position, transform.forward * distance);
      }

      //IEnumerator UpdatePoint(RaycastHit hit)
      //{
      //      yield return new WaitForSeconds(0.5f);
      //      waypoint.transform.position = hit.point;
      //}
}
