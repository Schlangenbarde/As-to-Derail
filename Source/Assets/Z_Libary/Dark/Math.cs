using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dark
{
    public class Math
    {
        public static int ToPositive(int number)
        {
            if (number < 0) number *= -1;
            return number;
        }
        public static float ToPositive(float number)
        {
            if (number < 0) number *= -1;
            return number;
        }
        public static double ToPositive(double number)
        {
            if (number < 0) number *= -1;
            return number;
        }
        public static Vector3 ToPositive(Vector3 vec)
        {
            if (vec.x < 0) vec.x *= -1;
            if (vec.y < 0) vec.y *= -1;
            if (vec.z < 0) vec.z *= -1;

            return vec;
        }

        public static bool IsBetween(int x, int A, int B)
        {
            if (A <= x && x <= B) return true;
            return false;
        }

        public static bool IsBetween(float x, float A, float B)
        {
            if (A <= x && x <= B) return true;
            return false;
        }

        public static bool IsBetween(double x, double A, double B)
        {
            if (A <= x && x <= B) return true;
            return false;
        }

        public static bool IsBetween(Vector2 x, Vector2 A, Vector2 B)
        {
            if (x.x < A.x || x.x > B.x)
            {
                return false;
            }

            if (x.y < A.y || x.y > B.y)
            {
                return false;
            }

            return true;
        }

        public static bool AreBigger(Vector3 v, float x)
        {
            if (v.x <= x) return false;
            if (v.y <= x) return false;
            if (v.z <= x) return false;
            return true;
        }

        public static bool AnyBigger(Vector3 v, float x)
        {
            if (v.x > x) return true;
            if (v.y > x) return true;
            if (v.z > x) return true;
            return false;
        }

        public static bool AnySmaller(Vector3 v, float x)
        {
            if (v.x < x) return true;
            if (v.y < x) return true;
            if (v.z < x) return true;
            return false;
        }
        public static int GetType(int f)
        {
            if (f < 0)
            {
                return -1;
            }
            return 1;
        }

        public static int GetType(float f)
        {
            if (f < 0)
            {
                return -1;
            }
            return 1;
        }

        public static int GetType(double f)
        {
            if (f < 0)
            {
                return -1;
            }
            return 1;
        }

        public static Vector3 GetType(Vector3 v)
        {
            Vector3 vector3 = Vector3.one;
            if (v.x < 0)
            {
                vector3.x = -1;
            }
            else
            {
                vector3.x = 1;
            }

            if (v.y < 0)
            {
                vector3.y = -1;
            }
            else
            {
                vector3.y = 1;
            }

            if (v.z < 0)
            {
                vector3.z = -1;
            }
            else
            {
                vector3.z = 1;
            }

            return vector3;
        }

        public static Vector3 AddVector(Vector3 v, float f)
        {
            v.x += f;
            v.y += f;
            v.z += f;
            return v;
        }

        public static Vector3 SubVector(Vector3 v, float f)
        {
            v.x -= f;
            v.y -= f;
            v.z -= f;
            return v;
        }

        public static Vector3 MulVector(Vector3 v, float f)
        {
            {
                v.x *= f;
                v.y *= f;
                v.z *= f;
                return v;
            }
        }

        public static Vector3 DivVector(Vector3 v, float f)
        {
            {
                v.x /= f;
                v.y /= f;
                v.z /= f;
                return v;
            }
        }

        public static Vector3 ReverseDivVector(float f, Vector3 v)
        {
            {
                v.x = f/v.x;
                v.y = f/v.y;
                v.z = f/v.z;
                return v;
            }
        }

        public static int BoolToInt(bool b)
        {
            if (b == true) return 1;
            return 0;
        }

        public static bool IsBigger(Vector2 a, Vector2 b)
        {
            if (a.x > b.x)
            {
                return true;
            }

            if (a.y > b.y)
            {
                return true;
            }

            return false;
        }

        public static bool Is(Vector2 a, Vector2 b)
        {
            if (a.x == b.x)
            {
                return true;
            }

            if (a.y == b.y)
            {
                return true;
            }

            return false;
        }

        public static bool IsSmaller(Vector2 a, Vector2 b)
        {
            if (a.x < b.x)
            {
                return true;
            }

            if (a.y < b.y)
            {
                return true;
            }

            return false;
        }

        public static bool IsBiggerEqual(Vector2 a, Vector2 b)
        {
            if (IsBigger(a, b) == true || Is(a, b) == true)
            {
                return true;
            }
            return false;
        }

        public static bool IsSmallerEqual(Vector2 a, Vector2 b)
        {
            if (IsSmaller(a, b) == true || Is(a, b) == true)
            {
                return true;
            }
            return false;
        }
    }
}
