using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Slider hpBar;
    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI levelText;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        UpdateHP();
        UpdateExp();
        UpdateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHP()
    {
        hpText.text = player.CurrentHP.ToString() + " / " + player.MaximumHP.ToString();

        hpBar.maxValue = player.MaximumHP;
        hpBar.value = player.CurrentHP;
    }

    public void UpdateExp()
    {
        expBar.maxValue = player.MaximumExp;
        expBar.value = player.CurrentExp;
    }

    public void UpdateLevel()
    {
        levelText.text = player.Level.ToString();

        UpdateHP();
        UpdateExp();
    }
}
