using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameEnd : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.GameEnd_Event += StartEnd;
    }
    private void OnDisable()
    {
        EventManager.GameEnd_Event -= StartEnd;
    }

    bool ending = false;
    void StartEnd()
    {
        ending = true;
        startRot = Game.Get().Player.transform.localEulerAngles.y;
    }

    float startRot;
    public bool debug = false;
    private void Update()
    {
        if (debug)
        {
            StartCoroutine(End());
            debug = false;
        }
        if (ending)
        {
            float currentRot = Game.Get().Player.transform.localEulerAngles.y;
            float rot = currentRot - startRot;
            Debug.LogWarning(rot);
            if (rot <= -90 || rot >= 90)
            {
                StartCoroutine(End());
                ending = false;
            }
        }
    }

    public Volume volume;
    IEnumerator End()
    {
        GetComponent<AudioSource>().Play();

        Vignette vignette;

        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 100;
        }
        //Player Vinega-Animation
        //if Ended, end
        yield return new WaitWhile(() => GetComponent<AudioSource>().isPlaying);
        Game.End();
    }
}
