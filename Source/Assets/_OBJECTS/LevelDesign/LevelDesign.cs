using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    public Transform PlayerSpawnPoint;

    private void Start()
    {
        if (PlayerSpawnPoint != null)
        {
            Game.Get().Player.transform.position = PlayerSpawnPoint.position;
        }
    }
}
