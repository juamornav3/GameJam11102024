using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : Fighter
{

    public bool isBodyPart;
    void Awake()
    {
        this.stats = new Stats(20, 70, 40, 30, 60, 15);
    }

    public override void InitTurn()
    {
        StartCoroutine(this.IA());
    }

    IEnumerator IA()
    {
        yield return new WaitForSeconds(0.1f);
        this.combatManager.NextTurn();

    }

}
