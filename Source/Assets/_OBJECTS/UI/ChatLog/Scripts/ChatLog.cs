using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatLog : MonoBehaviour
{
    public LayoutElement chatSize;
    public RectTransform chatScrollBar;

    public Image profilePic;

    public TextMeshProUGUI text;

    public Phone phone;
    public GameObject options;

    public ChatDialogBox chat;

    float chatMaxSize = 0f;

    private void Awake()
    {
        chatMaxSize = chatSize.minHeight;
    }
    public void StartDialog(Sprite profileRef, GameObject owner)
    {
        phone.SwitchToChatLog();

        profilePic.gameObject.SetActive(true);
        profilePic.sprite = profileRef;

        chatSize.minHeight = chatMaxSize / 2;
        chatScrollBar.sizeDelta = new Vector2(chatScrollBar.sizeDelta.x, chatSize.minHeight);

        options.SetActive(true);

        chat.StartDialog(owner);
    }

    public void CleanUpDialog()
    {
        profilePic.gameObject.SetActive(false);
        profilePic.sprite = null;

        chatSize.minHeight = chatMaxSize;
        RectTransform tf = chatSize.gameObject.GetComponent<RectTransform>();
        tf.sizeDelta = new Vector2(tf.sizeDelta.x, chatSize.minHeight);

        chatScrollBar.sizeDelta = new Vector2(chatScrollBar.sizeDelta.x, chatSize.minHeight);

        options.SetActive(false);
        chat.StopAllCoroutines();

        foreach (Transform button in options.transform)
        {
            button.GetComponent<ChatDialogButton>().StopAllCoroutines();
        }
        StopAllCoroutines();
        print("CleanUpDialog");
    }

    public void StopDialogAbruptly()
    {
        chat.GetOwner().GetComponent<Dialog>().SetDialogStatus(false);
        phone.Close();
    }
}
