using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : RangeState
{
    public override void EnterState()
    {
        stateMachine.REnemy.ChangeState("Range Attack State");

        stateMachine.REnemy.initPos = transform.position;

        stateMachine.REnemy.Attack();
    }
}
