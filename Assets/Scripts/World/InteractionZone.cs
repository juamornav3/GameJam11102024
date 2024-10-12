using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionZone : MonoBehaviour
{
    public string currentInformativeText;
    public Vector3 textOffset;
    Inventory inventory;
    CursorManager cursorManager;
    TextMeshProUGUI informativeText;
    


    private void Awake()
    {
        cursorManager = GameObject.Find("Scripts").GetComponent(typeof(CursorManager)) as CursorManager;
        informativeText = GameObject.Find("InformativeText").GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
    }

    private void Update()
    {
        if (inventory.GetComponent<Inventory>().inventoryEnabled && DialogueManager.instance.isShowed) {
            OnMouseExit();
        }
    }


    private void OnMouseEnter()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !DialogueManager.instance.isShowed) { 
            cursorManager.ChangeCursor("actionCursor");

             Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + textOffset);

             Vector3 clampedPosition = new Vector3(
             Mathf.Clamp(screenPosition.x, 0f, Screen.width),
             Mathf.Clamp(screenPosition.y, 0f, Screen.height),
             screenPosition.z
            );

            informativeText.transform.position = clampedPosition;

            informativeText.text = currentInformativeText;
        }

    }


    private void OnMouseExit()
    {
        cursorManager.ChangeCursor("gameCursor");
        informativeText.text = "";
    }

}
