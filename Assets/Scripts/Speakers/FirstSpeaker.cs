using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSpeaker : MonoBehaviour
{

    public DialogueSpeaker speaker;

    private void OnMouseDown()
    {
       if(speaker != null && speaker.conversationIndex<speaker.conversations.Count)
        {
            speaker.Conver();
        } 
    }
}
