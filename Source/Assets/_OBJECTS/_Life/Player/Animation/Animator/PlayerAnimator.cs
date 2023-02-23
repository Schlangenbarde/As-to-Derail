using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    public enum PlayerAnimations { DODGE }
    public void StartAnimation(PlayerAnimations animation)
    {
        switch (animation)
        {
            case PlayerAnimations.DODGE:
                animator.SetTrigger("Dodge");
                break;
            default:
                Debug.LogWarning("Error you tried to use an unasigned Animation");
                break;
        }
    }

    public void SetupOverlappingAnimation()
    {
        GetComponent<CharacterController>().detectCollisions = false;
        GetComponent<Movement>().acceptInput = false;
        GetComponent<Pause>().SetToButIgnorCursor(true);
    }

    public void EndOverlappingAnimation()
    {
        GetComponent<CharacterController>().detectCollisions = true;
        GetComponent<Movement>().acceptInput = true;
        GetComponent<Pause>().SetToButIgnorCursor(false);
    }
}
