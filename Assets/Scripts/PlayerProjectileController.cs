using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{

    public float speed = 10f; // Speed of the projectile
    public float lifetime = 1f; // Time before the projectile is destroyed
    // Start is called before the first frame update

    private AudioSFXManager audioSFXManager;
    void Start()
    {
        Destroy(gameObject, lifetime);

        GameObject audioObject = GameObject.FindWithTag("Audio");
        if (audioObject != null)
        {
            audioSFXManager = audioObject.GetComponent<AudioSFXManager>();
        }
        else
        {
            Debug.LogWarning("Audio GameObject with tag 'audio' not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioSFXManager.PlayMusicEnemyDie(); 
            // Implement damage logic here (e.g., reducing player's health)
            Debug.Log("Hit the Enemy!");

            Destroy(collision.gameObject);
            GameStatController.Instance.DecreaseMorality(10); 

            Destroy(gameObject); // Destroy the projectile on hit
        }
        
    }
}
