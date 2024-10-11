using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    SceneController transition;
    Inventory inventory;
    SaveInventory saveInventory;
    public AudioClip sound;
    public bool isFree = true;
    public int id;
    private void Awake()
    {
        transition = GameObject.Find("Transition").GetComponent<SceneController>();
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        saveInventory = GameObject.Find("Scripts").GetComponent(typeof(SaveInventory)) as SaveInventory;
    }

    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        if (inventory.GetComponent<Inventory>().inventoryEnabled)
        {
            Start();
        }
    }

    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            saveInventory.SaveInventoryButton();
            transition.ChangeSceneSound(sound, id);
        }
            
    }
}
