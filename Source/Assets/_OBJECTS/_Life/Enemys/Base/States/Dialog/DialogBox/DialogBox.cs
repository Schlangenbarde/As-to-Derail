using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI textElement;

    [SerializeField]
    Transform options;
    [SerializeField]
    float awnserShowTime = 3f;

    [SerializeField]
    GameObject owner;

    OptionPanel lastDialogPoint;

    bool dialogFinished = false;
    string finishedText;
    OptionPanel endingPanel;

    private void Start()
    {
        endingPanel = ScriptableObject.CreateInstance<OptionPanel>();
    }
    public void StartDialog(OptionPanel startPanel)
    {
        Show();

        if (dialogFinished && !string.IsNullOrEmpty(finishedText))
        {
            SetDialog(endingPanel);
            textElement.text = finishedText;
        }
        else
        {
            if (lastDialogPoint == null)
            {
                SetDialog(startPanel);
            }
            else
            {
                SetDialog(lastDialogPoint);
            }
        }
        
    }

    public IEnumerator NextPanel(string awnser, OptionPanel nextPanel)
    {
        if (false == string.IsNullOrEmpty(awnser))
        {
            textElement.text = awnser;
            yield return new WaitForSeconds(awnserShowTime);
        }

        SetDialog(nextPanel);
        
    }

    void SetDialog(OptionPanel panel)
    {
        textElement.text = panel.text;
        for (int i = 0; i < options.childCount; i++)
        {
            OptionButton optionButton = options.GetChild(i).GetComponent<OptionButton>();

            if(i < panel.options.Count)
            {
                optionButton.SetOption(panel.options[i], owner);
                optionButton.UpdateOption();
            }
            else
            {
                optionButton.gameObject.SetActive(false);
            }
        }
        lastDialogPoint = panel;
    }

    public void EndDialogAbrupt()
    {
        Hide();
    }
    public IEnumerator EndDialog(string endingText)
    {
        if (false == string.IsNullOrEmpty(endingText))
        {
            textElement.text = endingText;
            yield return new WaitForSeconds(awnserShowTime);
        }
        dialogFinished = true;
        finishedText = endingText;
        EndDialogAbrupt(); 
    }

    public void Show()
    {
        GetComponent<Image>().enabled = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        GetComponent<Image>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public GameObject GetOwner()
    {
        return owner;
    }

}
