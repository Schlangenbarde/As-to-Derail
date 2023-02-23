using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SanityVFX : SanityFX
{
    [SerializeField]
    private Volume volume;
    private LensDistortion lensDistortion;
    private FilmGrain filmGrain;
    private ChromaticAberration chromaticAberration;

    [SerializeField]
    private float lensDistortionMultiplier = 0.5f;
    [SerializeField]
    private float grainMultiplier = 1f;
    [SerializeField]
    private float chromaticAberrationMultiplier = 0.5f;

    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private Transform cameraTransform;
    private Vector3 originalPos;

    [SerializeField]
    private float shakeAmount;

    private void Awake()
    {
        cameraTransform = mainCam.transform;
    }

    private void Start()
    {
        //lensDistortion = volume.profile
        //chromaticAberration = GetComponent<ChromaticAberration>();
        //filmGrain = GetComponent<FilmGrain>();
        volume.profile.TryGet<LensDistortion>(out lensDistortion);
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
        volume.profile.TryGet<FilmGrain>(out filmGrain);

        if(lensDistortion == null)
        {
            Debug.Log("lensDistortion wasn't found");
        }
    }

    protected override void Update()
    {
        base.Update();
        float mySanity = (currentSanity - 0) / (100 - 0);
        currentSanity = Mathf.Clamp(currentSanity, 0, 100);
        mySanity = Mathf.Clamp(mySanity, 0, 1);
        //mySanity = 1 - mySanity / 100;
        ChangeSanityVFX(mySanity);
        CameraShake(mySanity);
    }


    void ChangeSanityVFX(float sanity)
    {
        
        //Debug.Log(sanity);
        lensDistortion.intensity.value = 0 - (sanity * lensDistortionMultiplier);
        filmGrain.intensity.value = sanity * grainMultiplier;
        chromaticAberration.intensity.value = sanity * chromaticAberrationMultiplier;
    }

    private void CameraShake(float sanity)
    {
        if (sanity > 0.5)
        {
            cameraTransform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount * (sanity - 0.5f);
        }
    }

}
