using UnityEngine;

public class LightEvent : MonoBehaviour
{
    [SerializeField]
    TicketAudio ticketAudio;

    bool isPlayerInsideEventCollider;
    bool wasAlreadySawn;

    private void Update()
    {
        if (IsInsideCameraFrustum(Camera.main))
        {
            Debug.Log("INsied");
        }

        if (!wasAlreadySawn && isPlayerInsideEventCollider && IsInsideCameraFrustum(Camera.main))
        {
            Debug.Log("kfahfjaoifjaojdpiasjdlajslkdja");
            FMODUnity.RuntimeManager.PlayOneShot("event:/Music/Jumpscare", Game.Get().Player.transform.position);
            wasAlreadySawn = true;
        }
        else if (wasAlreadySawn && isPlayerInsideEventCollider && IsInsideCameraFrustum(Camera.main))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Movement>()) isPlayerInsideEventCollider = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Movement>()) isPlayerInsideEventCollider = false;
        
    }

    bool IsInsideCameraFrustum(Camera cam)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        var point = transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }

        }
        return true;
    }
}
