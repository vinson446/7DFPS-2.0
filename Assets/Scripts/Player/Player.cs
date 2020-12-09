using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] int level;
    public int Level => level;

    [SerializeField] int currentExp;
    public int CurrentExp => currentExp;
    [SerializeField] int maximumExp;
    public int MaximumExp => maximumExp;

    [Header("HP Settings")]
    [SerializeField] int currentHP;
    public int CurrentHP => currentHP;
    [SerializeField] int maximumHP;
    public int MaximumHP => maximumHP;

    [Header("Combat Settings")]
    [SerializeField] int damage;
    public int Damage => damage;

    [SerializeField] float fireRate;
    public float FireRate => fireRate;

    [SerializeField] float bulletSize;
    public float BulletSize => bulletSize;

    [Header("Level Up Settings")]
    [SerializeField] int hpIncrementAmt;
    [SerializeField] int expIncrementAmt;
    [SerializeField] int damageUpgradeAmt;
    [SerializeField] float fireRateUpgrade;
    [SerializeField] float bulletSizeUpgrade;

    // events
    public UnityEvent TakeDamageEvent;
    public UnityEvent GainExpEvent;
    public UnityEvent LevelUpEvent;

    UIGameManager uiGameManager;

    // Start is called before the first frame update
    void Start()
    {
        uiGameManager = FindObjectOfType<UIGameManager>();

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
        else
        {
            uiGameManager.UpdateHP(true, true);
        }
    }

    public void GainExp(int exp)
    {
        currentExp += exp;

        if (currentExp >= maximumExp)
        {
            LevelUp();
        }

        uiGameManager.UpdateExp();
    }

    public void LevelUp()
    {
        level += 1;

        maximumHP += hpIncrementAmt;
        currentHP = maximumHP;

        currentExp = 0;
        maximumExp += expIncrementAmt;

        uiGameManager.UpdateLevel();
    }

    public void UpgradeDamage()
    {
        damage += damageUpgradeAmt;
    }

    public void UpgradeFireRate()
    {
        fireRate += fireRateUpgrade;
    }

    public void UpgradeBulletSize()
    {
        bulletSize += bulletSizeUpgrade;
    }

    public virtual void Die()
    {

    }
}
