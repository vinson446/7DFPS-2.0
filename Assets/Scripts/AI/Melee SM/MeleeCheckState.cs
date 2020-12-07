using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeCheckState : MeleeState
{
    [SerializeField] Transform target;

    [Header("Ranges")]
    [SerializeField] float aggroCheck;

    [Header("MoveSpeed")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float rotSpeed;
    bool rotInstantly;

    [Header("Debug")]
    [SerializeField] bool debugAggroRange;
    [SerializeField] bool debugAttackRange;

    public override void EnterState()
    {
        stateMachine.MEnemy.ChangeState("Melee Check State");

        rotInstantly = false;

        stateMachine.NavAgent.enabled = true;
    }

    public override void Tick()
    {
        CheckAction();
    }

    void CheckAction()
    {
        // attack if player is within range
        if (Vector3.Distance(transform.position, target.position) <= stateMachine.MEnemy.AtkRange)
        {
            stateMachine.NavAgent.enabled = false;
            stateMachine.ChangeState<MeleeAttackState>();

            return;
        }

        if (!rotInstantly)
        {
            LookAtTargetInstantly();
            rotInstantly = true;
        }

        LookAtTarget();

        // if player is outside of aggro check, walk towards player
        if (Vector3.Distance(transform.position, target.position) > aggroCheck)
        {
            stateMachine.NavAgent.speed = walkSpeed;
            stateMachine.NavAgent.SetDestination(target.position);

            stateMachine.MEnemy.OnWalkAnimation();
        }
        // if player is inside aggro check, run towards player
        else
        {
            stateMachine.NavAgent.speed = runSpeed;
            stateMachine.NavAgent.SetDestination(target.position);

            stateMachine.MEnemy.OnRunAnimation();
        }
    }

    void LookAtTarget()
    {
        Vector3 toTarget = (stateMachine.PlayerTrans.position - transform.position).normalized;

        if (Vector3.Dot(toTarget, transform.forward) < 0)
        {
            LookAtTargetInstantly();
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(target.position.x, 0, target.position.z)),
                Time.deltaTime * rotSpeed);
        }
    }

    void LookAtTargetInstantly()
    {
        Vector3 lookAt = new Vector3(stateMachine.PlayerTrans.position.x, transform.position.z, stateMachine.PlayerTrans.position.y);
        transform.LookAt(lookAt);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        MeleeEnemy mEnemy = GetComponentInParent<MeleeEnemy>();

        if (debugAggroRange)
            Gizmos.DrawWireSphere(transform.position, aggroCheck);
        else if (debugAttackRange)
            Gizmos.DrawWireSphere(transform.position, mEnemy.AtkRange);
    }
}
