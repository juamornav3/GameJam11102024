using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Puzzle : MonoBehaviour
{
    public GameObject selectedPiece;
    int layer = 1;
    public  int embeddedPieces;
    public Sprite secretCode;
    public bool finish;
    GameObject secondPart;
    GameObject firstPart;
    public AudioClip pickUpAudio;
    public AudioClip buttonAudio;



    private void Awake()
    {
        firstPart = GameObject.Find("FirstPart");
        secondPart = GameObject.Find("SecondPart");
        
    }
    void Start()
    {
        Cursor.visible = true;
        firstPart.SetActive(true);
        secondPart.SetActive(false);
        embeddedPieces = 0;
        finish = false; 
        for (int i = 0; i < 4; i++)
        {
            GameObject pieza = GameObject.Find("Piece" + (i+1));
            if (pieza != null)
            {
                Transform transformadaSecretCode = pieza.transform.Find("SecretCode");
                if (transformadaSecretCode != null)
                {
                    SpriteRenderer spriteRenderer = transformadaSecretCode.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sprite = secretCode;
                    }
                }
            }

        }
    }

   
    void Update()
    {
        if (!finish)
        {


            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.transform.CompareTag("Puzzle"))
                {
                    if (!hit.transform.GetComponent<Pieza>().selected)
                    {
                        SoundManager.instance.playSound(pickUpAudio, 0.6f);
                    }
                    if (!hit.transform.GetComponent<Pieza>().embedded)
                    {
                        
                        selectedPiece = hit.transform.gameObject;
                        selectedPiece.GetComponent<Pieza>().selected = true;
                        selectedPiece.GetComponent<SortingGroup>().sortingOrder = layer;
                        layer++;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (selectedPiece != null)
                {
                    selectedPiece.GetComponent<Pieza>().selected = false;
                    selectedPiece = null;
                }
            }

            if (selectedPiece != null)
            {
                Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                selectedPiece.transform.position = new Vector3(mouse.x, mouse.y, 0);
            }

            if (embeddedPieces == 4)
            {
                firstPart.SetActive(false);
                secondPart.SetActive(true);
                finish = true;
              
            }
        }
        

    }
    public void ResetPuzzle()
    {
        SoundManager.instance.playSound(buttonAudio, 0.6f);
        Start();
        
        for (int i = 0; i < 4; i++)
        {
            GameObject pieza = GameObject.Find("Piece" + (i+1));
            if (pieza != null)
            {
                pieza.GetComponent<Pieza>().ResetPiece();
            }

        }
    }

}
