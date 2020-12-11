using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDeathState : RangeState
{
    public override void EnterState()
    {
        stateMachine.REnemy.ChangeState("Range Death State");

        stateMachine.REnemy.OnDeathAnimation();
        // stateMachine.REnemy.enabled = false;

        stateMachine.NavAgent.enabled = false;

        Collider coll = gameObject.GetComponentInParent<Collider>();
        coll.enabled = false;
    }
}
