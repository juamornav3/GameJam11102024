using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Vector3 textOffset;
    CursorManager cursorManager;
    private DialogueSpeaker speaker;
    public GameObject principalSpeaker;
    public bool finish;

    private void Awake()
    {
        cursorManager = GameObject.Find("Scripts").GetComponent(typeof(CursorManager)) as CursorManager;
        speaker = gameObject.GetComponent<DialogueSpeaker>();
    }
    private void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        if (!DialogueManager.instance.isShowed)
        {
            cursorManager.ChangeCursor("actionCursor");

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + textOffset);

            Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(screenPosition.x, 0f, Screen.width),
            Mathf.Clamp(screenPosition.y, 0f, Screen.height),
            screenPosition.z
           );
            spriteRenderer.color = new Color(0.8773585f, 0.5164827f, 0.5164827f, 1f);
        } 
    }

    private void Update()
    {
        if (principalSpeaker.GetComponent<DialogueSpeaker>().conversations[0].finished)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            if (DialogueManager.instance.isShowed)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                OnMouseExit();
            }
            else if (finish)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
        cursorManager.ChangeCursor("gameCursor");
    }

    private void OnMouseDown()
    {
        speaker.Conver();
        gameObject.GetComponent<BattleQuestionManager>().speaker = true;
    }

}
