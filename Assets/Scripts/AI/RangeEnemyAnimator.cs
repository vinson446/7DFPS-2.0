using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAnimator : MonoBehaviour
{
    const string PistolIdleState = "Pistol Idle";
    const string RifleIdleState = "Rifle Idle";
    const string RunState = "Run";
    const string PistolAttackState = "Pistol";
    const string RifleAttackState = "Rifle";
    const string DeathState = "Death";

    RangeEnemy rEnemy;
    Animator animator;

    private void Awake()
    {
        rEnemy = GetComponent<RangeEnemy>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnIdle()
    {
        if (rEnemy.Weapon == "Rifle")
        {
            animator.CrossFadeInFixedTime(RifleIdleState, 0.2f);
        }
        else if (rEnemy.Weapon == "Pistol")
        {
            animator.CrossFadeInFixedTime(PistolIdleState, 0.2f);
        }
    }

    void OnRun()
    {
        animator.CrossFadeInFixedTime(RunState, 0.2f);
    }

    void OnRifle()
    {
        animator.CrossFadeInFixedTime(RifleAttackState, 0.2f);
    }

    void OnPistol()
    {
        animator.CrossFadeInFixedTime(PistolAttackState, 0.2f);
    }

    void OnDeath()
    {
        animator.CrossFadeInFixedTime(DeathState, 0.2f);
    }

    private void OnEnable()
    {
        rEnemy.OnIdle += OnIdle;
        rEnemy.OnRun += OnRun;
        if (rEnemy.Weapon == "Rifle")
            rEnemy.OnAttack += OnRifle;
        else if (rEnemy.Weapon == "Pistol")
            rEnemy.OnAttack += OnPistol;
        rEnemy.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        rEnemy.OnIdle -= OnIdle;
        rEnemy.OnRun -= OnRun;
        if (rEnemy.Weapon == "Rifle")
            rEnemy.OnAttack -= OnRifle;
        else if (rEnemy.Weapon == "Pistol")
            rEnemy.OnAttack -= OnPistol;
        rEnemy.OnDeath -= OnDeath;
    }
}
