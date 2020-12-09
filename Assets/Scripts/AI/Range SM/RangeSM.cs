using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeSM : StateMachine
{
    Transform playerTrans;
    public Transform PlayerTrans { get => playerTrans; set => playerTrans = value; }

    RangeEnemy rEnemy;
    public RangeEnemy REnemy { get => rEnemy; set => rEnemy = value; }

    NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get => navAgent; set => navAgent = value; }

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = FindObjectOfType<Player>().transform;
        rEnemy = GetComponentInParent<RangeEnemy>();
        navAgent = GetComponentInParent<NavMeshAgent>();

        ChangeState<RangeCheckState>();
    }
}
