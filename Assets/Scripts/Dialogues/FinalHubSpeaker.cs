using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalHubSpeaker : MonoBehaviour
{
    
    public GameObject iaBody;
    public GameObject guidoM;
    public GameObject guido;
    SceneController transition;
    TaskManager taskManager;


    private void Awake()
    {
        taskManager = GameObject.Find("Scripts").GetComponent<TaskManager>();
        transition = GameObject.Find("TransitionWhite").GetComponent<SceneController>();

    }

    void Start()
    {
        if (!gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished)
        {
            StartCoroutine(WaitToChangeScene());
        }
        else
        {
            guido.SetActive(false);
            guidoM.GetComponent<BoxCollider2D>().enabled = true;
            guidoM.GetComponent<SpriteRenderer>().enabled=true;
            if (!gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished)
            {
                StartCoroutine(WaitToTask());
            }
        }

        
    }

    void Update()
    {
        if (gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished && !taskManager.tasks[9].isActive)
        {
            taskManager.ShowCurrentTask(9);
        }

        iaBody.GetComponent<BoxCollider2D>().enabled = gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished && !DialogueManager.instance.isShowed;
    }

    IEnumerator WaitToChangeScene()
    {

        gameObject.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(() => gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished);
        transition.ChangeSceneWithSound();

    }

    IEnumerator WaitToTask()
    {
        taskManager.tasks[8].isCompleted = true;
        taskManager.ShowCurrentTask(8);
        yield return new WaitForSeconds(4);
        gameObject.gameObject.GetComponent<DialogueSpeaker>().Conver();
    }
}