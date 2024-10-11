using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModStatusCondition : StatusCondition
{

    [Header("Health mod")]
    public float percentage;

    public override bool OnApply()
    {
        Stats rStats = receiver.GetCurrentStats();

        receiver.ModifyHealth(rStats.maxHealth*percentage);

        messages.Enqueue(applyMessage.Replace("{receiver}", receiver.idName));

        return true;
    }

    public override bool BlocksTurn() => false;

}
