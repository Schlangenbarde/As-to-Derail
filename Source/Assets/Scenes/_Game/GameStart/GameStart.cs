using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameStart : MonoBehaviour
{
    PlayableDirector director;

    public GameObject dialogObject;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        ChatLog chat = Game.Get().chatLog;
        Phone phone = chat.phone;
        phone.SwitchToChatLog();
        
        chat.StartDialog(dialogObject.GetComponent<Dialog>().profilePic, dialogObject);

        Game.Get().ESC_Active = false;
    }
     
    public void StartGame()
    {
        Game.Get().ESC_Active = true;
        director.Play();
    }

    [SerializeField]
    PlayableDirector mapHelpTimeline;
    public void BlendInMapHelp()
    {
        mapHelpTimeline.Play();
    }

    public void End()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        EventManager.PlayerNegativImpulse_Event += BlendInMapHelp;
    }

    private void OnDisable()
    {
        EventManager.PlayerNegativImpulse_Event -= BlendInMapHelp;
    }
}
