using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public abstract class StatusCondition : MonoBehaviour
{
    [Header("Base Status Condition")]
    public float animationDuration;

    public string receptionMessage;
    public string applyMessage;
    public string expireMessage;

    public int turnDuration;

    public bool hasExpired { get { return turnDuration <= 0; } }

    protected Queue<string> messages;
    protected Fighter receiver;

    public void Awake()
    {
        messages = new Queue<string>();
    }

    public void SetReceiver(Fighter recv)
    {
        receiver = recv;
    }

    private void Animate()
    {
        
    }

    public void Apply()
    {

        if (receiver == null)
        {
            throw new System.InvalidOperationException("StatusCondition needs a receiver");
        }

        if (OnApply())
        {
            Animate();
        }

        turnDuration--;

        if (hasExpired) 
        {
            messages.Enqueue(expireMessage.Replace("{receiver}", receiver.idName));
        }
     }

    public string GetNextMessage()
    {
        if (messages.Count != 0)
        {
            return messages.Dequeue();
        }
        else
        {
            return null;
        }
    }

    public string GetReceptionMessage()
    {
        return receptionMessage.Replace("{receiver}", receiver.idName);
    }

    public abstract bool OnApply();
    public abstract bool BlocksTurn();

}
