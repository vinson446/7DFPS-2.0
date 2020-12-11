using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDeathState : MeleeState
{
    public override void EnterState()
    {
        stateMachine.MEnemy.ChangeState("Melee Death State");

        stateMachine.MEnemy.OnDeathAnimation();
        // stateMachine.MEnemy.enabled = false;

        stateMachine.NavAgent.enabled = false;

        Collider coll = gameObject.GetComponentInParent<Collider>();
        coll.enabled = false;
    }
}
