using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleQuestionManager : MonoBehaviour
{
    public bool speaker = false;
    public Conversation fail;
    public Conversation correct;
    public GameObject ia;
    public string trigger;
    void Start()
    {
        foreach(Conversation conversation in gameObject.GetComponent<DialogueSpeaker>().conversations)
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
        if (speaker && fail.finished)
        {
            speaker = false;
            int i = gameObject.GetComponent<DialogueSpeaker>().conversationIndex;
            gameObject.GetComponent<DialogueSpeaker>().conversations[i].finished = false;
            gameObject.GetComponent<DialogueSpeaker>().conversationPersistanceManagers[i].finished = false;
        }else if (speaker && correct.finished){
            speaker = false;
            StartCoroutine(AnimateDamage());
            if (gameObject.GetComponent<DialogueSpeaker>().conversationIndex >= (gameObject.GetComponent<DialogueSpeaker>().conversations.Count-1))
            {
                ia.GetComponent<Animator>().SetTrigger(trigger);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<HighLight>().finish = true;
            }
            
        }

    }

    IEnumerator AnimateDamage()
    {
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Animator>().SetTrigger("Damage");
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
