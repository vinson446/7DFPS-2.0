using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioClipSettings : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] float volume;
    public float Volume => volume;
    [SerializeField] float pitch;
    [SerializeField] float existenceTime;

    [Header("Random Pitch Settings")]
    [SerializeField] float randMinPitch;
    [SerializeField] float randMaxPitch;

    [Header("Fade Settings")]
    [SerializeField] bool fadeIn;
    public bool FadeIn => fadeIn;
    [SerializeField] bool fadeOut;
    public bool FadeOut => fadeOut;
    [SerializeField] float fadeDuration;
    public float FadeDuration => fadeDuration;

    private void Start()
    {
        Destroy(gameObject, existenceTime);
    }

    public void SetPlayOneShot()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
    }

    public void SetPlayOneShotRandomPitch()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(randMinPitch, randMaxPitch);
    }

    public void SetPlayLoopingSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = true;
    }
}
