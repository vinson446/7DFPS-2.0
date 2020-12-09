using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangeEnemy : Enemy
{
    [Header("Combat Settings")]
    [SerializeField] string weapon;
    public string Weapon => weapon;

    // state Machine
    [Header("State Machine")]
    [SerializeField] bool isIdle;
    [SerializeField] bool isRunning;
    [SerializeField] bool isAttacking;
    [SerializeField] bool isDead;

    public event Action OnIdle = delegate { };
    public event Action OnRun = delegate { };
    public event Action OnAttack = delegate { };
    public event Action OnDeath = delegate { };

    Vector3 initPos;

    RangeSM stateMachine;
    RangeRunState rangeRunState;

    private void Start()
    {
        stateMachine = GetComponentInChildren<RangeSM>();
        rangeRunState = GetComponentInChildren<RangeRunState>();
    }

    private void Update()
    {
        if ((Vector3.Distance(transform.position, stateMachine.PlayerTrans.position) <= rangeRunState.RunAwayCheck) 
                && !rangeRunState.HasRunAway)
        {
            StopAllCoroutines();
            stateMachine.ChangeState<RangeRunState>();
        }
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

            yield return new WaitForSeconds(0.5f);

            stateMachine.PlayerTrans.GetComponent<Player>().TakeDamage(Damage);

            yield return new WaitForSeconds(1f);

            ResetAnimBools();

        } while ((Vector3.Distance(transform.position, stateMachine.PlayerTrans.position) <= AtkRange));

        stateMachine.ChangeState<RangeCheckState>();
    }

    public override void Die()
    {
        stateMachine.ChangeState<RangeDeathState>();
    }

    void LookAtPlayer()
    {
        Vector3 lookAt = new Vector3(stateMachine.PlayerTrans.position.x, transform.position.y, stateMachine.PlayerTrans.position.z);
        transform.LookAt(lookAt);

        // to fix attack animation
        transform.Rotate(0, 45, 0);
    }

    // animation
    public void OnIdleAnimation()
    {
        if (!isIdle)
        {
            ResetAnimBools();

            OnIdle.Invoke();
            isIdle = true;
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
        isIdle = false;
        isRunning = false;
        isAttacking = false;
    }
}
