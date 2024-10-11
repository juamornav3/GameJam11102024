using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMin : MonoBehaviour
{

    public List<int> idPuzzles;
    public List<Sprite> sprites;
    public GameObject image;
    public GameObject button;
    int currentID;
    public SceneController transition;
    void Start()
    {
        Cursor.visible = true;
        button.SetActive(false);
    }

    public void ShowPuzzle(int i)
    {
        currentID = i;
        image.GetComponent<Image>().sprite = sprites[currentID];
        button.SetActive(true); 
    }
    
   public void OpenPuzzle()
    {
        if (!transition.itsChanging)
        {
            transition.duration = 3;
            transition.sceneId = idPuzzles[currentID];
            transition.ChangeScene();
        }
    }
}
