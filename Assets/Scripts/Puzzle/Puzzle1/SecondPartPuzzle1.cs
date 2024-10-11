using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SecondPartPuzzle1 : MonoBehaviour
{
    
    TextMeshProUGUI placeHolder;
    TMP_InputField inputField;
    TextMeshProUGUI panelText;
    public AudioClip buttonAudio;
    public AudioClip numberAudio;
    public AudioClip correctSound;
    public AudioClip failSound;
    SceneController transition;
    public string solution;
    
    Color initialColor;
    private void Awake()
    {
        placeHolder = GameObject.Find("Placeholder").GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        panelText = GameObject.Find("PanelText").GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        inputField = GameObject.Find("InputField").GetComponent<TMP_InputField>();
        initialColor = panelText.GetComponent<TextMeshProUGUI>().color;
        transition = GameObject.Find("Transition").GetComponent(typeof(SceneController)) as SceneController;
    }

    void Start()
    {
        Cursor.visible = true;
        placeHolder.text = "Enter code...";
        panelText.text = "";
        inputField.text = "";
        
       
       
    }

    public void IsCorrectCode(string inputText)
    {
        if (inputText.Equals(solution))
        {
            gameObject.GetComponent<PuzzleManager>().puzzles[0].finished = true;
            SoundManager.instance.playSound(correctSound, 0.5f);
            transition.ChangeScene();

        }
        else
         {
            SoundManager.instance.playSound(failSound,0.5f);
            panelText.GetComponent<TextMeshProUGUI>().color = Color.red;
                 
            
        }
    }

    public void PanelUpdate(string inputText)
    {
        SoundManager.instance.playSound(numberAudio,0.4f);
        panelText.GetComponent<TextMeshProUGUI>().color =  initialColor;
        panelText.text = inputText;  
    }

    public void ResetSecondPart()
    {
        SoundManager.instance.playSound(buttonAudio, 0.6f);
        Start();
        

    }

    
}
