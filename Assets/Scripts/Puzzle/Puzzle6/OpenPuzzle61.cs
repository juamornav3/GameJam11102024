using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPuzzle61: MonoBehaviour
{
    TaskManager taskManager;
    Inventory inventory;
    public GameObject laptop;
    public PuzzleData data;

   
    private void Awake()
    {
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        taskManager = GameObject.Find("Scripts").GetComponent(typeof(TaskManager)) as TaskManager;
    }

    private void Update()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = !DialogueManager.instance.isShowed;
    
       
    }

    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !laptop.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            laptop.GetComponent<SceneController>().ChangeScene();
        }
    }
}
