using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToChangeScene : MonoBehaviour
{

    public GameObject transition;
    void Start()
    {
        StartCoroutine(WaitToTransition());
    }

    IEnumerator WaitToTransition()
    {
        yield return new WaitForSeconds(5f);
        transition.GetComponent<SceneController>().duration = 1;
        transition.GetComponent<SceneController>().ChangeScene();
    }

}
