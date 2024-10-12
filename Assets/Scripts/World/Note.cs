using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    Inventory inventory;
    public SceneController transition;
    SaveInventory saveInventory;

    private void Awake()
    {
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        saveInventory = GameObject.Find("Scripts").GetComponent(typeof(SaveInventory)) as SaveInventory;
    }
    private void Update()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = !DialogueManager.instance.isShowed;
    }

    private void OnMouseDown()
    {
        saveInventory.SaveInventoryButton();
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.itsChanging && !DialogueManager.instance.isShowed)
        {
            transition.GetComponent<SceneController>().sceneId = 32;
            transition.GetComponent<SceneController>().ChangeScene();
        }
    }
}
