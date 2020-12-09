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

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        UpdateHP(false, false);
        UpdateExp();
        UpdateLevel();
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
    }

    IEnumerator HPHealCoroutine()
    {
        HPImage.sprite = hpSprites[1];

        HPImage.DOFade(1, healFadeInDuration);

        yield return new WaitForSeconds(healDuration);

        HPImage.DOFade(1, healFadeInDuration);
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

    public void UpdateAmmo(int currentAmmo, float maxAmmo)
    {
        ammoText.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();
    }
}
