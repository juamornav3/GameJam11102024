using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public static DialogueSpeaker actualSpeaker;
   
    DialogueUI dialUI;

    
    public bool isShowed;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }

        dialUI = FindFirstObjectByType<DialogueUI>();
      
        ShowUI(false);

    }

    void Start()
    {
        ShowUI(false);
    }

    
    public void ShowUI(bool show)
    {
        dialUI.gameObject.SetActive(show);
        isShowed = show;
        if (!show)
        {
            dialUI.localIndex = 0;
        }
    }

    public void SetConversation (Conversation conv,DialogueSpeaker speaker)
    {
        if(speaker != null)
        {
            actualSpeaker = speaker;
        }
        else
        {
            GetConversation(conv,0,0);
        }
        if(conv.finished && !conv.reuse)
        {
            GetConversation(conv, conv.dialogues.Length, 1);
        }
        else
        {
            GetConversation(conv, actualSpeaker.dialLocalIn, 0);
        }
    }

    public void GetConversation(Conversation conv, int id, int textLine)
    {
        dialUI.conversation = conv;
        dialUI.localIndex = id;
        dialUI.UpdateTexts(textLine);
    }

    public void ChaneReuseState(Conversation conv, bool state)
    {
        conv.reuse = state;
    }

    public void LockConversation(Conversation conv, bool unlock)
    {
        conv.unlocked = unlock;
    }
}
