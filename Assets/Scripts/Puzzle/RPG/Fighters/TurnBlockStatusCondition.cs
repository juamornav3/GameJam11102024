using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBlockStatusCondition : StatusCondition
{
    [Range(0f, 1f)]
    public float blockChance;

    private bool blocks;

    public override bool OnApply()
    {
        blocks = false;

        float dice = Random.Range(0f, 1f);

        if (dice <= blockChance)
        {
            blocks = true;
            messages.Enqueue(applyMessage.Replace("{receiver}", receiver.idName));

            return true;
        }
        return false;
    }

    public override bool BlocksTurn() => blocks;

}
