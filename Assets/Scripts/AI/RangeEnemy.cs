using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [Header("Combat Settings")]
    [SerializeField] Transform playerTrans;
    [SerializeField] bool inAtkRange;

    [Header("References")]
    [SerializeField] RangeSM stateMachine;

    private void Update()
    {
        // if player is in atk range still and atk speed cooldown is refreshed, attack again
    }

    void CheckIfTargetInAtkRange()
    {
        
    }

    public override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1);

        stateMachine.ChangeState<RangeCheckState>();
    }

    public override void Die()
    {
        stateMachine.ChangeState<RangeDeathState>();
    }
}
