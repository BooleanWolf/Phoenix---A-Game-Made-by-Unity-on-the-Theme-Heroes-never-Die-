using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSFXManager : MonoBehaviour
{
    public AudioClip blessing, die, hurt, revived, run, shoot, bush;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Optional: manage when each sound should play based on certain game events
    }

    // Method to play blessing sound
    public void PlayMusicBlessing()
    {
        audioSource.clip = blessing;
        audioSource.loop = false;
        audioSource.Play();
    }

    // Method to play die sound
    public void PlayMusicDie()
    {
        audioSource.clip = die;
        audioSource.loop = false;
        audioSource.Play();
    }

    // Method to play hurt sound
    public void PlayMusicHurt()
    {
        audioSource.clip = hurt;
        audioSource.loop = false;
        audioSource.Play();
    }

    // Method to play revived sound
    public void PlayMusicRevived()
    {
        audioSource.clip = revived;
        audioSource.loop = false;
        audioSource.Play();
    }

    // Method to play running sound
    public void PlayMusicRun()
    {
        audioSource.clip = run;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Method to play shooting sound
    public void PlayMusicShoot()
    {
        audioSource.clip = shoot;
        audioSource.loop = false; 
        audioSource.Play();
    }

    public void PlayMusicBush()
    {
        audioSource.clip = bush;
        audioSource.loop = true;
        audioSource.Play();
    }
}
