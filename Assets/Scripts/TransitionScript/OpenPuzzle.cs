using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenPuzzle : MonoBehaviour
{
    SceneController laptop;
    public string itemType;
    public int quantity;
    Inventory inventory;
    SaveInventory saveInventory;
    int slotsNumber = 0;

    private void Awake()
    {
        laptop = GameObject.Find("Laptop").GetComponent<SceneController>();
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as  Inventory;
        slotsNumber = inventory.GetComponent<Inventory>().slotHolder.transform.childCount;
        saveInventory = GameObject.Find("Scripts").GetComponent(typeof(SaveInventory)) as SaveInventory;
    }

    private void OnMouseDown()
    {
        if (hasItems())
        {
            Cursor.visible = true;
            saveInventory.SaveInventoryButton();
            laptop.ChangeScene();
        }

    }

    private bool hasItems()
    {
        int cont = 0;
        for (int i = 0; i < slotsNumber; i++)
        {
            if (!inventory.slot[i].GetComponent<Slot>().empty)
            {
                if (inventory.slot[i].GetComponent<Slot>().type == itemType)
                {
                    cont++;
                }
            }
        }

        return cont == quantity;
    }


}
