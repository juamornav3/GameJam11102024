using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PickUpItemText : MonoBehaviour
{

    public Transform container;
    public GameObject text;
    GameObject item;

  
    public void ActiveCanvasObject(Sprite icon)
    {
        var newItem = Instantiate(text, container);
        newItem.transform.GetChild(0).GetComponent<Image>().sprite = icon;
        newItem.gameObject.SetActive(true);
        item=newItem;
        StopCoroutine(ActiveCanvas());
        StartCoroutine(ActiveCanvas());
    }

    IEnumerator ActiveCanvas()
    {
        yield return new WaitForSeconds(3f);
        if (item != null)
        {
          item.GetComponent<Animator>().SetTrigger("Disappear");
          yield return new WaitForSeconds(0.5f);
          Destroy(item);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.SetActive(false);  
        }
        
    }
}
