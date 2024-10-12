using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoe : MonoBehaviour
{
    Inventory inventory;
    GameObject shoeCanvas;
    private void Awake()
    {
        shoeCanvas = GameObject.Find("ShoeCanvas");
    }


    public void ActivateArrows()
    {
        GameObject[] arrows = GameObject.FindGameObjectsWithTag("Arrow");
        foreach (GameObject arrow in arrows)
        {
            if (!arrow.GetComponent<BoxCollider2D>().enabled)
            {
                if (arrow.GetComponent<Arrow>().isFree)
                {
                    arrow.GetComponent<BoxCollider2D>().enabled = true;
                    arrow.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                arrow.GetComponent<BoxCollider2D>().enabled = false;
                arrow.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
