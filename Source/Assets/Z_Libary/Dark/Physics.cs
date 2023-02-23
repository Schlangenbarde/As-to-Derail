using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dark
{
    public class Physics
    {
        public class RayCastInfo
        {
            public RaycastHit hit;

            public RayCastInfo(RaycastHit hit)
            {
                this.hit = hit;
            }
        }

        public static bool AIsInRangeOfB(Vector3 posX, Vector3 posY, float range)
        {
            float distance = Vector3.Distance(posX, posY);
            if (distance <= range) return true;
            return false;
        }

        public static Vector3 GetDirectionFromAToB(Vector3 posA, Vector3 posB)
        {
            return (posB - posA).normalized;
        }

        public static RayCastInfo RayCastFromAToB(Vector3 posA, Vector3 posB, float length = 10f, int layer = -1)
        {
            Vector3 direction = GetDirectionFromAToB(posA, posB);
            Ray ray = new Ray(posA, direction);
            
            if(UnityEngine.Physics.Raycast(ray, out RaycastHit hit, length, layer))
            {
                return new RayCastInfo(hit);
            }
            return null;
        }

        public static bool RayCastCheckFromAToB(Vector3 posA, Vector3 posB, float length = 10f, int layer = 0)
        {
            Vector3 direction = GetDirectionFromAToB(posA,posB);
            Ray ray = new Ray(posA, direction);

            return UnityEngine.Physics.Raycast(ray, length, layer);
        }

        public static void Vector3ToFloat(ref Vector3 vector3, ref float f0, ref float f1, ref float f2)
        {
            f0 = vector3.x;
            f1 = vector3.y;
            f2 = vector3.z;
        }

        public static Vector3 Vector3Filter(Vector3 filter, Vector3 vector3)
        {
            Vector3 vector = Vector3.zero;
            vector.x = filter.x * vector3.x;
            vector.y = filter.y * vector3.y;
            vector.z = filter.z * vector3.z;

            return vector;
        }

    }
}
