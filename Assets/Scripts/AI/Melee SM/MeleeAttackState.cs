using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : MeleeState
{
    public override void EnterState()
    {
        stateMachine.MEnemy.ChangeState("Melee Attack State");

        stateMachine.MEnemy.Attack();
    }
}
