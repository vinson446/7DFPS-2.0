using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeRunState : RangeState
{
    [Header("Runaway Settings")]
    [SerializeField] float runAwayCheck;
    public float RunAwayCheck => runAwayCheck;
    [SerializeField] bool hasRunAway;
    public bool HasRunAway => hasRunAway;
    [SerializeField] bool isRunningAway;

    [SerializeField] Transform[] coverSpots;
    [SerializeField] int currentCoverIndex = -1;

    RangeCheckState rangeCheckState;

    public override void EnterState()
    {
        stateMachine.REnemy.ChangeState("Range Run State");

        rangeCheckState = GetComponent<RangeCheckState>();

        hasRunAway = true;
        isRunningAway = true;

        FindClosestCover();
    }

    public override void Tick()
    {
        if (Vector3.Distance(transform.position, coverSpots[currentCoverIndex].position) <= 3 && hasRunAway)
        {
            stateMachine.NavAgent.enabled = false;

            isRunningAway = false;
            stateMachine.ChangeState<RangeCheckState>();
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

        MoveToClosestCover();
    }

    void MoveToClosestCover()
    {
        // Transform target = coverSpots[currentCoverIndex];

        stateMachine.NavAgent.speed = rangeCheckState.RunSpeed;
        stateMachine.NavAgent.SetDestination(coverSpots[currentCoverIndex].position);

        stateMachine.REnemy.OnRunAnimation();
    }
}
