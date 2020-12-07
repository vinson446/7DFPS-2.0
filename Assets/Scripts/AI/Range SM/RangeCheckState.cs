using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeCheckState : RangeState
{
    [SerializeField] Transform target;

    [Header("Ranges")]
    [SerializeField] float runAwayCheck;

    [Header("MoveSpeed")]
    [SerializeField] bool hasRunAway;
    [SerializeField] bool isRunningAway;
    [SerializeField] float runSpeed;

    [Header("Cover Spots")]
    [SerializeField] Transform[] coverSpots;
    [SerializeField] int currentCoverIndex = -1;

    [Header("Debug")]
    [SerializeField] bool showStateMessages;
    [SerializeField] bool debugAttackRange;

    [Header("References")]
    [SerializeField] Transform playerTrans;
    [SerializeField] RangeEnemy rEnemy;
    [SerializeField] NavMeshAgent navMeshAgent;

    public override void EnterState()
    {
        if (showStateMessages)
            print("Enter Range Enemy Check State");
    }

    public override void Tick()
    {
        if (isRunningAway)
        {
            LookAtPlayerAgain();
        }

        LookAtTarget();
        CheckAction();
    }

    void LookAtTarget()
    {
        Vector3 targetPos = new Vector3(target.position.x, transform.parent.position.y, target.position.z);
        transform.parent.LookAt(targetPos);
    }

    void CheckAction()
    {
        // if player is too close, run to closest cover ONCE
        if (Vector3.Distance(transform.position, target.position) <= runAwayCheck)
        {
            if (!hasRunAway)
            {
                hasRunAway = true;
                isRunningAway = true;

                FindClosestCover();
            }
        }
        // else if player is in attack range, shoot
        else if (Vector3.Distance(transform.position, target.position) <= rEnemy.AtkRange)
        {
            if (!isRunningAway)
            {
                stateMachine.ChangeState<RangeAttackState>();
            }
        }
    }

    void FindClosestCover()
    {
        float smallestDist = Mathf.Infinity;
     
        for (int i = 0; i < coverSpots.Length; i++)
        {
            float currentDist = Vector3.Distance(transform.position, coverSpots[i].position);
            if (currentDist < smallestDist && currentCoverIndex != i)
            {
                smallestDist = currentDist;
                currentCoverIndex = i;
            }
        }

        MoveToClosestCover(currentCoverIndex);
    }

    void MoveToClosestCover(int closestCoverIndex)
    {
        target = coverSpots[currentCoverIndex];

        navMeshAgent.speed = runSpeed;
        navMeshAgent.SetDestination(coverSpots[closestCoverIndex].position);
    }

    void LookAtPlayerAgain()
    {
        if (Vector3.Distance(transform.position, coverSpots[currentCoverIndex].position) < 5)
        {
            isRunningAway = false;
            target = playerTrans;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (debugAttackRange)
            Gizmos.DrawWireSphere(transform.position, rEnemy.AtkRange);
        else
            Gizmos.DrawWireSphere(transform.position, runAwayCheck);
    }
}
