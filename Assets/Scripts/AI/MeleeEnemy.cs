using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeEnemy : Enemy
{
    [Header("Combat Settings")]
    [SerializeField] string weapon;
    public string Weapon => weapon;

    // state Machine
    [Header("State Machine")]
    [SerializeField] bool isWalking;
    [SerializeField] bool isRunning;
    [SerializeField] bool isAttacking;
    [SerializeField] bool isDead;

    public event Action OnWalk = delegate { };
    public event Action OnRun = delegate { };
    public event Action OnAttack = delegate { };
    public event Action OnDeath = delegate { };

    Vector3 initPos;

    MeleeSM stateMachine;

    private void Start()
    {
        stateMachine = GetComponentInChildren<MeleeSM>();
    }

    private void Update()
    {

    }

    public override void Attack()
    {
        initPos = transform.position;

        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        do
        {
            transform.position = initPos;
            LookAtPlayer();

            OnAttackAnimation();

            yield return new WaitForSeconds(1.5f);

            ResetAnimBools();

        } while (Vector3.Distance(transform.position, stateMachine.PlayerTrans.position) <= AtkRange);

        stateMachine.ChangeState<MeleeCheckState>();
    }

    public override void Die()
    {
        stateMachine.ChangeState<MeleeDeathState>();
    }

    void LookAtPlayer()
    {
        Vector3 lookAt = new Vector3(stateMachine.PlayerTrans.position.x, transform.position.y, stateMachine.PlayerTrans.position.z);
        transform.LookAt(lookAt);

        // to fix attack animation
        transform.Rotate(0, 30, 0);
    }

    // animation
    public void OnWalkAnimation()
    {
        if (!isWalking)
        {
            ResetAnimBools();

            OnWalk.Invoke();
            isWalking = true;
        }
    }

    public void OnRunAnimation()
    {
        if (!isRunning)
        {
            ResetAnimBools();

            OnRun.Invoke();
            isRunning = true;
        }
    }

    public void OnAttackAnimation()
    {
        if (!isAttacking)
        {
            ResetAnimBools();

            OnAttack.Invoke();
            isAttacking = true;
        }
    }

    public void OnDeathAnimation()
    {
        if (!isDead)
        {
            ResetAnimBools();

            OnDeath.Invoke();
            isDead = true;
        }
    }

    public void ResetAnimBools()
    {
        isWalking = false;
        isRunning = false;
        isAttacking = false;
    }
}
