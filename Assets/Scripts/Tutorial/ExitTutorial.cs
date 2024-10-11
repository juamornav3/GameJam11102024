using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTutorial : MonoBehaviour
{

    SceneController controller;

    private void Awake()
    {
        controller = GameObject.Find("Transition").GetComponent(typeof(SceneController)) as SceneController;
    }
    public void OnMouseDown()
    {
        if (!DialogueManager.instance.isShowed)
        {
            controller.ChangeScene();
        }

    }
}
