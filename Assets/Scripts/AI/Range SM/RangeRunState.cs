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

    GameObject[] coverSpots;
    [SerializeField] int currentCoverIndex = -1;

    RangeCheckState rangeCheckState;

    public override void EnterState()
    {
        stateMachine.REnemy.ChangeState("Range Run State");

        rangeCheckState = GetComponent<RangeCheckState>();

        hasRunAway = true;
        isRunningAway = true;

        coverSpots = GameObject.FindGameObjectsWithTag("Cover Spots");
        FindClosestCover();
    }

    public override void Tick()
    {
        // print("Distance from closest coer: " + Vector3.Distance(transform.position, coverSpots[currentCoverIndex].transform.position));

        if (Vector3.Distance(transform.position, coverSpots[currentCoverIndex].transform.position) <= 1 && hasRunAway)
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
            float currentDist = Vector3.Distance(transform.position, coverSpots[i].transform.position);

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

        if (stateMachine.NavAgent.enabled == true)
        {
            stateMachine.NavAgent.speed = rangeCheckState.RunSpeed;
            stateMachine.NavAgent.SetDestination(coverSpots[currentCoverIndex].transform.position);

            stateMachine.REnemy.OnRunAnimation();
        }
    }
}
