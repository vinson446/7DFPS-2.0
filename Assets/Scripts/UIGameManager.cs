using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIGameManager : MonoBehaviour
{
    [Header("Top Left References")]
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Slider hpBar;
    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI levelText;

    [Header("Middle Center References")]
    [SerializeField] TextMeshProUGUI roundCompleteText1;
    [SerializeField] TextMeshProUGUI roundCompleteText2;
    [SerializeField] TextMeshProUGUI newRoundText;

    [Header("Top Right References")]
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI enemyCountText;

    [Header("Bot Right References")]
    [SerializeField] Image weaponImage;
    [SerializeField] Sprite[] weaponSprites;
    [SerializeField] TextMeshProUGUI ammoText;

    [Header("HP Image Settings")]
    [SerializeField] Image HPImage;
    [SerializeField] Sprite[] hpSprites;

    [SerializeField] float hurtFadeInDuration;
    [SerializeField] float hurtDuration;
    [SerializeField] float hurtFadeOutDuration;

    [SerializeField] float healFadeInDuration;
    [SerializeField] float healDuration;
    [SerializeField] float healFadeOutDuration;
    bool isHurt;

    // references
    Player player;
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();

        UpdateHP(false, false);
        UpdateExp();
        UpdateLevel();

        UpdateRound(1);
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHP(bool needImage, bool hurt)
    {
        hpText.text = player.CurrentHP.ToString() + " / " + player.MaximumHP.ToString();

        hpBar.maxValue = player.MaximumHP;
        hpBar.value = player.CurrentHP;

        if (needImage)
        {
            if (hurt)
            {
                StartCoroutine(HPHurtCoroutine());
            }
            else
            {
                StartCoroutine(HPHealCoroutine());
            }
        }
    }

    IEnumerator HPHurtCoroutine()
    {
        HPImage.sprite = hpSprites[0];

        HPImage.DOFade(1, hurtFadeInDuration);

        yield return new WaitForSeconds(hurtDuration);

        HPImage.DOFade(0, hurtFadeOutDuration);

        yield return new WaitForSeconds(hurtFadeOutDuration);
    }

    IEnumerator HPHealCoroutine()
    {
        HPImage.sprite = hpSprites[1];

        HPImage.DOFade(1, healFadeInDuration);

        yield return new WaitForSeconds(healDuration);

        HPImage.DOFade(0, healFadeOutDuration);

        yield return new WaitForSeconds(healFadeOutDuration);
    }

    public void ShowEnemyCount()
    {
        enemyCountText.text = gameManager.NumEnemiesKilledThisRound.ToString() + " / " + gameManager.TotalNumEnemiesToSpawn.ToString();
    }

    public void UpdateExp()
    {
        expBar.maxValue = player.MaximumExp;
        expBar.value = player.CurrentExp;
    }

    public void UpdateLevel()
    {
        levelText.text = player.Level.ToString();

        UpdateHP(false, false);
        UpdateExp();
    }

    public void UpdateWeaponImage(int index)
    {
        weaponImage.sprite = weaponSprites[index];
    }

    public void UpdateAmmo(int currentAmmo, float maxAmmo)
    {
        ammoText.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();
    }

    public void UpdateRound(int round)
    {
        roundText.text = round.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void StartNewRoundCoroutine()
    {
        StartCoroutine(NewRoundCoroutine());
    }

    IEnumerator NewRoundCoroutine()
    {
        roundCompleteText1.gameObject.SetActive(true);
        roundCompleteText2.gameObject.SetActive(true);
        newRoundText.gameObject.SetActive(true);
        roundCompleteText1.DOFade(1, 0);
        roundCompleteText2.DOFade(1, 0);
        newRoundText.DOFade(1, 0);

        roundCompleteText1.text = "ROUND COMPLETE";
        roundCompleteText2.text = "Starting next Round in...";

        for (int i = 3; i > 0; i--)
        {
            newRoundText.text = i.ToString();

            yield return new WaitForSeconds(1);

        }

        gameManager.StartNewRound();

        roundCompleteText1.DOFade(0, 0.25f);
        roundCompleteText2.DOFade(0, 0.25f);
        newRoundText.DOFade(0, 0.25f);

        yield return new WaitForSeconds(1);

        roundCompleteText1.gameObject.SetActive(false);
        roundCompleteText2.gameObject.SetActive(false);
        newRoundText.gameObject.SetActive(false);
    }
}
