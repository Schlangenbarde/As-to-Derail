using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionButton : MonoBehaviour
{
    private Option optionData;
    private GameObject textElement;

    [SerializeField]
    private DialogBox dialogBox;

    private void Awake()
    {
        textElement = transform.GetChild(0).gameObject;
    }

    public void SetOption(Option optionData, GameObject owner)
    {
        this.optionData = optionData;
        this.optionData.SetOwner(owner);
    }

    public void UpdateOption()
    {
        if (optionData.AbleToAcces())
        {
            gameObject.SetActive(true);
            textElement.GetComponent<TextMeshProUGUI>().text = optionData.optionText;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Pressed()
    {
        optionData.Consequenz();

        if (gameObject.activeSelf == false) return;

        if(null == optionData.nextPanel)
        {
            StartCoroutine(dialogBox.EndDialog(optionData.awnserText));
        }
        else
        {
            StartCoroutine(dialogBox.NextPanel(optionData.awnserText, optionData.nextPanel));
        }
    }

    public void Show()
    {
        foreach (var component in GetComponents<MonoBehaviour>())
        {
            if (component != this) component.enabled = true;
        }

        textElement.SetActive(true);
    }

    public void Hide()
    {
        foreach (var component in GetComponents<MonoBehaviour>())
        {
            if (component != this) component.enabled = false;
        }
        textElement.SetActive(false);
    }
}
