using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabSpeaker : MonoBehaviour
{


    TaskManager taskManager;
    private void Awake()
    {
        taskManager = GameObject.Find("Scripts").GetComponent(typeof(TaskManager)) as TaskManager;
    }

    void Start()
    {
        gameObject.GetComponent<DialogueSpeaker>().Conver();
    }


    void Update()
    {
        if (gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished && !taskManager.tasks[1].isCompleted)
        {
            taskManager.tasks[1].isCompleted = true;
            taskManager.ShowCurrentTask(1);
            StartCoroutine(WaitToActiveObjetives());
        }
    }

    IEnumerator WaitToActiveObjetives()
    {
        yield return new WaitForSeconds(2.5f);
        taskManager.ShowCurrentTask(5);
    }
}
