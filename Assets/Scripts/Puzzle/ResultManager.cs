using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public Result result;
    public List<Sprite> sprites;
    public SceneController sceneController;
  
    void Start()
    {
        if (result.result)
        {
            gameObject.GetComponent<Image>().sprite = sprites[0];
            gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text=result.well;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = sprites[1];
            gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = result.bad;
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void Exit()
    {
        sceneController.sceneId = result.idScene;
        sceneController.ChangeSceneWithSound();
    }

    public void Restart()
    {
        sceneController.sceneId = result.idPuzle;
        sceneController.ChangeSceneWithSound();
    }
   
}
