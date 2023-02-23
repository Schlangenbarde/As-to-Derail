using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dark
{
    public class Sound : MonoBehaviour
    {
        public static void PlayAudio(AudioSource source, AudioClip clip)
        {
            print("Play Audio");
            source.clip = clip;
            source.Play();
        }
    }
}
