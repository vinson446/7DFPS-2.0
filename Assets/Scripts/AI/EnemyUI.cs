using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] GameObject[] UI;
    [SerializeField] TextMeshProUGUI leelText;
    [SerializeField] Slider hpBar;

    Enemy enemy;

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        SetupHp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupHp()
    {
        hpBar.maxValue = enemy.MaximumHP;
        hpBar.value = enemy.CurrentHP;
    }

    public void UpdateHP()
    {
        TurnOnOffUI(true);
        hpBar.value = enemy.CurrentHP;
    }

    public void UpdateLevel()
    {
        leelText.text = enemy.Level.ToString();
        TurnOnOffUI(false);
    }

    public void TurnOnOffUI(bool on)
    {
        if (on)
        {
            foreach (GameObject o in UI)
            {
                o.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject o in UI)
            {
                o.SetActive(false);
            }
        }
    }
}
