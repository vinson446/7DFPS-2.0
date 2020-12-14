using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("HP Settings")]
    [SerializeField] int currentHP;
    public int CurrentHP { get => currentHP; set => currentHP = value; }

    [SerializeField] int maximumHP;
    public int MaximumHP { get => maximumHP; set => maximumHP = value; }

    [Header("Combat Settings")]
    [SerializeField] int level;
    public int Level { get => level; set => level = value; }

    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] float atkSpeed;
    public float AtkSpeed { get => atkSpeed; set => atkSpeed = value; }

    [SerializeField] float atkRange;
    public float AtkRange => atkRange;

    [Header("Debug")]
    [SerializeField] string currentState;

    EnemyUI enemyUI;

    protected bool isDead;

    // Start is called before the first frame update
    void Awake()
    {
        enemyUI = GetComponentInChildren<EnemyUI>();
    }

    public virtual void Attack()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        enemyUI.UpdateHP();

        if (currentHP <= 0 && !isDead)
        {
            Die();
        }
    }

    public abstract void Die();

    public void ChangeState(string state)
    {
        currentState = state;
    }

    public abstract void LevelUp(int round);

}
