using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CarnivalSpeaker : MonoBehaviour
{
    Inventory inventory;
    SceneController transition;
    DialogueSpeaker speaker;
    public int idConversation;


    private void Awake()
    {
        inventory = GameObject.Find("Scripts").GetComponent<Inventory>();
        transition = GameObject.Find("Transition").GetComponent<SceneController>();
        speaker = GameObject.Find("Speaker").GetComponent<DialogueSpeaker>();
    }

    private void Update()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = !DialogueManager.instance.isShowed;
    }

    public void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed && speaker.conversations[1].finished)
        {
            speaker.conversationIndex = idConversation;
            speaker.Conver();
        }
    }

}
