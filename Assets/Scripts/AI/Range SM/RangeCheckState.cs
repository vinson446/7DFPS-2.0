using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeCheckState : RangeState
{
    Transform target;

    [Header("MoveSpeed")]
    [SerializeField] float runSpeed;
    public float RunSpeed => runSpeed;
    [SerializeField] float rotSpeed;
    public float RotSpeed => rotSpeed;

    [Header("Debug")]
    [SerializeField] bool debugRunAwayRange;
    [SerializeField] bool debugAttackRange;

    public override void EnterState()
    {
        stateMachine.REnemy.ChangeState("Range Check State");

        target = FindObjectOfType<Player>().transform;

        stateMachine.NavAgent.enabled = true;
    }

    public override void Tick()
    {
        CheckAction();
        // LookAtTarget();
    }

    void CheckAction()
    {
        // if player is in attack range, shoot
        if (Vector3.Distance(transform.position, target.position) <= stateMachine.REnemy.AtkRange)
        {
            stateMachine.ChangeState<RangeAttackState>();
        }
        else
        {
            stateMachine.REnemy.OnIdleAnimation();
        }
    }

    void LookAtTarget()
    {
        /*
        Vector3 toTarget = (stateMachine.PlayerTrans.position - transform.position).normalized;

        if (Vector3.Dot(toTarget, transform.forward) < 0)
        {
            LookAtTargetInstantly();
        }
        else 
        */

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(target.position.x, 0, target.position.z)),
            Time.deltaTime * rotSpeed);
    }

    void LookAtTargetInstantly()
    {
        Vector3 lookAt = new Vector3(stateMachine.PlayerTrans.position.x, transform.position.y, stateMachine.PlayerTrans.position.z);
        transform.LookAt(lookAt);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        RangeEnemy rEnemy = GetComponentInParent<RangeEnemy>();
        RangeRunState rangeRunState = GetComponent<RangeRunState>();

        if (debugRunAwayRange)
            Gizmos.DrawWireSphere(transform.position, rangeRunState.RunAwayCheck);
        else if (debugAttackRange)
            Gizmos.DrawWireSphere(transform.position, rEnemy.AtkRange);
    }
}
