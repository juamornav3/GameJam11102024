using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectorSpeaker : MonoBehaviour
{
    
    

        public DialogueSpeaker speaker;
    private SceneController sceneController;

    private void Awake()
    {
        sceneController = FindAnyObjectByType<SceneController>();
    }

    private void OnMouseDown()
        {
            if (speaker != null && speaker.conversationIndex < speaker.conversations.Count)
            {
                speaker.Conver();
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

    private void Update()
    {
        if (speaker.conversations[0].finished)
        {
            speaker.conversations[0].finished = false;
            sceneController.ChangeScene();
        }
    }

}
