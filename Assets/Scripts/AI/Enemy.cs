using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("HP Settings")]
    [SerializeField] int currentHP;
    [SerializeField] int maximumHP;

    [Header("Combat Settings")]
    [SerializeField] int level;
    public int Level { get => level; set => level = value; }

    [SerializeField] int exp;
    public int Exp { get => exp; set => exp = value; }

    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] float atkSpeed;
    public float AtkSpeed { get => atkSpeed; set => atkSpeed = value; }

    [SerializeField] float atkRange;
    public float AtkRange => atkRange;

    [Header("Debug")]
    [SerializeField] string currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maximumHP;
    }

    public virtual void Attack()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
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
