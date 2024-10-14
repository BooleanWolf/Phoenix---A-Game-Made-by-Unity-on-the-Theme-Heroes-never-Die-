using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile
    public float lifetime = 0.5f; // Time before the projectile is destroyed

    private AudioSFXManager audioSFXManager;

    private void Start()
    {
        GameObject audioObject = GameObject.FindWithTag("Audio");
        if (audioObject != null)
        {
            audioSFXManager = audioObject.GetComponent<AudioSFXManager>();
        }
        else
        {
            Debug.LogWarning("Audio GameObject with tag 'audio' not found!");
        }
        // Destroy the projectile after a set lifetime
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Implement damage logic here (e.g., reducing player's health)
            Debug.Log("Hit the player!");
            audioSFXManager.PlayMusicHurt();

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplySlowdown(0.2f); // 20% slowdown
            }

            Destroy(gameObject); // Destroy the projectile on hit
        }
        else
        {
            // Destroy the projectile on any other collision
            Destroy(gameObject);
        }
    }
}
