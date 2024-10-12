using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class HintManager : MonoBehaviour
{

    public HintData hintData;
    public PlayerData playerData;
    public List<GameObject> gameObjects;
    public GameObject cont;
    public AudioClip sound;
    void Start()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(!hintData.sold);
        cont.GetComponent<TextMeshProUGUI>().text = "x " + playerData.cont;
        if (hintData.sold)
        {
            gameObjects[1].SetActive(true);
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hintData.hint;
        }
        else
        {
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        }

    }

    public void UnlockHint()
    {
        if (!hintData.sold && hintData.price <= playerData.cont)
        {
            playerData.cont = playerData.cont - hintData.price;
            hintData.sold = true;
            gameObjects[1].SetActive(true);
            SoundManager.instance.playSound(sound, 1f);
        }
        Start();
    }

    public void ChangeOrder()
    {
        gameObject.GetComponent<Canvas>().sortingOrder = 3;
        for (int i = 0; i<gameObjects.Count; i++)
        {
            gameObjects[i].GetComponent<Canvas>().sortingOrder = i+1;
        }
    }
}
