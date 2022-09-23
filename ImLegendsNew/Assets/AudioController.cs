using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioClip zombieExplode;
    public AudioClip carDrive;
    public AudioClip carCrash;
    AudioSource audioSource;

    bool crash;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
        InvokeRepeating(nameof(PlayMusic), 0 , carDrive.length);

        crash = false;
    }

    private void Update()
    {
        if (GameManager.instance.gameOver && !crash)
        {
            PlayCraash();
        }
    }
    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void PlayMusic()
    {
        audioSource.PlayOneShot(carDrive);
    }

    internal void PlayCraash()
    {
        audioSource.Stop();
        CancelInvoke(nameof(PlayMusic));
        audioSource.clip = carCrash;
        crash = true;
        audioSource.Play();
    }
}
