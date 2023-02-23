using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class TrainSounds : MonoBehaviour
{
      [SerializeField]
      string path;

      FMOD.Studio.EventInstance sound;
      //[SerializeField]
      //GameObject[] doors;

      //void OpenDoors()
      //{
      //      foreach (var door in doors)
      //      {
      //            PlaySound("event:/SFX/DoorsOpen", door.transform);
      //      }
      //}

      //void CloseDoors()
      //{
      //      foreach (var door in doors)
      //      {
      //            PlaySound("event:/SFX/DoorsClose", door.transform);
      //      }
      //}

      //public void PlaySound(string path, Transform tfm)
      //{
      //      FMODUnity.RuntimeManager.PlayOneShot(path, tfm.position);
      //}

      public void PlaySound(string path)
      {
           // FMODUnity.RuntimeManager.PlayOneShot(path, transform.position);
           PlaySoundInstance(path);
      }
      public void PlaySoundInstance(string path)
      {
            sound = FMODUnity.RuntimeManager.CreateInstance(path);
            sound.start();
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, GetComponent<Transform>(), GetComponent<Rigidbody>());
      }
}
