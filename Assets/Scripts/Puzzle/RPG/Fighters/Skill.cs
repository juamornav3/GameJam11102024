using UnityEngine;
using System.Collections.Generic;

public abstract class Skill : MonoBehaviour
{
    [Header("Base Skill")]
    public string skillName;
    public float animationDuration;
    public float audioLevel;

    public SkillTargeting targeting;
    public string trigger;
    public AudioClip triggerClip;
    protected Fighter emitter;
    protected List<Fighter> receivers;

    protected Queue<string> messages;

    public bool needsManualTargeting
    {
        get
        {
            switch (this.targeting)
            {
                case SkillTargeting.SINGLE_ALLY:
                case SkillTargeting.SINGLE_OPPONENT:
                    return true;

                default:
                    return false;
            }
        }
    }

    void Awake()
    {
        this.messages = new Queue<string>();
        this.receivers = new List<Fighter>();
    }

    private void Animate(Fighter receiver)
    {
        SoundManager.instance.playSound(triggerClip, audioLevel);
        emitter.animator.SetTrigger(trigger);
    }

    public void Run()
    {
        foreach (var receiver in this.receivers)
        {
            this.Animate(receiver);
            this.OnRun(receiver);
        }

        this.receivers.Clear();
    }

    public void SetEmitter(Fighter _emitter)
    {
        this.emitter = _emitter;
    }

    public void AddReceiver(Fighter _receiver)
    {
        this.receivers.Add(_receiver);
    }

    public string GetNextMessage()
    {
        if (this.messages.Count != 0)
            return this.messages.Dequeue();
        else
            return null;
    }

    protected abstract void OnRun(Fighter receiver);
}