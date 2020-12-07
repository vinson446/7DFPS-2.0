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

    [Header("Debug")]
    [SerializeField] bool debugAggroRange;
    [SerializeField] bool debugAttackRange;

    public override void EnterState()
    {
        stateMachine.MEnemy.ChangeState("Melee Check State");

        stateMachine.NavAgent.enabled = true;
    }

    public override void Tick()
    {
        LookAtTarget();
        CheckAction();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.MEnemy.Die();
        }
    }

    void LookAtTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(new Vector3(target.position.x, 0, target.position.z)), Time.deltaTime * rotSpeed);
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
