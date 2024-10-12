using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Map : MonoBehaviour
{
    GameObject map;
    GameObject tasks;
    Inventory inventory;
    public AudioClip openMapSound;
    public AudioClip buttonSound;
    public int currentLevel = 1;

    private void Awake()
    {
        if (GameObject.Find("TaskList"))
        {
            tasks = GameObject.Find("TaskList");
        }
        else
        {
            tasks = null;
        }
        map = GameObject.Find("Map");
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
    }

    void Start()
    {
        map.SetActive(false);
    }

    void Update()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled)
        {
            map.SetActive(false);
        }
    }

    public void ShowMap()
    {
        if (tasks != null)
        {
            if (tasks.activeSelf)
            {
                tasks.SetActive(false);
            }

        }
        SoundManager.instance.playSound(openMapSound, 0.2f);
        map.SetActive(true);
        showLevel(currentLevel);
    }

    public void playSoundButton()
    {
        SoundManager.instance.playSound(buttonSound, 0.3f);
    }
    public void showLevel(int i)
    {
        if (i==0)
        {
            map.transform.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text ="L-1";
        }else if (i==1)
        {
            map.transform.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = "M-1";
        }
        else
        {
            map.transform.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = "M-2";
        }
       
        Color currentColor = Color.white;
        Image img = map.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(i).gameObject.GetComponent<Image>();
        Image img2 = img;

        for (int j = 0; j < 3; j++)
        {
            if (map.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(j).childCount>0  && j==i)
            {
                map.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(j).GetChild(0).gameObject.SetActive(true);
            }
            else if(map.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(j).childCount>0 && j!=i)
            {
                map.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(j).GetChild(0).gameObject.SetActive(false);
            }
            img2 = map.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(j).gameObject.GetComponent<Image>();
            currentColor = img2.color;
            currentColor.a = 0.1f;
            img2.color= currentColor;
        }
        currentColor = img.color;
        currentColor.a = 1f;
        img.color = currentColor;
    }
}
