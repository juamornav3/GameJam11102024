using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ScrapSpeaker : MonoBehaviour
{
    TaskManager taskManager;
    GameObject transition;
    public GameObject arrow1;
    public GameObject arrow2;
    public PuzzleData puzzleData;
    public PuzzleData level3;
    public GameObject laptop;
    Inventory inventory;
    int slotsNumber = 0;
    SaveInventory saveInventory;


    private void Awake()
    {
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        transition = GameObject.Find("Transition");
        slotsNumber = GameObject.Find("Scripts").GetComponent<Inventory>().slotHolder.transform.childCount;
        taskManager = GameObject.Find("Scripts").GetComponent<TaskManager>();
        saveInventory = GameObject.Find("Scripts").GetComponent(typeof(SaveInventory)) as SaveInventory;
    }
    void Start()
    {
        if (level3.finished)
        {
            arrow2.GetComponent<Arrow>().isFree = true;
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        arrow1.GetComponent<Arrow>().isFree = false;
        if (gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished)
        {
            if (!gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished) 
            {
                StartCoroutine(WaitToStartTransition(1, 20));
            }else if (!gameObject.GetComponent<DialogueSpeaker>().conversations[2].finished)
            {
                StartCoroutine(WaitToTask(2,6));
            }
        }
        if (puzzleData.finished)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            if (!taskManager.tasks[6].isCompleted)
            {
                taskManager.tasks[6].isCompleted = true;
                taskManager.ShowCurrentTask(6);
            }

            arrow1.GetComponent<Arrow>().isFree = true;
        }

    }

    private void Update()
    {
        gameObject.GetComponent<Comentary>().enabled = gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished && !HasItems();
        gameObject.GetComponent<BoxCollider2D>().enabled = !DialogueManager.instance.isShowed && !puzzleData.finished;

        if (puzzleData.finished && !HasCard())
        {
            int id = 13;
            string type = "Card2";
            string description = "Tarjeta de acceso de nivel 2";
            Sprite icon = Resources.Load<Sprite>("Tarjeta2");

            inventory.AddItem(id, type, description, icon);
        }


    }
    private void OnMouseDown()
    {
        saveInventory.SaveInventoryButton();
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !laptop.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            if (!gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished)
            {
                StartCoroutine(WaitToStartTransition(0,19));
            }else if (HasItems() && !puzzleData.finished)
            {
                laptop.GetComponent<SceneController>().ChangeScene();
            }

        }
    }

    private bool HasItems()
    {

        int hasCard = 0;
        for (int i = 0; i < slotsNumber; i++)
        {
            if (!inventory.slot[i].GetComponent<Slot>().empty)
            {

                if (inventory.slot[i].GetComponent<Slot>().type == "Key")
                {
                    hasCard +=1;
                }
            }
        }

        return hasCard==3;
    }

    private bool HasCard()
    {

        bool hasCard = false;
        for (int i = 0; i < slotsNumber; i++)
        {
            if (!inventory.slot[i].GetComponent<Slot>().empty)
            {

                if (inventory.slot[i].GetComponent<Slot>().type == "Card2")
                {
                    hasCard = true;
                    break;
                }
            }
        }

        return hasCard;
    }
    IEnumerator WaitToStartTransition(int conver, int sceneID)
    {
        
        gameObject.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(()=> gameObject.GetComponent<DialogueSpeaker>().conversations[conver].finished);
        transition.GetComponent<SceneController>().sceneId = sceneID;
        transition.GetComponent<SceneController>().ChangeScene();
    }

    IEnumerator WaitToTask(int conver, int task)
    {
        gameObject.GetComponent<DialogueSpeaker>().conversationIndex=conver;
        gameObject.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(() => gameObject.GetComponent<DialogueSpeaker>().conversations[conver].finished);
        taskManager.tasks[5].isCompleted = true;
        taskManager.ShowCurrentTask(5);
        yield return new WaitForSeconds(3);
        taskManager.ShowCurrentTask(task);
        
    }

}
