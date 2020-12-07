using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : RangeState
{
    [SerializeField] RangeEnemy rEnemy;

    [Header("Debug")]
    [SerializeField] bool showStateMessages;

    public override void EnterState()
    {
        if (showStateMessages)
            print("Enter Range Enemy Attack State");
        rEnemy.Attack();
    }
}
