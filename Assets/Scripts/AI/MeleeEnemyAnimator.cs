using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAnimator : MonoBehaviour
{
    const string WalkState = "Walk";
    const string RunState = "Run";
    const string KnifeState = "Knife";
    const string AxeState = "Axe";
    const string DeathState = "Death";

    MeleeEnemy mEnemy;
    Animator animator;

    private void Awake()
    {
        mEnemy = GetComponent<MeleeEnemy>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnWalk()
    {
        animator.CrossFadeInFixedTime(WalkState, 0.2f);
    }

    void OnRun()
    {
        animator.CrossFadeInFixedTime(RunState, 0.2f);
    }

    void OnKnife()
    {
        animator.CrossFadeInFixedTime(KnifeState, 0.2f);
    }

    void OnAxe()
    {
        animator.CrossFadeInFixedTime(AxeState, 0.2f);
    }

    void OnDeath()
    {
        animator.CrossFadeInFixedTime(DeathState, 0.2f);
    }

    private void OnEnable()
    {
        mEnemy.OnWalk += OnWalk;
        mEnemy.OnRun += OnRun;
        if (mEnemy.Weapon == "Knife")
            mEnemy.OnAttack += OnKnife;
        else if (mEnemy.Weapon == "Axe")
            mEnemy.OnAttack += OnAxe;
        mEnemy.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        mEnemy.OnWalk -= OnWalk;
        mEnemy.OnRun -= OnRun;
        if (mEnemy.Weapon == "Knife")
            mEnemy.OnAttack -= OnKnife;
        else if (mEnemy.Weapon == "Axe")
            mEnemy.OnAttack -= OnAxe;
        mEnemy.OnDeath -= OnDeath;
    }
}
