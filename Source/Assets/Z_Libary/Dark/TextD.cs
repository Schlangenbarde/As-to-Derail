using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dark
{
    public class TextD : MonoBehaviour
    {
        public static string DeleteChar(char c, string s)
        {
            string newString = "";
            foreach (char cha in s)
            {
                if(cha != c)
                {
                    newString += cha;
                }
            }
            return newString;
        }
    }
}
