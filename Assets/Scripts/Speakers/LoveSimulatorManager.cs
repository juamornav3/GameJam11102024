using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoveSimulatorManager : MonoBehaviour
{
    public bool speaker = false;
    public List<Conversation> fail;
    public List<Conversation> correct;
    public int currentDialogue = 0;
    public GameObject concierge;
    public Image loveLevel;
    public string trigger;
    public PlayerData playerData;
    private SceneController sceneController;

    private void Awake()
    {
        sceneController = FindAnyObjectByType<SceneController>();
    }
    void Start()
    {
        loveLevel.fillAmount = 0;
        foreach (Conversation conversation in gameObject.GetComponent<DialogueSpeaker>().conversations)
        {
            conversation.finished = false;
        }
        foreach (ConversationPersistanceManager conversation in gameObject.GetComponent<DialogueSpeaker>().conversationPersistanceManagers)
        {
            conversation.finished = false;
        }
    }

    public void Update()
    {
        if(currentDialogue < fail.Count)
        {
            if (fail[currentDialogue].finished)
            {
                fail[currentDialogue].finished = false;
                currentDialogue++;
                loveLevel.fillAmount = Mathf.Max(0, loveLevel.fillAmount - 0.25f);

                    currentDialogue++;

            }
            else if (correct[currentDialogue].finished)
            {
                correct[currentDialogue].finished = false;

                loveLevel.fillAmount = Mathf.Min(1, loveLevel.fillAmount + 0.25f);

                    currentDialogue++;

            }
        }
        else
        {
            
            if (!gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished && !gameObject.GetComponent<DialogueSpeaker>().conversations[1].unlocked)
            {
                concierge.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<DialogueSpeaker>().conversations[1].unlocked = true;
                gameObject.GetComponent<DialogueSpeaker>().conversationIndex = 1;
                gameObject.GetComponent<DialogueSpeaker>().Conver();
            }

            if (loveLevel.fillAmount >= 1 && playerData.cont <= 0)
            {
                playerData.cont = 1;
            }

            if (!sceneController.itsChanging && gameObject.GetComponent<DialogueSpeaker>().conversations[1].unlocked && gameObject.GetComponent<DialogueSpeaker>().conversations[1].finished)
            {
                sceneController.ChangeScene();
            }
        }
        
       
        


    }
}
