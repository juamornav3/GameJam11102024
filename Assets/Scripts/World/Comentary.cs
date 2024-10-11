using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Comentary : MonoBehaviour
{
    CursorManager cursorManager;
    GameObject commentary;
    TextMeshProUGUI informativeText;
    Inventory inventory;
    private bool commentaryEnabled;
    private bool isMouseOver = false;  
    public string currentCommentary;
    public Vector3 commentaryOffSet;
    public AudioClip commentaryAudio;
    SceneController transition;

    private void Awake()
    {
        informativeText = GameObject.Find("InformativeText").GetComponent<TextMeshProUGUI>();
        cursorManager = GameObject.Find("Scripts").GetComponent<CursorManager>();
        inventory = GameObject.Find("Scripts").GetComponent<Inventory>();
        commentary = GameObject.Find("CommentaryCanvas");
        transition = GameObject.Find("Transition").GetComponent(typeof(SceneController)) as SceneController;
    }

    void Start()
    {
        commentary.SetActive(false);
    }

    private void Update()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            if (Input.GetMouseButtonDown(0) && commentaryEnabled && !isMouseOver)
            {
                commentary.SetActive(false);
                commentaryEnabled = false;
            }
        }
        else
        {
            commentary.SetActive(false);
            commentaryEnabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            if (commentaryEnabled)
            {
         
                commentary.SetActive(false);
                commentaryEnabled = false;
            }
            else
            {
               
                Comentary[] allCommentaries = FindObjectsOfType<Comentary>();
                foreach (Comentary otherCommentary in allCommentaries)
                {
                    if (otherCommentary.commentaryEnabled)
                    {
                       
                        otherCommentary.commentary.SetActive(false);
                        otherCommentary.commentaryEnabled = false;
                    }
                }

                SoundManager.instance.playSound(commentaryAudio, 0.05f);
                commentary.SetActive(true);
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + commentaryOffSet);

                Vector3 clampedPosition = new Vector3(
                Mathf.Clamp(screenPosition.x, 0f, Screen.width),
                Mathf.Clamp(screenPosition.y, 0f, Screen.height),
                screenPosition.z
               );
               
                commentary.transform.GetChild(0).transform.position = clampedPosition;
                
                commentary.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = currentCommentary;
                informativeText.text = "";
                cursorManager.ChangeCursor("gameCursor");
                SoundManager.instance.playSound(commentaryAudio, 0.05f);
                commentaryEnabled = true;
            }
        }
    }

    
    private void OnMouseEnter()
    {
        isMouseOver = true;
    }

   
    private void OnMouseExit()
    {
        isMouseOver = false;
    }
}
