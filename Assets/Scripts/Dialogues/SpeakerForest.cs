using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeakerForest : MonoBehaviour
{
    public GameObject speaker;
    public GameObject laptop;
    public GameObject arrow;
    public Sprite open;
    public PlayerData playerData;
    TaskManager taskManager;
    GameObject forest;
    
    private void Awake()
    {
        taskManager = GameObject.Find("Scripts").GetComponent(typeof(TaskManager)) as TaskManager;
        forest = GameObject.Find("Forest");
        playerData.idScene = 1;
    }
    void Start()
    {
       
        if (speaker.GetComponent<DialogueSpeaker>().conversations[0].finished==false)
        {
            StartCoroutine(WaitToStartDialogue(1));
        }
        else
        {
            if (!taskManager.tasks[0].isActive && !taskManager.tasks[0].isCompleted)
            {
                taskManager.ShowCurrentTask(0);
            }
        }
        if (gameObject.GetComponent<PuzzleManager>().puzzles[0].finished)
        {
            forest.GetComponent<SpriteRenderer>().sprite = open;
            arrow.GetComponent<Arrow>().isFree = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            taskManager.tasks[0].isCompleted = true;
            taskManager.ShowCurrentTask(0);
        }
       
    }


    IEnumerator WaitToStartDialogue(float duration)
    {
        yield return new WaitForSeconds(duration);
        speaker.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(() => speaker.GetComponent<DialogueSpeaker>().conversations[0].question.choices[1].result.finished  || speaker.GetComponent<DialogueSpeaker>().conversations[0].question.choices[0].result.finished );
        if (speaker.GetComponent<DialogueSpeaker>().conversations[0].question.choices[1].result.finished)
        {
            taskManager.ShowCurrentTask(0);
        }
        else
        {
            laptop.GetComponent<SceneController>().ChangeScene();
        }

    }
}
