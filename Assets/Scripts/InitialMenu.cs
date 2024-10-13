using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;


public class InitialMenu : MonoBehaviour
{
    public GameObject transition;
    public PlayerData playerData;


    public void Start()
    {
        
    }

    public void NewPlay()
    {
        playerData.idScene = 1;
        playerData.cont = 0;
        Play();
    }
    public void Play()
    {
        transition.gameObject.GetComponent<SceneController>().ChangeScene(playerData.idScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
