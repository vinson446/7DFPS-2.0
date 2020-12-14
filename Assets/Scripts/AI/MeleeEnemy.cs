using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class MeleeEnemy : Enemy
{
    [Header("Combat Settings")]
    [SerializeField] string weapon;
    public string Weapon => weapon;
    [SerializeField] Transform axeHitBox;
    public Transform AxeHitBox => axeHitBox;
    [SerializeField] Transform knifeHitBox;
    public Transform KnifeHitBox => knifeHitBox;

    [SerializeField] Transform fistHitBox;
    public Transform FistHitBox => fistHitBox;

    [SerializeField] float axeWaitTime;
    [SerializeField] float knifeWaitTime;
    [SerializeField] float fistWaitTime;

    // state Machine
    [Header("State Machine")]
    [SerializeField] bool isWalking;
    [SerializeField] bool isRunning;
    [SerializeField] bool isAttacking;

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

            Collider[] colls;
            if (weapon == "Axe")
            {
                yield return new WaitForSeconds(axeWaitTime);

                colls = Physics.OverlapSphere(axeHitBox.position, AtkRange);
                foreach (Collider coll in colls)
                {
                    if (coll.tag == "Player")
                    {
                        stateMachine.PlayerTrans.GetComponent<Player>().TakeDamage(Damage);

                        break;
                    }
                }
            }
            else if (weapon == "Knife")
            {
                yield return new WaitForSeconds(knifeWaitTime);

                colls = Physics.OverlapSphere(knifeHitBox.position, AtkRange);
                foreach (Collider coll in colls)
                {
                    if (coll.tag == "Player")
                    {
                        stateMachine.PlayerTrans.GetComponent<Player>().TakeDamage(Damage);
                        break;
                    }
                }
            }
            else if (weapon == "Fist")
            {
                yield return new WaitForSeconds(fistWaitTime);

                colls = Physics.OverlapSphere(fistHitBox.position, AtkRange);
                foreach (Collider coll in colls)
                {
                    if (coll.tag == "Player")
                    {
                        stateMachine.PlayerTrans.GetComponent<Player>().TakeDamage(Damage);
                        break;
                    }
                }
            }

            // attack once per second
            yield return new WaitForSeconds(AtkSpeed);

            ResetAnimBools();

        } while (Vector3.Distance(transform.position, stateMachine.PlayerTrans.position) <= AtkRange && !isDead);

        if (!isDead)
            stateMachine.ChangeState<MeleeCheckState>();
    }

    public override void Die()
    {
        stateMachine.ChangeState<MeleeDeathState>();
    }

    public override void LevelUp(int round)
    {
        for (int i = 0; i < round - 1; i++)
        {
            MaximumHP += 3;
            Level += 1;

            Damage += 2;
        }

        EnemyUI enemyUI = GetComponentInChildren<EnemyUI>();
        enemyUI.UpdateLevel();
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
