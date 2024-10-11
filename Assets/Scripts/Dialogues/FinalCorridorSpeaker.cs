using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCorridorSpeaker : MonoBehaviour
{
    public PuzzleData puzzleData;
    public GameObject panel;
    Inventory inventory;
    public Arrow arrow;
    public GameObject soldiers;
    SceneController transition;
    public Sprite background;
    public GameObject lab;


    private void Awake()
    {
        transition = GameObject.Find("Transition").GetComponent<SceneController>();
        inventory = GameObject.Find("Scripts").GetComponent<Inventory>();

    }

    void Start()
    {
        if (!puzzleData.finished)
        {
            if (!gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished && gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished)
            {
                gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 1;
                gameObject.GetComponent<DialogueSpeaker>().Conver();
            }
        }
        else
        {
            lab.GetComponent<SpriteRenderer>().sprite = background;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            arrow.isFree = true;
            soldiers.GetComponent<BoxCollider2D>().enabled = true;
            for (int i = 0; i<2; i++)
            {
                soldiers.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                soldiers.transform.GetChild(i).GetComponent<AudioSource>().enabled = true;
                soldiers.transform.GetChild(i).GetComponent<Animator>().enabled = true;

            }
            if (!gameObject.GetComponent<DialogueSpeaker>().conversations[2].finished)
            {
                gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 2;
                gameObject.GetComponent<DialogueSpeaker>().Conver();
            }
        }




    }
    private void Update()
    {

        gameObject.GetComponent<BoxCollider2D>().enabled = !DialogueManager.instance.isShowed && !gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished;
        panel.GetComponent<BoxCollider2D>().enabled = !puzzleData.finished && gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished;

    }

    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            if (!gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished)
            {
                gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 0;
                StartCoroutine(WaitToChangeScene());
            }
        }


        IEnumerator WaitToChangeScene()
        {

            gameObject.GetComponent<DialogueSpeaker>().Conver();
            yield return new WaitUntil(() => gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished);
            transition.ChangeScene();

        }
    }
}
