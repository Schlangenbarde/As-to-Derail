using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    GameObject itemInHands;
    public GameObject ItemInHands => itemInHands;

    [SerializeField]
    Transform holdingPoint;
    public Transform HoldingPoint => holdingPoint;

    public void Hold(GameObject item)
    {
        if (item.TryGetComponent(out Gravity gravity))
        {
            gravity.Deactivate();
            item.GetComponent<CharacterController>().enabled = false;

            gravity.StopImpuls();
        }

        if (item.TryGetComponent(out BoxCollider collider))
        {
            collider.enabled = false;
        }

        item.transform.parent = holdingPoint.transform;
        item.transform.position = holdingPoint.transform.position;
        item.transform.rotation = transform.rotation;
        itemInHands = item;
    }

    public void Drop(RaycastHit hit, Ray ray)
    {
        itemInHands.transform.parent = itemInHands.GetComponent<Interactable>().OldParent;

        itemInHands.transform.position = hit.point + new Vector3(0, itemInHands.GetComponent<BoxCollider>().size.y/2 * itemInHands.transform.localScale.y, 0);

        if (itemInHands.TryGetComponent(out Gravity gravity))
        {
            itemInHands.GetComponent<CharacterController>().enabled = true;
            gravity.Ativate();
        }

        if (itemInHands.TryGetComponent(out BoxCollider collider))
        {
            collider.enabled = true;
        }

        itemInHands = null;
    }

    public void UseItem()
    {
        Destroy(itemInHands);
        itemInHands = null;
    }
}
