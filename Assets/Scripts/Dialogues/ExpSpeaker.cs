using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExpSpeaker : MonoBehaviour
{
    TaskManager taskManager;
    Inventory inventory;
    int slotsNumber = 0;
    public PuzzleData data;
    public SceneController transition;
    public GameObject coment;
    SaveInventory saveInventory;

    private void Awake()
    {

        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        taskManager = GameObject.Find("Scripts").GetComponent(typeof(TaskManager)) as TaskManager;
        slotsNumber = GameObject.Find("Scripts").GetComponent<Inventory>().slotHolder.transform.childCount;
        saveInventory = GameObject.Find("Scripts").GetComponent(typeof(SaveInventory)) as SaveInventory;
    }
    void Start()
    {
        if (gameObject.GetComponent<DialogueSpeaker>().conversations[2].question.choices[0].result.finished && !gameObject.GetComponent<DialogueSpeaker>().conversations[3].finished)
        {
            gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 3;
            gameObject.GetComponent<DialogueSpeaker>().Conver();
        }
        else
        {
            if (!gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished)
            {
                gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 0;
                StartCoroutine(WaitToEndDialogue(0f, 0, 7));
            }
            else if (data.finished && !gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished)
            {
                gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 1;
                StartCoroutine(WaitToEndDialogue(0f, 1, 7));
            }
        }
        
    }
    private void Update()
    {
        
        gameObject.GetComponent<BoxCollider2D>().enabled = !DialogueManager.instance.isShowed && !gameObject.GetComponent<DialogueSpeaker>().conversations[3].finished;
        coment.GetComponent<BoxCollider2D>().enabled = !HasItems();

    }

    private void OnMouseDown()
    {
        saveInventory.SaveInventoryButton();
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.itsChanging && !DialogueManager.instance.isShowed)
        {
            if (HasItems() && !gameObject.GetComponent<DialogueSpeaker>().conversations[3].finished)
            {
                gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 2;
                StartCoroutine(WaitToInitVideo());
            }
                
        }
    }

    private bool HasItems()
    {

        bool hasKey = false;
        for (int i = 0; i < slotsNumber; i++)
        {
            if (!inventory.slot[i].GetComponent<Slot>().empty)
            {

                if (inventory.slot[i].GetComponent<Slot>().type == "SKey")
                {
                    hasKey = true;
                }
            }
        }

        return hasKey;
    }


    IEnumerator WaitToInitVideo()
    {
        gameObject.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(() => gameObject.GetComponent<DialogueSpeaker>().conversations[2].question.choices[0].result.finished);
        transition.GetComponent<SceneController>().sceneId = 33;
        transition.GetComponent<SceneController>().ChangeScene();
    }
    IEnumerator WaitToEndDialogue(float duration, int conversation, int taskPosition)
    {
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(() => gameObject.GetComponent<DialogueSpeaker>().conversations[conversation].finished);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (!taskManager.tasks[taskPosition].isActive)
        {
            taskManager.ShowCurrentTask(taskPosition);
        }
        if (!taskManager.tasks[taskPosition].isCompleted)
        {
            taskManager.tasks[taskPosition].isCompleted = true;
            taskManager.ShowCurrentTask(taskPosition);
            yield return new WaitForSeconds(4);
            taskManager.ShowCurrentTask(8);
        }


    }
}
