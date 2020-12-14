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
    [SerializeField] int rifleDamage;
    public int RifleDamage => rifleDamage;

    [SerializeField] int shotgunDamage;
    public int ShotgunDamage => shotgunDamage;

    [SerializeField] int pistolDamage;
    public int PistolDamage => pistolDamage;

    [SerializeField] float rifleFireRate;
    public float RifleFireRate => rifleFireRate;

    [SerializeField] float shotgunFireRate;
    public float ShotgunFireRate => shotgunFireRate;

    [SerializeField] float pistolFireRate;
    public float PistolFireRate => pistolFireRate;

    [SerializeField] float bulletSize;
    public float BulletSize => bulletSize;

    [SerializeField] bool cantTakeDamage;
    public bool CantTakeDamage => cantTakeDamage;
    [SerializeField] float cantTakeDamageDuration;

    [Header("Level Up Settings")]
    [SerializeField] int hpIncrementAmt;
    [SerializeField] int expIncrementAmt;

    [SerializeField] int rifleDamageUpgradeAmt;
    [SerializeField] int shotgunDamageUpgradeAmt;
    [SerializeField] int pistolDamageUpgradeAmt;

    [SerializeField] float rifleFireRateUpgrade;
    [SerializeField] float shotgunFireRateUpgrade;
    [SerializeField] float pistolFireRateUpgrade;

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
        if (!cantTakeDamage)
        {
            currentHP -= damage;

            if (currentHP > maximumHP)
            {
                currentHP = maximumHP;
            }

            if (currentHP <= 0)
            {
                Die();
            }

            uiGameManager.UpdateHP(true, true);

            StartCoroutine(CantTakeDamageCoroutine());
        }
    }

    IEnumerator CantTakeDamageCoroutine()
    {
        cantTakeDamage = true;

        yield return new WaitForSeconds(cantTakeDamageDuration);

        cantTakeDamage = false;
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

        UpgradeDamage();
        UpgradeFireRate();
    }

    public void UpgradeDamage()
    {
        rifleDamage += rifleDamageUpgradeAmt;
        shotgunDamage += shotgunDamageUpgradeAmt;
        pistolDamage += pistolDamageUpgradeAmt;

    }

    public void UpgradeFireRate()
    {
        rifleFireRate += rifleFireRateUpgrade;
        shotgunFireRate += shotgunFireRateUpgrade;
        pistolFireRate += pistolFireRateUpgrade;
    }

    public virtual void Die()
    {

    }
}
