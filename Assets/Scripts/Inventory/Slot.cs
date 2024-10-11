using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public int id;
    public string type;
    public string description;
    public bool empty;
    public Sprite icon;

   
    public Vector3 Offset;
    public Transform slotIconGameObject;
    GameObject map;
    Inventory inventory;
    GameObject inspectionPanel;
    RectTransform canvasRectTransform;
    public AudioClip inspectAudio;
    GameObject taskManager;


    private void Awake()
    {
        canvasRectTransform = GameObject.Find("Inventory").GetComponent<RectTransform>();
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        inspectionPanel = GameObject.Find("Inspection");
        taskManager = GameObject.Find("TaskList");
        if (GameObject.Find("Map"))
        {
            map = GameObject.Find("Map");
        }
        else
        {
            map = null;
        }

    }

    private void Start()
    {
        slotIconGameObject = transform.GetChild(0);
        inspectionPanel.SetActive(false);
       
        
    }
    public void UpdateSlot()
    {
        slotIconGameObject.GetComponent<Image>().sprite = icon;
    }

    void Update()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled)
        {
            inspectionPanel.SetActive(false);
        }
    }

    public void InspectItem()
    {
        SoundManager.instance.playSound(inspectAudio,1f);
        inspectionPanel.SetActive(true);
        inspectionPanel.transform.GetChild(0).GetComponent<Image>().sprite = icon;
        inspectionPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = description;

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(!empty)
        {
            InspectItem();
        }
        else
        {
            inspectionPanel.SetActive(false);
        }
        if (map != null)
        {
            map.SetActive(false);
        }
        taskManager.SetActive(false);
    }

    
}
