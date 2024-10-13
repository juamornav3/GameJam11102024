using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveSimulatorSpeaker : MonoBehaviour
{

    public DialogueSpeaker speaker;
    LoveSimulatorManager manager;

    private void Awake()
    {
        manager = gameObject.GetComponent<LoveSimulatorManager>();
    }
    private void OnMouseDown()
    {
        if (speaker != null && speaker.conversationIndex < speaker.conversations.Count)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            manager.speaker = true;
            speaker.Conver();
        }
    }

   
}
