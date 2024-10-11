using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dartboard : MonoBehaviour
{
    private Animator animator;
    CursorManager cursorManager;
    private ActiveSpeaker activeDialogue;
    private DialogueSpeaker dialogueSpeaker;
    public AudioClip destroyed;
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        cursorManager = GameObject.Find("Scripts").GetComponent(typeof(CursorManager)) as CursorManager;
        activeDialogue = GameObject.Find("Speaker").GetComponent(typeof(ActiveSpeaker)) as ActiveSpeaker;
        dialogueSpeaker = GameObject.Find("Speaker").GetComponent(typeof(DialogueSpeaker)) as DialogueSpeaker;
    }


   

   
    private void OnMouseDown()
    {
        if (!DialogueManager.instance.isShowed && dialogueSpeaker.conversations[0].finished)
        {
            if (destroyed != null)
            {
                SoundManager.instance.playSound(destroyed, 1f);
            }
            animator.SetTrigger("Destroy");
            StartCoroutine(WaitForDestruction());
        }
        
    }
    public void OnMouseEnter()
    {
        if (!DialogueManager.instance.isShowed && dialogueSpeaker.conversations[0].finished)
        {
            
            activeDialogue.EnterDialogue();
            
        }
        
    }

   
    IEnumerator WaitForDestruction()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        activeDialogue.nextTutorialStep();
        cursorManager.ChangeCursor("gameCursor");
    }
}
