using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangeEnemy : Enemy
{
    [Header("Combat Settings")]
    [SerializeField] string weapon;
    public string Weapon => weapon;

    [SerializeField] float fireRate;
    public float FireRate => fireRate;

    [Header("Combat References")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float bulletForce;

    [Header("Rot Settings")]
    [SerializeField] float rotSpeed;
    public float RotSpeed => rotSpeed;

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

        if (isAttacking)
        {
            LookAtPlayer();
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

            // FixRotation();

            OnAttackAnimation();

            GameObject bull = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

            EnemyBullet b1 = bull.GetComponent<EnemyBullet>();
            b1.Damage = Damage;

            bull.transform.LookAt(stateMachine.PlayerTrans);
            bull.transform.rotation *= Quaternion.Euler(90, 0, 0);

            Rigidbody rb = bull.GetComponent<Rigidbody>();

            // rb.AddForce(bulletForce * bulletSpawn.right);
            var dir = stateMachine.PlayerTrans.position - bulletSpawn.position;
            rb.AddForce(bulletForce * dir);

            yield return new WaitForSeconds(1 / fireRate);

            ResetAnimBools();

        } while ((Vector3.Distance(transform.position, stateMachine.PlayerTrans.position) <= AtkRange) && !isDead);

        if (!isDead)
            stateMachine.ChangeState<RangeCheckState>();
    }

    public override void Die()
    {
        stateMachine.ChangeState<RangeDeathState>();
        stateMachine.PlayerTrans.GetComponent<Player>().GainExp(Exp);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.IncreaseScore(Exp);
        gameManager.UpdateEnemyKilled();
    }

    public override void LevelUp(int round)
    {
        for (int i = 0; i < round; i++)
        {
            Damage += 1;
            AtkSpeed += 0.1f;
        }
    }

    void LookAtPlayer()
    {
        Vector3 lookAt = new Vector3(stateMachine.PlayerTrans.position.x, transform.position.y, stateMachine.PlayerTrans.position.z);
        transform.LookAt(lookAt);
        transform.rotation *= Quaternion.Euler(0, 45, 0);

        /*
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(stateMachine.PlayerTrans.position.x, 0, 
            stateMachine.PlayerTrans.position.z)), Time.deltaTime * rotSpeed);

        // to fix attack animation
        // transform.Rotate(0, 45, 0);
        */
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
