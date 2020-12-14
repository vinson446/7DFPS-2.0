using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMEBGM : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] clips;

    public int currentMusic = -1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayAudioSequentially());
    }

    IEnumerator PlayAudioSequentially()
    {
        yield return null;

        while (true)
        {
            if (currentMusic == 2)
            {
                currentMusic = 0;
            }
            else
            {
                currentMusic += 1;
            }

            audioSource.clip = clips[currentMusic];

            audioSource.Play();

            while (audioSource.isPlaying)
            {
                yield return null;
            }

        }
    }
}
