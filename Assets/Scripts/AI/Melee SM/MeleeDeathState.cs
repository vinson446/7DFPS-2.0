using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDeathState : MeleeState
{
    public override void EnterState()
    {
        stateMachine.MEnemy.OnDeathAnimation();
        stateMachine.NavAgent.enabled = false;
    }
}
