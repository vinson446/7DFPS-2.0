using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] Image[] imageToFade;
    [SerializeField] float[] alphaToFadeTo;
    [SerializeField] float[] durationForFade;

    // for button calls
    public void FadeInOutImage(int index)
    {
        FadeInOutUIElementAlpha(imageToFade[index], alphaToFadeTo[index], durationForFade[index]);
    }

    public void FadeInOutUIElementAlpha(Image image, float fadeAlpha, float duration)
    {
        image.DOFade(fadeAlpha, duration);
    }
}
