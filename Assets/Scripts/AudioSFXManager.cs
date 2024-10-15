using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSFXManager : MonoBehaviour
{
    public AudioClip blessing, die, hurt, revived, run, shoot, bush, regen, enemy_die, not_saved;
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
        audioSource.volume = 0.65f;
        audioSource.Play();
    }

    // Method to play die sound
    public void PlayMusicDie()
    {
        audioSource.clip = die;
        audioSource.volume = 1f;
        audioSource.loop = false;
        audioSource.Play();
    }

    // Method to play hurt sound
    public void PlayMusicHurt()
    {
        audioSource.clip = hurt;
        audioSource.volume = 0.6f;
        audioSource.loop = false;
        audioSource.Play();
    }

    // Method to play revived sound
    public void PlayMusicRevived()
    {
        audioSource.clip = revived;
        audioSource.loop = false;
        audioSource.volume = 0.6f;
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
        audioSource.volume = 0.4f; 
        audioSource.Play();
    }

    public void PlayMusicBush()
    {
        audioSource.clip = bush;
        audioSource.loop = true;
        audioSource.volume = 0.4f; 
        audioSource.Play();
    }

    public void PlayMusicRegen()
    {
        
        audioSource.loop = false;
        audioSource.volume = 1f;
        audioSource.PlayOneShot(regen);
    }

    public void PlayMusicEnemyDie()
    {
        audioSource.clip = enemy_die;
        audioSource.loop = false;
        audioSource.volume = 1f;
        audioSource.Play(); 
    }

    public void PlayMusicNotSaved()
    {
        audioSource.clip = not_saved;
        audioSource.loop = false;
        audioSource.volume = 1f;
        audioSource.Play();
    }
}
