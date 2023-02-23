using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dark;
using UnityEngine.UI;
using FMODUnity;

public class ChatDialogBox : MonoBehaviour
{
    public GameObject messageBoxPrefab;
    public Transform messages;

    public Transform options;

    GameObject owner;

    [SerializeField]
    ChatLog chatLog;

    public ScrollRect scrollRect;

    FMOD_Plugins.Instance instance = null;

    private void Update()
    {
        FMOD_Plugins.CheckInstanceEnded(instance);
    }

    public void StartDialog(GameObject owner)
    {
        print("StartDialog");
        this.owner = owner;
        Dialog dialog = owner.GetComponent<Dialog>();
        OptionPanel startPanel = dialog.startPanel;

        if (dialog.dialogFinished && !string.IsNullOrEmpty(dialog.finishedText))
        {
            dialog.endingPanel.text = dialog.finishedText;
            dialog.endingPanel.audioClip = dialog.finishedAudio;
            ShowEnding(dialog.endingPanel);
        }
        else
        {
            if (dialog.lastDialogPoint == null)
            {
                print("With: " + startPanel);
                SetDialog(startPanel);
            }
            else
            {
                print("With: " + dialog.lastDialogPoint);
                SetDialog(dialog.lastDialogPoint);
            }
        }

    }
    void AddText(string text)
    {
        AddText(owner, text);
    }
    public void AddText(GameObject gameObject, string text)
    {
        GameObject message = Instantiate(messageBoxPrefab);
        message.transform.SetParent(messages, false);

        message.transform.localScale = Vector3.one;

        string checkedText = CommandCharracterCheck(text);


        string Tname = "<color=#be974e>" + gameObject.name + ": <color=#fefeb6>";
        message.GetComponent<MessageBox>().mainText.text = Tname + checkedText;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.GetComponent<RectTransform>());
        StartCoroutine(SnapToBottom());
    }

    string CommandCharracterCheck(string text)
    {
        string cout = "";

        for (int index = 0; index < text.Length; index++)
        {
            if (text[index] == '§')
            {
                string returnValue = "//Command Not Found//";
                switch (text[index + 1])
                {
                    case '0':
                        returnValue = Game.Get().pathRenderer.GetDirection();
                        break;
                    case '1':
                        returnValue = Game.Get().pathRenderer.GetDirectionU();
                        break;
                }
                index++;
                cout += returnValue;
            }
            else
            {
                cout += text[index];
            }
        }
        return cout;
    }

    IEnumerator SnapToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0;
    }
    OptionPanel bufferPanel;
    string bufferString;
    EventReference bufferAudio;
    public void NextPanel(string awnser, EventReference audio, OptionPanel nextPanel)
    {
        print("Next Panel");
        bufferPanel = nextPanel;
        if (false == string.IsNullOrEmpty(awnser))
        {
            AddText(awnser);
            instance = FMOD_Plugins.CreateInstance(audio, owner);
            FMOD_Plugins.LinkToInstance(instance, NextPanel_End);
            FMOD_Plugins.PlayInstance(instance);
        }
        else
        {
            NextPanel_End();
        }
    }

    void NextPanel_End()
    {
        Debug.LogWarning("NextPanel");
        SetDialog(bufferPanel);
    }

    void ShowEnding(OptionPanel panel)
    {
        AddText(panel.text);

        for (int i = 0; i < options.childCount; i++)
        {
            ChatDialogButton optionButton = options.GetChild(i).GetComponent<ChatDialogButton>();
            optionButton.gameObject.SetActive(false);
        }

        instance = FMOD_Plugins.CreateInstance(panel.audioClip, owner);
        bufferPanel = panel;
        FMOD_Plugins.LinkToInstance(instance, ShowEnding_End);
        FMOD_Plugins.PlayInstance(instance);
    }

    void ShowEnding_End()
    {
        EndDialogAbrupt();
    }

    void SetDialog(OptionPanel panel)
    {
        print("SetDialog");
        AddText(panel.text);

        instance = FMOD_Plugins.CreateInstance(panel.audioClip, owner);
        bufferPanel = panel;
        FMOD_Plugins.LinkToInstance(instance, SetDialog_End);
        FMOD_Plugins.PlayInstance(instance);
    }

    void SetDialog_End()
    {
        if (bufferPanel.options.Count == 0)
        {
            EndDialogAbrupt();
            return;
        }

        int activeOptions = 0;
        ChatDialogButton autoAwnser = null;
        Debug.LogWarning("SetDialog");
        for (int i = 0; i < options.childCount; i++)
        {
            ChatDialogButton optionButton = options.GetChild(i).GetComponent<ChatDialogButton>();

            if (i < bufferPanel.options.Count)
            {
                optionButton.SetOption(bufferPanel.options[i], owner);
                if (optionButton.UpdateOption() == true)
                {
                    activeOptions++;

                    if (autoAwnser == null)
                    {
                        autoAwnser = optionButton;
                    }
                }
            }
            else
            {
                optionButton.gameObject.SetActive(false);
            }
        }

        owner.GetComponent<Dialog>().lastDialogPoint = bufferPanel;
        foreach (Transform button in options)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }

    public void EndDialogAbrupt()
    {
        chatLog.StopDialogAbruptly();
    }
    public void EndDialog(string endingText, EventReference audio)
    {
        Debug.LogWarning("EndDialog");
        bufferString = endingText;
        bufferAudio = audio;
        if (false == string.IsNullOrEmpty(endingText))
        {
            AddText(endingText);
            instance = FMOD_Plugins.CreateInstance(audio, owner);
            FMOD_Plugins.LinkToInstance(instance, EndDialog_End);
            FMOD_Plugins.PlayInstance(instance);
        }
        else
        {
            EndDialog_End();
        }

    }

    void EndDialog_End()
    {
        Dialog dialog = owner.GetComponent<Dialog>();
        dialog.dialogFinished = true;
        dialog.finishedText = bufferString;
        dialog.finishedAudio = bufferAudio;

        EndDialogAbrupt();
    }

    public GameObject GetOwner()
    {
        return owner;
    }

}
