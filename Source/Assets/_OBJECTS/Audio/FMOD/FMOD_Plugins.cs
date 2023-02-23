using UnityEngine;

namespace FMODUnity
{
    public class FMOD_Plugins
    {
        public delegate void DoAfterAudio_Blueprint();
        public DoAfterAudio_Blueprint DoAfterAudio_Var;

        public class Instance
        {
            public DoAfterAudio_Blueprint func;
            public FMOD.Studio.EventInstance instance;
        }

        public static void CheckInstanceEnded(Instance instance)
        {
            if (instance == null) return;
            if (instance.instance.isValid())
            {
                instance.instance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
                if (state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                {
                    instance.instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    instance.instance.release();
                    instance.func();
                    instance = null;
                }
                
            }
        }

        public static Instance CreateInstance(EventReference eventRef, GameObject gameObject)
        {
            FMOD.Studio.EventInstance instance;
            instance = RuntimeManager.CreateInstance(eventRef);

            instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));

            Instance realInstance = new();
            realInstance.instance = instance;

            return realInstance;
        }

        public static void LinkToInstance(Instance instance, DoAfterAudio_Blueprint func)
        {
            instance.func = func;
        }

        public static void PlayInstance(Instance instance)
        {
            instance.instance.start();
        }
    }
}
