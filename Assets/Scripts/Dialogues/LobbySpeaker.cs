using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbySpeaker : MonoBehaviour
{
    public GameObject arrow;
    public PuzzleData puzzleData;
    public GameObject laptop;
    Inventory inventory;


    private void Awake()
    {
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
    }
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        arrow.GetComponent<Arrow>().isFree = false;
        if (gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished == false)
        {
            StartCoroutine(WaitToStartDialogue(1));
        }
        if (puzzleData.finished)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            arrow.GetComponent<Arrow>().isFree = true;
        }
        
    }

    private void Update()
    {
        if (DialogueManager.instance.isShowed)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            if (!puzzleData.finished)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                if (gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished && !laptop.GetComponent<SceneController>().itsChanging)
                {
                    gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished = false;
                    laptop.GetComponent<SceneController>().ChangeScene();
                }
            }
        }  
       
    }
    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !laptop.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            if (!puzzleData.finished)
            {
                gameObject.GetComponent<DialogueSpeaker>().Conver();
            }
        }
    }
    IEnumerator WaitToStartDialogue(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<DialogueSpeaker>().Conver();
    }


    

}
