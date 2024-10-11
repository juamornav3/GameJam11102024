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
    public PuzzleData end;
    public GameObject data;
    public GameObject questionPanel;
    public GameObject questionPanelOptions;
    public GameObject optionsPanel;
    public GameObject optionButton;
    public GameObject mainMenu;
    public List<GameObject> buttons;
    public List<GameObject> extraButtons;
    public List<Sprite> backgrounds;
    public GameObject defaultBackground;
    string[] psoFiles;

    public void Start()
    {
        Cursor.visible = true;
        
        if (File.Exists(data.GetComponent<SaveInventory>().GetSavePath()))
        {
            psoFiles = Directory.GetFiles(data.GetComponent<SaveInventory>().GetSavePath().Replace("inventorySave.dat", ""), "*.pso");
            if (psoFiles.Length > 0)
            {

                foreach (GameObject button in buttons)
                {
                    button.gameObject.GetComponent<Button>().interactable = true;
                    button.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
                }
            }
            else
            {
                foreach (GameObject button in buttons)
                {
                    button.gameObject.GetComponent<Button>().interactable = false;
                    button.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
                }
            }
              
        }
        foreach (GameObject button in extraButtons)
        {
            button.gameObject.GetComponent<Button>().interactable = end.finished;
            if (end.finished)
            {
                button.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
            }
            else
            {
                button.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
            }
        }
        LoadBackground();
    }

    public void LoadBackground()
    {
        switch (playerData.idScene)
        {
            case 1:
                ChangeBackground(0);
                break;
            case 4:
                ChangeBackground(1);
                break;
            case 5:
                ChangeBackground(2);
                break;
            case 8:
                ChangeBackground(3);
                break;
            case 9:
                ChangeBackground(4);
                break;
            case 12:
                ChangeBackground(5);
                break;
            case 14:
                ChangeBackground(6);
                break;
            case 16:
                ChangeBackground(7);
                break;
            case 18:
                ChangeBackground(8);
                break;
            case 22:
                ChangeBackground(9);
                break;
            case 24:
                ChangeBackground(10);
                break;
            case 27:
                ChangeBackground(11);
                break;
            default:
                ChangeBackground(12);
                defaultBackground.SetActive(true);
                break;
                
        }
    }

    public void ChangeBackground(int i)
    {
        mainMenu.GetComponent<Image>().sprite = backgrounds[i];
    }
    public void NewPlay()
    {
        if (File.Exists(data.GetComponent<SaveInventory>().GetSavePath()))
        {
            if (psoFiles.Length > 0)
            {
                questionPanel.SetActive(true);
            }
            else
            {
                playerData.idScene = 48;
                Play();
            }
        }
        else
        {
            playerData.idScene = 48;
            Play();
        }
    }
    public void Play()
    {
        transition.gameObject.GetComponent<SceneController>().ChangeScene(playerData.idScene);
    }

    public void OptionNo()
    {
        questionPanel.SetActive(false);
    }

    public void OptionYes()
    {
        questionPanel.SetActive(false);
        data.GetComponent<SaveInventory>().DeleteSaves();
        playerData.idScene = 48;
        Play();
    }

    public void Delete()
    {
        questionPanelOptions.SetActive(false);
        data.GetComponent<SaveInventory>().DeleteSaves();
        foreach (GameObject button in buttons)
        {
            button.gameObject.GetComponent<Button>().interactable = false;
            button.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
        }
    }

    public void DeleteData()
    {
        if (File.Exists(data.GetComponent<SaveInventory>().GetSavePath()))
        {
            buttons[0].gameObject.GetComponent<Button>().interactable = true;
            buttons[0].gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
            if (psoFiles.Length > 0)
            {
                questionPanelOptions.SetActive(true);
            }
        }
    }

    public void OpenOptions()
    {
        optionsPanel.GetComponent<Animator>().SetTrigger("Open");
        optionButton.GetComponent<Button>().interactable = false;
    }
    public void CloseOptions()
    {
        optionsPanel.GetComponent<Animator>().SetTrigger("Close");
        optionButton.GetComponent<Button>().interactable = true;
    }

    public void Exit()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
