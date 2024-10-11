using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{

    public List<Task> tasks = new List<Task>();
    public List<String> currentTasks = new List<String>();
    GameObject taskText;
    GameObject taskCanvas;
    GameObject map;
    public int pageItems = 4;
    private int currentPage = 0;
    Inventory inventory;
    public AudioClip changePageSound;

    private void Awake()
    {
        taskText = GameObject.Find("TaskTextCanvas");
        taskCanvas = GameObject.Find("TaskList");
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        if (GameObject.Find("Map"))
        {
            map = GameObject.Find("Map");
        }
        else
        {
            map = null;
        }
    }

    public void Start()
    {
        taskCanvas.SetActive(false);
    }

    void Update()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled)
        {
           taskCanvas.SetActive(false);
        }
       
    }
    public void ShowCurrentPage (int page)
    {
        if (map != null)
        {
            if (map.activeSelf)
            {
                map.SetActive(false);
            }

        }
        SoundManager.instance.playSound(changePageSound, 0.7f);
        taskCanvas.SetActive(true);
        currentPage = page;
        int startIndex = page * pageItems;
        int endIndex = Mathf.Min(startIndex + pageItems, tasks.Count);
        currentTasks.Clear();
        for (int i = 0; i < 4; i++)
        {
            taskCanvas.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
        }

        for (int i=startIndex; i<endIndex; i++)
        {
            string description = tasks[i].description;
            if (tasks[i].isActive)
            {
                currentTasks.Add($"<b>{description}</b>");
            }
            if (tasks[i].isCompleted)
            {
                currentTasks.Remove($"<b>{description}</b>");
                currentTasks.Add($"<s>{description}</s>");
        
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (i < currentTasks.Count)
            {
                taskCanvas.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>().text = currentTasks[i];
            }
           
        }
        ShowNextButton();
        ShowPreviousButton();


    }

    private void ShowNextButton()
    {
        if (currentPage == (tasks.Count / pageItems))
        {
            taskCanvas.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            taskCanvas.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
        }
    }

    private void ShowPreviousButton()
    {
        if (currentPage == 0)
        {
            taskCanvas.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            taskCanvas.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
    }

    public void NextPage()
    {
        currentPage = Mathf.Min(currentPage + 1, (tasks.Count/pageItems));
        ShowCurrentPage(currentPage);
    }

    public void PreviousPage()
    {
        currentPage = Mathf.Max(currentPage - 1, 0);
        ShowCurrentPage(currentPage);
    }
    public void ShowCurrentTask(int taskPosition)
    {
        Task task = tasks[taskPosition];
        if (task != null)
        {
            if (task.isCompleted)
            {
                taskText.transform.GetChild(0).GetComponent<Image>().color = HexToColor("#296629E6");
                taskText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Objetivo Completado: " + task.description;
                taskText.GetComponent<Animator>().SetTrigger("Open");
            }
            else
            {
                taskText.transform.GetChild(0).GetComponent<Image>().color = HexToColor("#1A1A1ACC");
                taskText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Nuevo Objetivo: " + task.description;
                taskText.GetComponent<Animator>().SetTrigger("Open");
                task.isActive = true;
            }
            StopCoroutine(ActiveTaskObjetive());
            StartCoroutine(ActiveTaskObjetive());
        }
    }

    private Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
    IEnumerator ActiveTaskObjetive()
    {
        yield return new WaitForSeconds(1f);
        if (taskText != null)
        {
            yield return new WaitForSeconds(1f);
            taskText.GetComponent<Animator>().SetTrigger("Close");
        }

    }
}
