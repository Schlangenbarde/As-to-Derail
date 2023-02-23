using UnityEngine;
using FMODUnity;

public class FModSoundTest : MonoBehaviour
{
    public EventReference sound;
    FMOD_Plugins.Instance instance;
    void Start()
    {
        instance = FMOD_Plugins.CreateInstance(sound, gameObject);
        FMOD_Plugins.LinkToInstance(instance, PrintDebug);
        FMOD_Plugins.PlayInstance(instance);
    }

    void PrintDebug()
    {
        Debug.LogWarning("Audio Ended!");
    }

    private void Update()
    {
        FMOD_Plugins.CheckInstanceEnded(instance);
    }
}
