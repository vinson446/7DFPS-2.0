using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] AudioClip[] audioClips;

    // spawn settings
    GameObject audioObj;
    GameObject audioParentObj;

    // call before playing sound
    // obj- gameobj with the AudioClipSetting script
    public void SetAudioObj(GameObject obj)
    {
        audioObj = obj;
    }

    // call before playing sound (optional)
    // obj- any gameobj
    public void SetAudioParent(GameObject obj)
    {
        audioParentObj = obj;
    }

    public void PlayOneShot(int clipIndex)
    {
        GameObject sfx = Instantiate(audioObj, transform.position, Quaternion.identity);
        if (audioParentObj != null)
            sfx.transform.SetParent(audioParentObj.transform);
        audioParentObj = null;

        AudioSource audioSource = sfx.GetComponent<AudioSource>();
        AudioClipSettings audioClipSettings = sfx.GetComponent<AudioClipSettings>();

        audioClipSettings.SetPlayOneShot();
        if (audioClipSettings.FadeIn)
        {
            FadeInSound(sfx);
        }
        else if (audioClipSettings.FadeOut)
        {
            FadeOutSound(sfx);
        }

        audioSource.PlayOneShot(audioClips[clipIndex]);
    }

    public void PlayOneShotRandomPitch(int clipIndex)
    {
        GameObject sfx = Instantiate(audioObj, transform.position, Quaternion.identity);
        if (audioParentObj != null)
            sfx.transform.SetParent(audioParentObj.transform);
        audioParentObj = null;

        AudioSource audioSource = sfx.GetComponent<AudioSource>();
        AudioClipSettings audioClipSettings = sfx.GetComponent<AudioClipSettings>();

        audioClipSettings.SetPlayOneShotRandomPitch();
        if (audioClipSettings.FadeIn)
        {
            FadeInSound(sfx);
        }
        else if (audioClipSettings.FadeOut)
        {
            FadeOutSound(sfx);
        }

        audioSource.PlayOneShot(audioClips[clipIndex]);
    }

    public void PlayLoopingSound(int clipIndex)
    {
        GameObject sfx = Instantiate(audioObj, transform.position, Quaternion.identity);
        if (audioParentObj != null)
            sfx.transform.SetParent(audioParentObj.transform);
        audioParentObj = null;

        AudioSource audioSource = sfx.GetComponent<AudioSource>();
        AudioClipSettings audioClipSettings = sfx.GetComponent<AudioClipSettings>();

        audioClipSettings.SetPlayLoopingSound();
        if (audioClipSettings.FadeIn)
        {
            FadeInSound(sfx);
        }
        else if (audioClipSettings.FadeOut)
        {
            FadeOutSound(sfx);
        }

        audioSource.clip = audioClips[clipIndex];
        audioSource.Play();
    }

    public void FadeInSound(GameObject audioObj)
    {
        StartCoroutine(FadeInSoundCoroutine(audioObj));
    }

    IEnumerator FadeInSoundCoroutine(GameObject audioObj)
    {
        float timer = 0;

        AudioSource audioSource = audioObj.GetComponent<AudioSource>();
        AudioClipSettings audioClipSettings = audioObj.GetComponent<AudioClipSettings>();

        audioSource.volume = 0;

        while (timer < audioClipSettings.FadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, audioClipSettings.Volume, timer / audioClipSettings.FadeDuration);
            yield return null;
        }

        yield break;
    }

    public void FadeOutSound(GameObject audioObj)
    {
        StartCoroutine(FadeOutSoundCoroutine(audioObj));
    }

    IEnumerator FadeOutSoundCoroutine(GameObject audioObj)
    {
        float timer = 0;

        AudioSource audioSource = audioObj.GetComponent<AudioSource>();
        AudioClipSettings audioClipSettings = audioObj.GetComponent<AudioClipSettings>();

        audioSource.volume = audioClipSettings.Volume;

        while (timer < audioClipSettings.FadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(audioClipSettings.Volume, 0, timer / audioClipSettings.FadeDuration);
            yield return null;
        }

        yield break;
    }
}
