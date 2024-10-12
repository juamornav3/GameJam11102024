using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public event EventHandler OnOpen;
    public event EventHandler OnClose;
    public AudioClip openSuitCaseAudio;
    public bool inventoryEnabled;
    public GameObject inventory;
    public GameObject image;
    public int allSlots;
    public GameObject slotHolder;
    public GameObject[] slot;
    public GameObject suitcaseIcon;
    public Sprite iconOpen;
    public Sprite iconClosed;
    public GameObject suitcase;
    public GameObject panel;
    public List<SceneController> transitions = new List<SceneController>();
    SaveInventory inventorySave;
    bool itsChanging=false;

    private void Awake()
    {
        
        inventorySave = GameObject.Find("Scripts").GetComponent(typeof(SaveInventory)) as SaveInventory;
        for (int i = 0; i < transitions.Count; i++)
        {
            transitions[i].OnChange += DisabledInventory;
        }
    }

    void Start()
    {   
        StartCoroutine(InitializeSlots());
    }

    private void DisabledInventory(object sender, EventArgs e)
    {
        for (int i = 0; i < transitions.Count; i++)
        {
            if (transitions[i].itsChanging)
            {    
                itsChanging = true;
                break;
            }
            else
            {
               itsChanging=false;
            }
        }
        

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !itsChanging &&!DialogueManager.instance.isShowed)
        {
            
            suitcase.GetComponent<Animator>().SetTrigger(inventoryEnabled ? "Close" : "Open");
            slotHolder.GetComponent<Animator>().SetTrigger(inventoryEnabled ? "Close" : "Open");
            panel.GetComponent<Animator>().SetTrigger(inventoryEnabled ? "Close" : "Open");
            inventoryEnabled = !inventoryEnabled;
            if (inventoryEnabled)
            {
                OnOpen?.Invoke(this, EventArgs.Empty);
                SoundManager.instance.SetMasterVolume(0.45f);
                SoundManager.instance.playSound(openSuitCaseAudio, 1f);
            }
            else
            {
                OnClose?.Invoke(this, EventArgs.Empty);
                SoundManager.instance.SetMasterVolume(1f);
                SoundManager.instance.playSound(openSuitCaseAudio, 0.45f);
            }
            
            
        }

        if (inventoryEnabled)
        {
            
            suitcaseIcon.GetComponent<Image>().sprite = iconOpen;
           
            image.SetActive(true);



        }
        else
            {
            suitcaseIcon.GetComponent<Image>().sprite = iconClosed;
            
            
            image.SetActive(false);


        }
    }
    public void ToggleInventory()
    {
        suitcase.GetComponent<Animator>().SetTrigger(inventoryEnabled ? "Close" : "Open");
        slotHolder.GetComponent<Animator>().SetTrigger(inventoryEnabled ? "Close" : "Open");
        panel.GetComponent<Animator>().SetTrigger(inventoryEnabled ? "Close" : "Open");
        inventoryEnabled = !inventoryEnabled;
        
        if (inventoryEnabled)
        {
            OnOpen?.Invoke(this, EventArgs.Empty);
            suitcaseIcon.GetComponent<Image>().sprite = iconOpen;
            SoundManager.instance.SetMasterVolume(0.45f);
            SoundManager.instance.playSound(openSuitCaseAudio, 1f);
            
            image.SetActive(true);

        }
        else
            {
            OnClose?.Invoke(this, EventArgs.Empty);
            suitcaseIcon.GetComponent<Image>().sprite =iconClosed;
            SoundManager.instance.SetMasterVolume(1f);
            SoundManager.instance.playSound(openSuitCaseAudio, 0.45f);
            image.SetActive(false);

        }
    }

    public void AddItem(int itemId, string itemType, string itemDescription, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                
                slot[i].GetComponent<Slot>().id = itemId;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().description = itemDescription;
                slot[i].GetComponent<Slot>().icon = itemIcon;

                

                slot[i].GetComponent<Slot>().UpdateSlot();

                slot[i].GetComponent<Slot>().empty = false;
                return;
            }
           
        }
    }

    IEnumerator InitializeSlots()
    {
        allSlots = slotHolder.transform.childCount;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
            if (slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }

        yield return null;

        inventorySave.LoadInventoryButton();
    }

}


