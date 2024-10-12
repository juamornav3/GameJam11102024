using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Pieza : MonoBehaviour
{
    private Vector3 truePosition;
    public bool embedded;
    public bool selected;
    public AudioClip embeddedAudio;

    void Start()
    {
        embedded = false;
        truePosition = transform.position;
        transform.position = new Vector3(-5.5f, -2.5f);
    }


    void Update()
    {
       if (Vector3.Distance(transform.position, truePosition) < 0.5f)
        {
            if (!selected)
            {
                if (embedded == false)
                {
                    transform.position = truePosition;
                    embedded = true;
                    SoundManager.instance.playSound(embeddedAudio,0.6f);
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    Camera.main.GetComponent<Puzzle>().embeddedPieces++;
                }
            }
            
            
        } 
    }

    public void ResetPiece()
    {
        embedded = false;
        transform.position = new Vector3(-5.5f, -2.5f);
    }


}
