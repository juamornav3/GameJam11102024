using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CorridorSpeaker : MonoBehaviour
{
    public GameObject secretDoor;
    int slotsNumber = 0;
    Inventory inventory;
    public Arrow arrow;
    TaskManager taskManager;
    public GameObject soldiers;

    private void Awake()
    {
        slotsNumber = GameObject.Find("Scripts").GetComponent<Inventory>().slotHolder.transform.childCount;
        inventory=GameObject.Find("Scripts").GetComponent<Inventory>();
        taskManager = GameObject.Find("Scripts").GetComponent(typeof(TaskManager)) as TaskManager;
    }

    void Start()
    {

        secretDoor.SetActive(gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished);
        if (!gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished)
        {
            gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 0;
            StartCoroutine(WaitForConver(3,0.5f));
        }
        

    }

    private void Update()
    {
        secretDoor.SetActive(gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished);
        soldiers.SetActive(!gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished);
        if (HasItems())
        {
            arrow.isFree = true;
            if (!gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished && !gameObject.GetComponent<DialogueSpeaker>().conversations[1].unlocked)
            {
                gameObject.GetComponent<DialogueSpeaker>().conversations[1].unlocked = true;
                gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 1;
                taskManager.tasks[3].isCompleted = true;
                StartCoroutine(WaitForConver(3,0.5f));
            }
        }

    }

    private bool HasItems()
    {
        
        bool hasUniform = false;
        for (int i = 0; i < slotsNumber; i++)
        {
            if (!inventory.slot[i].GetComponent<Slot>().empty)
            {
                
                if (inventory.slot[i].GetComponent<Slot>().type == "Uniform")
                {
                    hasUniform=true;
                }
            }
        }

        return hasUniform;
    }

    IEnumerator WaitForConver(int task,float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(() => !DialogueManager.instance.isShowed);
        taskManager.ShowCurrentTask(task);
    }

}
