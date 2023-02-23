using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New/Train/Data")]
public class TrainData : ScriptableObject
{
    [SerializeField]
    private GameObject waypoints;

    [SerializeField]
    private List<GameObject> encounter;

    public List<GameObject> Encounter => encounter;
    public GameObject WayPoints => waypoints;


}
