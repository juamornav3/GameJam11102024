using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public Conversation conversation;
    [SerializeField]
    private float textSpeed = 30;

    [SerializeField]
    private GameObject convContainer;
    [SerializeField]
    private GameObject questContainer;

    [SerializeField]
    private Image speakIm;
    [SerializeField]
    private Image speakIm2;
    [SerializeField]
    private Image speakIm3;
    [SerializeField]
    private TextMeshProUGUI characterName;
    [SerializeField]
    private TextMeshProUGUI convText;
    QuestionManager questionManager;

    public AudioClip typeSound;

    [SerializeField]
    private Button next;

   

    public int localIndex = 1;

    private void Awake()
    {
        questionManager = FindObjectOfType<QuestionManager>();
    }
    void Start()
    {
        convContainer.SetActive(true);
        questContainer.SetActive(false);

        next.gameObject.SetActive(false);
        
    }

  

    public void UpdateTexts(int caseNumber)
    {
        convContainer.SetActive(true);
        questContainer.SetActive(false);

        switch (caseNumber)
        {
            case 0:
                ShowDialogue();
                break;
            case 1:
                if (localIndex < conversation.dialogues.Length - 1)
                {
                    localIndex++;
                    ShowDialogue();
                }
                else
                {
                    localIndex = 0;
                    DialogueManager.actualSpeaker.dialLocalIn = 0;
                    conversation.finished = true;

                    if(conversation.question != null)
                    {
                        convContainer.SetActive(false);
                        questContainer.SetActive(true);
                        var quest = conversation.question;
                        questionManager.ActiveButtons(quest.choices.Length, quest.question, quest.choices);

                        return;
                    }
                    DialogueManager.instance.ShowUI(false);
                    return;
                }
                DialogueManager.actualSpeaker.dialLocalIn = localIndex;
                break;
            default:
                Debug.Log("WARNING");
                break;
        }
    }

    private void ShowDialogue()
    {
        next.gameObject.SetActive(false);
        characterName.text = conversation.dialogues[localIndex].character.characterName;
        StopAllCoroutines();
        StartCoroutine(WriteText());
        convText.text = conversation.dialogues[localIndex].dialogue;
        speakIm.sprite = conversation.dialogues[localIndex].character.image;
        speakIm2.sprite = conversation.dialogues[localIndex].character.image2;
        speakIm3.sprite = conversation.dialogues[localIndex].character.image3;

        if (conversation.dialogues[localIndex].sound != null)
        {
            SoundManager.instance.StopSound();
            var audio = conversation.dialogues[localIndex].sound;
            SoundManager.instance.playSound(audio, 0.25f);
        }
    }

    IEnumerator WriteText()
    {
        convText.maxVisibleCharacters = 0;
        convText.text = conversation.dialogues[localIndex].dialogue;
        convText.richText = true;

        for(int i = 0; i < conversation.dialogues[localIndex].dialogue.ToCharArray().Length; i++)
        {
            convText.maxVisibleCharacters++;
            SoundManager.instance.playSound(typeSound,0.1f);
            yield return new WaitForSeconds(1f/textSpeed);
            if (Input.GetMouseButton(0))
            {
                convText.maxVisibleCharacters = conversation.dialogues[localIndex].dialogue.ToCharArray().Length;
                break;
            }
            
        }
        next.gameObject.SetActive(true);
    }
}
