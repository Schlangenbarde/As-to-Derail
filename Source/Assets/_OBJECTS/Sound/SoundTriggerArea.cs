using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dark;

public class SoundTriggerArea : MonoBehaviour
{
    [SerializeField]
    Transform audioSourcesParent;

    List<AudioSource> sources = new List<AudioSource>();

    int currentAudioClip = 0;

    [SerializeField]
    List<AudioClip> audioClips;

    [SerializeField]
    bool oneTimeOnly = true;

    int timesTriggered = 0;

    [SerializeField]
    EventManager.Event eventToTrigger;

    private void Awake()
    {
        foreach (Transform sourceTransform in audioSourcesParent)
        {
            sources.Add(sourceTransform.GetComponent<AudioSource>());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (oneTimeOnly)
        {
            if (timesTriggered > 0)
            {
                return;
            }
        }

        timesTriggered++;
        for (int i = 0; i < sources.Count; i++)
        {
            Sound.PlayAudio(sources[i], UseAudioClip());
        }
        StartCoroutine(WaitForAudiosToStop(sources));
    }

    IEnumerator WaitForAudiosToStop(List<AudioSource> audioSources)
    {
        foreach (var audioSource in audioSources)
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        EventManager.PlayEvent(eventToTrigger);
    }

    AudioClip UseAudioClip()
    {
        AudioClip clip = audioClips[currentAudioClip];
        currentAudioClip++;
        if (currentAudioClip > audioClips.Count -1)
        {
            currentAudioClip = 0;
        }

        return clip;
    }
}
