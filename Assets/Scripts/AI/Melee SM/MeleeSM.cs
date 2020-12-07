using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeSM : StateMachine
{
    MeleeEnemy mEnemy;
    public MeleeEnemy MEnemy { get => mEnemy; set => mEnemy = value; }

    NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get => navAgent; set => navAgent = value; }

    // Start is called before the first frame update
    void Start()
    {
        mEnemy = GetComponentInParent<MeleeEnemy>();
        navAgent = GetComponentInParent<NavMeshAgent>();

        ChangeState<MeleeCheckState>();
    }
}
