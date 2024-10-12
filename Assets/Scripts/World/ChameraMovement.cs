using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChameraMovement : MonoBehaviour
{
    public float rate=12;
    SceneController transition;

    private void Awake()
    {
        transition = GameObject.Find("Transition").GetComponent(typeof(SceneController)) as SceneController;
    }
    void Update()
    {
        if (rate > 0 && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            Vector2 chameraCenter=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = -chameraCenter / rate;
        }
    }
}
