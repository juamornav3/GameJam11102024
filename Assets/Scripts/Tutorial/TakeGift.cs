using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TakeGift : MonoBehaviour
{
    CursorManager cursorManager;
    public AudioClip pickUp;
    TextMeshProUGUI informativeText;
    Inventory inventory;
    SceneController transition;
    ActiveSpeaker activeSpeaker;
 

    private void Awake()
    {
     
        informativeText = GameObject.Find("InformativeText").GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        cursorManager = GameObject.Find("Scripts").GetComponent(typeof(CursorManager)) as CursorManager;
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        transition = GameObject.Find("Laptop").GetComponent(typeof(SceneController)) as SceneController;
        activeSpeaker = GameObject.Find("Speaker").GetComponent(typeof(ActiveSpeaker)) as ActiveSpeaker;
    }

    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            if (pickUp != null)
            {
                SoundManager.instance.playSound(pickUp,0.65f);
            }
            GameObject itemPickedUp = gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            gameObject.GetComponent<PickUpItemText>().ActiveCanvasObject(item.icon);
            inventory.AddItem( item.id, item.type, item.description, item.icon);
            informativeText.text = "";
            cursorManager.ChangeCursor("gameCursor");
            item.pickedUP = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            activeSpeaker.TakeGift();

        }
        
    }
}
