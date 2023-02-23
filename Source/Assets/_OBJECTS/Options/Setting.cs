using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textElement;

    public float value;
    public void UpdateValue()
    {
        textElement.text = name + ": " + GetComponent<Slider>().value.ToString("F2");
        value = GetComponent<Slider>().value;
    }

    public void UpdateUI()
    {
        textElement.text = name + ": " + value.ToString("F2");
        GetComponent<Slider>().value = value;
    }
}
