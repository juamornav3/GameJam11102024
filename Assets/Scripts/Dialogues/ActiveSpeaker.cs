using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSpeaker : MonoBehaviour
{
    public GameObject speaker;
    public GameObject gift;
    public GameObject suitCaseIcon;
    public GameObject suitCase;
    public GameObject scripts;
    public GameObject arrow;
    public GameObject shoe;


    public int cont = 0;
    int active = 0;


    void Start()
    {
        cont = 0;
        StartCoroutine(ActiveFirstTask(1));
        scripts.GetComponent<Inventory>().enabled = false;
        scripts.GetComponent<Shoe>().enabled = false;
       
    }

    
    private void ActivateDialog(object sender, EventArgs e)
    {

        StartCoroutine(WaitToNextTutorialStep(0.1f));
        scripts.GetComponent<Inventory>().OnOpen -= ActivateDialog;
        scripts.GetComponent<Inventory>().OnClose += ActiveDialogue2;
        

    }

    private void ActiveDialogue2(object sender,EventArgs e)
    {

        speaker.GetComponent<DialogueSpeaker>().Conver();
        StartCoroutine(WaitEndConversation(2, 5));
        arrow.GetComponent<Arrow>().isFree = true;
        shoe.SetActive(true);
        scripts.GetComponent<Inventory>().OnClose -= ActiveDialogue2;
       
    }

   


    public void EnterDialogue()
    {
        active++;
        if (active<=1)
        {
            StartCoroutine(WaitToNextTutorialStep(0.25f));
        }
    }

    public void nextTutorialStep()
    {
        cont++;
        if (cont == 4)
        {
            scripts.GetComponent<TaskManager>().tasks[0].isCompleted = true;
            scripts.GetComponent<TaskManager>().ShowCurrentTask(0);
            StartCoroutine(WaitToNextTutorialStep(2));
            StartCoroutine(WaitEndConversation(1,2));
            StartCoroutine(ActiveGift());
        }
    }

    public void TakeGift()
    {
        scripts.GetComponent<TaskManager>().tasks[1].isCompleted = true;
        scripts.GetComponent<TaskManager>().ShowCurrentTask(1);
        StartCoroutine(WaitToNextTutorialStep(0.5f));
        StartCoroutine(WaitToEndConversationGift(3));
        
        if (scripts.GetComponent<Inventory>().enabled)
        {
            scripts.GetComponent<Inventory>().OnOpen += ActivateDialog;
        }
    }


    IEnumerator ActiveGift()
    {
        yield return new WaitUntil(() => speaker.GetComponent<DialogueSpeaker>().conversations[2].finished == true);
        gift.SetActive(true);
        suitCase.SetActive(true);
        scripts.GetComponent<Inventory>().enabled = true;
        scripts.GetComponent<Shoe>().enabled = true;
        suitCaseIcon.SetActive(false);
    }
    IEnumerator WaitEndConversation(int task, int conversation)
    {
        yield return new WaitUntil(() => speaker.GetComponent<DialogueSpeaker>().conversations[conversation].finished == true);
        scripts.GetComponent<TaskManager>().ShowCurrentTask(task);
    }
    IEnumerator WaitToNextTutorialStep(float duration) { 
        yield return new WaitForSeconds(duration);
        speaker.GetComponent<DialogueSpeaker>().Conver();

    }

    IEnumerator WaitToEndConversationGift(int conversation)
    {
        yield return new WaitUntil(() => speaker.GetComponent<DialogueSpeaker>().conversations[conversation].finished == true);
        suitCaseIcon.SetActive(true);
    }

    IEnumerator ActiveFirstTask(float duration)
    {
        yield return new WaitForSeconds(duration);
        speaker.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(() => speaker.GetComponent<DialogueSpeaker>().conversations[0].finished==true && !scripts.GetComponent<TaskManager>().tasks[0].isActive && !scripts.GetComponent<TaskManager>().tasks[0].isCompleted);
        scripts.GetComponent<TaskManager>().ShowCurrentTask(0);
        

    }
}
