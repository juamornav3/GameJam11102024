using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSpeaker : MonoBehaviour
{
    public DialogueSpeaker speaker;
    private SceneController sceneController;

    private void Awake()
    {
        sceneController = FindFirstObjectByType<SceneController>();
    }

    private void OnMouseDown()
    {
        if (speaker != null && speaker.conversationIndex < speaker.conversations.Count)
        {
            speaker.Conver();
        }
    }

    
    void Update()
    {
        if (speaker.conversations[0].finished && !sceneController.itsChanging)
        {
            sceneController.ChangeScene();
        }
    }
}
