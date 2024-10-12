using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarnivalBuhonero : MonoBehaviour
{
    public PuzzleData puzzleData;
    public PuzzleData minigame;
    public PuzzleData secondgame;
    public Coin coin;
    public PlayerData playerData;
    public GameObject laptop;
    public GameObject transition;
    public DialogueSpeaker dialogue;
    public GameObject uniform;
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
        
        if (puzzleData.finished)
        {
            if (!minigame.finished)
            {
                dialogue.conversationIndex = 5;   
                dialogue.conversations[5].question.choices[0].result.finished = false;
                dialogue.Conver();
            }
            else
            {
                
                if (!dialogue.conversations[6].finished)
                {
                    dialogue.conversationIndex = 6;
                    dialogue.Conver();
                }
                
                uniform.transform.position = new Vector3(uniform.transform.position.x, uniform.transform.position.y, -2f);
                uniform.gameObject.GetComponent<BoxCollider2D>().enabled = true;

                if (secondgame.finished && !dialogue.conversations[8].finished)
                {
                    dialogue.conversationIndex = 8;
                    dialogue.Conver();
                }
            }

        }

    }

    private void Update()
    {
        if (dialogue.conversations[1].finished)
        {
            if (dialogue.conversations[1].finished && !taskManager.tasks[4].isActive)
            {
                taskManager.ShowCurrentTask(4);
            }
            else if (dialogue.conversations[6].finished && !taskManager.tasks[4].isCompleted)
            {
                taskManager.tasks[4].isCompleted = true;
                taskManager.ShowCurrentTask(4);
            }else if (dialogue.conversations[8].finished && !coin.isRecolected)
            {
                coin.isRecolected = true;
                playerData.cont += 6;
                
            }
            if (DialogueManager.instance.isShowed || transition.GetComponent<SceneController>().itsChanging || laptop.GetComponent<SceneController>().itsChanging)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                if (!puzzleData.finished && dialogue.conversations[1].finished)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    if (dialogue.conversations[4].finished && !laptop.GetComponent<SceneController>().itsChanging)
                    {
                        dialogue.conversations[4].finished = false;
                        laptop.GetComponent<SceneController>().sceneId = 10;
                        laptop.GetComponent<SceneController>().ChangeScene();
                    }
                }
                else if (!minigame.finished)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    if (dialogue.conversations[5].question.choices[0].result.finished && !transition.GetComponent<SceneController>().itsChanging)
                    {
                        transition.GetComponent<SceneController>().sceneId = 11;
                        transition.GetComponent<SceneController>().ChangeScene();
                    }
                }else if (!secondgame.finished)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    if (dialogue.conversations[7].question.choices[0].result.finished && !transition.GetComponent<SceneController>().itsChanging)
                    {
                        transition.GetComponent<SceneController>().sceneId = 35;
                        transition.GetComponent<SceneController>().ChangeScene();
                    }
                }
            }

        }

    }
    private void OnMouseDown()
    {
        saveInventory.SaveInventoryButton();
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !laptop.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            if (!puzzleData.finished)
            {
                dialogue.conversationIndex = 4;
                dialogue.Conver();
            }else if (!minigame.finished)
            {
                dialogue.conversationIndex = 5;
                dialogue.Conver();
            }else if (!secondgame.finished)
            {
                dialogue.conversationIndex = 7;
                dialogue.Conver();
            }
        }
    }
}
