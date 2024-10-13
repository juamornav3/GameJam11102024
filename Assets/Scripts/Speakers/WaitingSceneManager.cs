using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingSceneManager : MonoBehaviour
{
    SceneController controller;

    private void Awake()
    {
        controller = FindAnyObjectByType<SceneController>();
    }
    void Start()
    {
        StartCoroutine(WaitToChangeSceneCO());
    }

    IEnumerator WaitToChangeSceneCO()
    {
        yield return new WaitForSeconds(8f);
        controller.ChangeScene(0);
    } 
}
