using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySkills : Skill
{
    private StatusCondition condition;

    protected override void OnRun(Fighter receiver)
    {
        if (this.condition == null)
        {
            this.condition = this.GetComponentInChildren<StatusCondition>();

            if (this.condition.gameObject == this.gameObject)
            {
                throw new System.InvalidOperationException(
                    "The StatusCondition should be a child of the skill object because it needs to be cloned"
                );
            }
        }

        if (receiver.GetCurrentStatusCondition())
        {
            this.messages.Enqueue("¡Falló!");
            return;
        }


        GameObject go = Instantiate(this.condition.gameObject);
        go.transform.SetParent(receiver.transform);


        StatusCondition clonedCondition = go.GetComponent<StatusCondition>();
        clonedCondition.SetReceiver(receiver);
        receiver.statusCondition = clonedCondition;

        this.messages.Enqueue(clonedCondition.GetReceptionMessage());
    }
}
