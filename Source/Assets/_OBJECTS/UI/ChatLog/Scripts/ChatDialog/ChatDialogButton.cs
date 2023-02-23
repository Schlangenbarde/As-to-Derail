using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dark;
using FMODUnity;

public class ChatDialogButton : MonoBehaviour
{
    private Option optionData;
    private GameObject textElement;

    [SerializeField]
    private ChatDialogBox chat;

    FMOD_Plugins.Instance instance;

    Option setOption
    {
        set
        {
            optionData = value;
        }
    }

    private void Awake()
    {
        textElement = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        FMOD_Plugins.CheckInstanceEnded(instance);
    }

    public void SetOption(Option optionData, GameObject owner)
    {
        this.setOption = optionData;
        this.optionData.SetOwner(owner);
    }

    public bool UpdateOption()
    {
        if (optionData.AbleToAcces())
        {
            gameObject.SetActive(true);
            textElement.GetComponent<TextMeshProUGUI>().text = optionData.previewOptionText;
            return true;
        }
        else
        {
            gameObject.SetActive(false);
            return false;
        }
    }

    public void Pressed()
    {
        DoPress();
    }

    void DoPress()
    {
        chat.AddText(Game.Get().Player, optionData.optionText);
        foreach (Transform button in chat.options)
        {
            button.GetChild(0).GetComponent<TextMeshProUGUI>().text = "...";
            button.GetComponent<Button>().interactable = false;
        }

        instance = FMOD_Plugins.CreateInstance(optionData.optionAudioClip,  Game.Get().Player);
        FMOD_Plugins.LinkToInstance(instance, DoPress_End);
        FMOD_Plugins.PlayInstance(instance);
    }

    void DoPress_End()
    {
        optionData.Consequenz();

        if (gameObject.activeSelf)
        {
            if (null == optionData.nextPanel)
            {
                chat.EndDialog(optionData.awnserText, optionData.awnserAudioClip);
            }
            else
            {
                chat.NextPanel(optionData.awnserText, optionData.awnserAudioClip, optionData.nextPanel);
            }
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
