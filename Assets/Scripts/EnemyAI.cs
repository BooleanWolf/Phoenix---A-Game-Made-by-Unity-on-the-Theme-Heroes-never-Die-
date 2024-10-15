using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public GameObject hero;             // The hero that this enemy will follow and shoot at
    public GameObject tilemapBush;      // Reference to the tilemap containing bushes
    public GameObject projectilePrefab;  // Prefab for the projectile to be instantiated
    public Transform firePoint;          // Point from which the projectile will be fired
    private BushController bushController; // Reference to the BushController script

    [Header("Movement Settings")]
    public float speed = 2f;            // Speed at which the enemy moves towards the hero
    public float followRange = 5f;      // Distance within which the enemy will start following the hero
    public float shootRange = 5f;      // Distance within which the enemy can shoot the hero
    public float fireRate = 1.5f;          // Time between shots

    private float nextFireTime = 0f;    // Timer for firing projectiles

    private AudioSFXManager audioSFXManager;
    private void Start()
    {
        hero = GameObject.Find("Hero"); // Assumes the hero GameObject is named "Hero"
        tilemapBush = GameObject.Find("Tilemap_Bushes"); // Assumes the tilemap is named "TilemapBush"
        GameObject audioObject = GameObject.FindWithTag("Audio");
        if (audioObject != null)
        {
            audioSFXManager = audioObject.GetComponent<AudioSFXManager>();
        }
        else
        {
            Debug.LogWarning("Audio GameObject with tag 'audio' not found!");
        }


        // Initialize firePoint if it's not set in the Inspector
        if (firePoint == null)
        {
            firePoint = transform; // Default to the enemy's position
        }

        //if (projectilePrefab == null)
        //{
        //    projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile"); // Adjust the path based on your project structure
        //}
    }
    void Update()
    {
        if (tilemapBush != null)
        {
            bushController = tilemapBush.GetComponent<BushController>();
        }
        float distanceToHero = Vector3.Distance(transform.position, hero.transform.position);

       
        //Debug.Log(bushController.isHiding);

        if (distanceToHero <= followRange &&  !bushController.isHiding)
        {
            // Move towards the hero
            transform.position = Vector3.MoveTowards(transform.position, hero.transform.position, speed * Time.deltaTime);

            // Face the hero
            Vector3 direction = (hero.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set the rotation only along the Z-axis to face the hero
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        // Check if the enemy can shoot
        if (distanceToHero <= shootRange && Time.time >= nextFireTime && !bushController.isHiding)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Update the next fire time
        }

    }

    private void Shoot()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Calculate direction to shoot
            Vector3 shootDirection = (hero.transform.position - firePoint.position).normalized;
            rb.velocity = shootDirection * projectile.GetComponent<Projectile>().speed; // Set projectile speed
        }
        else
        {
            Debug.LogWarning("No Rigidbody2D found on the projectile prefab!");
        }
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
                player.ApplyEnemyFreeze(); // 20% slowdown
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
