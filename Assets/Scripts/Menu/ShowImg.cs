using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowImg : MonoBehaviour
{

    
    public List<Sprite> sprites;
    public List<string> descriptions;
    public TextMeshProUGUI text;
    public GameObject image;
    public GameObject panel;
    int currentID;
    void Start()
    {
        Cursor.visible = true;
        text.text = "";
        panel.SetActive(false);
    }

    public void ShowImage(int i)
    {
        currentID = i;
        image.GetComponent<Image>().sprite = sprites[currentID];
        text.text = descriptions[currentID];
        panel.SetActive(true); 
    }
    
  
}
