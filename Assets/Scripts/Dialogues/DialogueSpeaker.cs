using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueSpeaker : MonoBehaviour
{
    
    public List<Conversation> conversations = new List<Conversation>();
    public List<ConversationPersistanceManager> conversationPersistanceManagers = new List<ConversationPersistanceManager>();
    
    public int conversationIndex = 0;

    public int dialLocalIn = 0;

    Inventory inventory;

    private void Awake()
    {
        inventory = inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        for (int i = 0; i < conversations.Count; i++)
        {
              
            conversations[i].finished = conversationPersistanceManagers[i].finished;
        }
        conversationIndex = 0;
        dialLocalIn = 0;
    }

    private void Update()
    {
        if (conversations[conversationIndex].finished)
        {
            conversationPersistanceManagers[conversationIndex].finished = true;
        }
    }

    public void Conver()
    {
       
        if (conversationIndex <= conversations.Count - 1)
        {
            
            if (conversations[conversationIndex].unlocked)
            {
               
                if (conversations[conversationIndex].finished)
                {
                    if (UpdateConversation())
                    {
                        DialogueManager.instance.ShowUI(true);
                        DialogueManager.instance.SetConversation(conversations[conversationIndex], this);
                    }
                    DialogueManager.instance.SetConversation(conversations[conversationIndex], this);
                    return;
                }
                DialogueManager.instance.ShowUI(true);
                DialogueManager.instance.SetConversation(conversations[conversationIndex], this);
            }
            else
            {
                Debug.LogWarning("La conversación está bloqueada");
                DialogueManager.instance.ShowUI(false);
            }
        }
        else
        {
            DialogueManager.instance.ShowUI(false);
        }


    }

    private bool UpdateConversation()
    {
        if (!conversations[conversationIndex].reuse)
        {
            if(conversationIndex < conversations.Count - 1)
            {
                conversationIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
}
