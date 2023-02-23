using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dark
{
    public class TransformD
    {
        static public void SetChildsActive(Transform parent, bool newState)
        {
            foreach (Transform child in parent)
            {
                child.gameObject.SetActive(newState);
            }
        }

        static public void SetChildsActive(Transform parent, bool newState, List<GameObject> ignoreList)
        {
            foreach (Transform child in parent)
            {
                if (false == GameObjectIsInList(child.gameObject, ignoreList))
                {
                    child.gameObject.SetActive(newState);
                }
            }
        }

        static public bool GameObjectIsInList(GameObject target, List<GameObject> list)
        {
            foreach (var element in list)
            {
                if (target == element)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
