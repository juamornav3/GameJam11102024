using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPuzzle6 : MonoBehaviour
{
    TaskManager taskManager;
    Inventory inventory;
    public GameObject laptop;
    public PuzzleData data;
    int slotsNumber = 0;

    public int idItem;
    public string typeItem;
    public string descriptionItem;
    public Sprite iconItem;
    private void Awake()
    {
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        taskManager = GameObject.Find("Scripts").GetComponent(typeof(TaskManager)) as TaskManager;
        slotsNumber = GameObject.Find("Scripts").GetComponent<Inventory>().slotHolder.transform.childCount;
    }

    private void Update()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = !DialogueManager.instance.isShowed;
        gameObject.GetComponent<Comentary>().enabled = data.finished;
        if (data.finished && !HasItem())
        {
            int id = idItem;
            string type = typeItem;
            string description = descriptionItem;
            Sprite icon = iconItem;

            inventory.AddItem(id, type, description, icon);
        }
    }


    private bool HasItem()
    {

        bool hasCard = false;
        for (int i = 0; i < slotsNumber; i++)
        {
            if (!inventory.slot[i].GetComponent<Slot>().empty)
            {

                if (inventory.slot[i].GetComponent<Slot>().type == typeItem)
                {
                    hasCard = true;
                    break;
                }
            }
        }

        return hasCard;
    }

    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !laptop.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed && !gameObject.GetComponent<Comentary>().enabled)
        {
            laptop.GetComponent<SceneController>().ChangeScene();
        }
    }
}
