using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodHandler : MonoBehaviour
{
    public Texture2D bloodTexture;

    public List<Transform> meshParents;

    private void Awake()
    {
        if (meshParents != null)
        {
            foreach (Transform parent in meshParents)
            {
                if (parent != null)
                {
                    foreach (Transform child in parent)
                    {
                        if (child != null)
                        {
                            if (child.TryGetComponent(out SurfaceShaderInstancing instance))
                            {
                                instance.BloodTexture = bloodTexture;
                            }
                        }
                    }
                }
            }
        }
    }
}
