using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceShaderInstancing : MonoBehaviour
{
    public Texture2D MainTexture;
    public Texture2D BloodTexture;
    public float Bloodiness;
    public float BloodStrength;
    [Range(0, 0.499f)]
    public float BloodHeightTop;
    [Range(0, 0.499f)]
    public float BloodHeightBottom;
    public bool ShowTop;
    public bool ShowBottom;

    void Start()
    {
        GetComponent<MeshRenderer>().material.SetTexture("_MainTexture", MainTexture);
        GetComponent<MeshRenderer>().material.SetTexture("_BloodTexture", BloodTexture);
        GetComponent<MeshRenderer>().material.SetFloat("_Bloodiness", Bloodiness);
        GetComponent<MeshRenderer>().material.SetFloat("_BloodStrength", BloodStrength);
        GetComponent<MeshRenderer>().material.SetFloat("_BloodHeightTop", BloodHeightTop);
        GetComponent<MeshRenderer>().material.SetFloat("_BloodHeightBottom", BloodHeightBottom);
        GetComponent<MeshRenderer>().material.SetInt("_ShowTop", Dark.Math.BoolToInt(ShowTop));
        GetComponent<MeshRenderer>().material.SetInt("_ShowBottom", Dark.Math.BoolToInt(ShowBottom));
    }
}
