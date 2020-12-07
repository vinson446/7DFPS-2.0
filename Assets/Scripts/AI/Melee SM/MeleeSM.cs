using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeSM : StateMachine
{
    Transform playerTrans;
    public Transform PlayerTrans { get => playerTrans; set => playerTrans = value; }

    MeleeEnemy mEnemy;
    public MeleeEnemy MEnemy { get => mEnemy; set => mEnemy = value; }

    NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get => navAgent; set => navAgent = value; }

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = FindObjectOfType<Player>().transform;
        mEnemy = GetComponentInParent<MeleeEnemy>();
        navAgent = GetComponentInParent<NavMeshAgent>();

        ChangeState<MeleeCheckState>();
    }
}
