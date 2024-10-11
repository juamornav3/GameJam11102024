using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChangeScene : MonoBehaviour
{
    Inventory inventory;
    public SceneController transition;

    public void Awake()
    {
        inventory = GameObject.Find("Scripts").GetComponent<Inventory>();
    }

    private void OnMouseDown()
    {
        if (!inventory.inventoryEnabled && !transition.itsChanging)
        {
            transition.ChangeScene();
        }
    }
}
