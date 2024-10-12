using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivalManager : MonoBehaviour
{
    Inventory inventory;
    SceneController transition;
    DialogueSpeaker speaker;
    public int idConversation;
    public GameObject panel;
    public AudioClip sound;
    public GameObject buhonero;

    private void Awake()
    {
        inventory = GameObject.Find("Scripts").GetComponent<Inventory>();
        transition = GameObject.Find("Transition").GetComponent<SceneController>();
        speaker = GameObject.Find("Speaker").GetComponent<DialogueSpeaker>();
    }

    private void Start()
    {
        panel.SetActive(!speaker.conversations[0].finished);
        if (!speaker.conversations[0].finished)
        {
           StartCoroutine(TurnOnLights());  
        }
        else
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
            if (!speaker.conversations[1].finished)
            {
                buhonero.GetComponent<BoxCollider2D>().enabled = false; 
                speaker.conversationIndex = 1;
                StartCoroutine(WaitConver());
            }
        }
    }

    IEnumerator TurnOnLights()
    {
        speaker.Conver();
        yield return new WaitUntil(() => speaker.conversations[0].finished);
        transition.ChangeSceneSound(sound, 9);
    }

    IEnumerator WaitConver()
    {
        yield return new WaitForSeconds(1f);
        speaker.Conver();
        yield return new WaitUntil(() => speaker.conversations[1].finished);
        buhonero.GetComponent<BoxCollider2D>().enabled = true;
    }
}
