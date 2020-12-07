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

    MeleeSM stateMachine;

    private void Start()
    {
        stateMachine = GetComponentInChildren<MeleeSM>();
    }

    private void Update()
    {
        // if player is in atk range still and atk speed cooldown is refreshed, attack again
    }

    public override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1);

        ResetAnimBools();

        stateMachine.ChangeState<MeleeCheckState>();
    }

    public override void Die()
    {
        stateMachine.ChangeState<MeleeDeathState>();
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
