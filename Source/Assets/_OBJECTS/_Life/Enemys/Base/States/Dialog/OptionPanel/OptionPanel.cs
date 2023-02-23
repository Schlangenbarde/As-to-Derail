using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "NewOptionPanel", menuName = "New/OptionPanel", order = 0)]
public class OptionPanel : ScriptableObject
{
    [TextArea(3,10)]
    public string text;
    public List<Option> options = new List<Option>();
    public EventReference audioClip;
}
