using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
   public Sprite background;
    public PuzzleData data;
    void Start()
    {
        if (data.finished)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = background;
        }
    }

}
