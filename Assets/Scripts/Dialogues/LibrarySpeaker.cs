using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarySpeaker : MonoBehaviour
{
    public PuzzleData puzzleData;
    public GameObject laptop;
    public GameObject transition;
    public GameObject lector;
    Inventory inventory;
    TaskManager taskManager;
    SaveInventory saveInventory;
    private void Awake()
    {
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        taskManager = GameObject.Find("Scripts").GetComponent(typeof(TaskManager)) as TaskManager;
        saveInventory = GameObject.Find("Scripts").GetComponent(typeof(SaveInventory)) as SaveInventory;
    }

    void Start()
    {
        transition.GetComponent<SceneController>().duration = 3;
        if (!gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished)
        {
            StartCoroutine(WaitToChangeScene());

        }else if (!gameObject.GetComponent<DialogueSpeaker>().conversations[1].unlocked)
        {
            gameObject.GetComponent<DialogueSpeaker>().conversations[1].unlocked = true;
            gameObject.GetComponent<DialogueSpeaker>().Conver();
        }else if (puzzleData.finished)
        {
            if (!taskManager.tasks[2].isCompleted)
            {
                taskManager.tasks[2].isCompleted = true;
                taskManager.ShowCurrentTask(2);
            }
            
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            lector.transform.position = new Vector3(lector.transform.position.x, lector.transform.position.y, -1f);
            lector.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    void Update()
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
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !laptop.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            if (!puzzleData.finished)
            {
                laptop.GetComponent<SceneController>().ChangeScene();
                saveInventory.SaveInventoryButton();
            }
        }
    }

    IEnumerator WaitToChangeScene()
    {
        gameObject.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(()=> gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished);
        transition.GetComponent<SceneController>().duration = 1;
        transition.GetComponent<SceneController>().ChangeScene();
    }
}
