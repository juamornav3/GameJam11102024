using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSpeaker : MonoBehaviour
{
    public GameObject arrow;
    public GameObject arrow2;
    int slotsNumber = 0;
    TaskManager taskManager;
    public List<Coin> coinList;
    Inventory inventory;
    SceneController transition;

    private void Awake()
    {
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        slotsNumber = GameObject.Find("Scripts").GetComponent<Inventory>().slotHolder.transform.childCount;
        transition = GameObject.Find("Transition").GetComponent<SceneController>();
        taskManager = GameObject.Find("Scripts").GetComponent(typeof(TaskManager)) as TaskManager;
    }

    void Start()
    {
        if (!gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished)
        {
            StartCoroutine(WaitToStartDialogue(1));
        }
        if (gameObject.GetComponent<DialogueSpeaker>().conversations[2].finished)
        {
            arrow.GetComponent<Arrow>().isFree = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<Comentary>().enabled = true;
        }
    }

    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed && !gameObject.GetComponent<Comentary>().enabled)
        {
            gameObject.GetComponent<DialogueSpeaker>().Conver();
        }
          
    }

    void Update()
    {
        if ((coinList[0].isRecolected || coinList[1].isRecolected || coinList[2].isRecolected || coinList[3].isRecolected) && !gameObject.GetComponent<DialogueSpeaker>().conversations[1].unlocked)
        {
            gameObject.GetComponent<DialogueSpeaker>().conversations[1].unlocked = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(WaitToEndDialogue(1, 1, 1));

        }
        if (DialogueManager.instance.isShowed)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (gameObject.GetComponent<DialogueSpeaker>().conversations[2].finished)
        {
            gameObject.GetComponent<Comentary>().enabled = true;
            arrow.GetComponent<Arrow>().isFree = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            if (!taskManager.tasks[2].isActive)
            {
                taskManager.ShowCurrentTask(2);
            }
            if (HasItems())
            {
                arrow2.GetComponent<Arrow>().isFree = true;
                gameObject.GetComponent <BoxCollider2D>().enabled = false;
                if (!gameObject.GetComponent<DialogueSpeaker>().conversations[3].finished && !gameObject.GetComponent<DialogueSpeaker>().conversations[3].unlocked)
                {
                    gameObject.GetComponent<DialogueSpeaker>().conversations[3].unlocked = true;
                    gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 3;
                    StartCoroutine(WaitToStartDialogue(0.5f));
                }
            }
        }
    }

    private bool HasItems()
    {

        bool hasCard = false;
        for (int i = 0; i < slotsNumber; i++)
        {
            if (!inventory.slot[i].GetComponent<Slot>().empty)
            {

                if (inventory.slot[i].GetComponent<Slot>().type == "Card")
                {
                    hasCard = true;
                }
            }
        }

        return hasCard;
    }

    IEnumerator WaitToStartDialogue(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<DialogueSpeaker>().Conver();
    }

    IEnumerator WaitToEndDialogue(float duration,int conversation, int taskPosition)
    {
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(() => gameObject.GetComponent<DialogueSpeaker>().conversations[conversation].finished);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (!taskManager.tasks[taskPosition].isActive)
        {
            taskManager.ShowCurrentTask(taskPosition);
        }
        
      
    }
}
