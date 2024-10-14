using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;           // The enemy prefab to spawn
    public List<Transform> spawnPoints;      // List of potential spawn points
    public float spawnInterval = 2f;         // Time between spawns
    public int maxEnemies = 10;              // Maximum number of enemies in the scene

    public GameObject bulletPrefab;
    private int currentEnemyCount = 0;       // Counter for currently active enemies

    private void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // Instantiate the enemy at the chosen spawn point
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Pass the projectilePrefab to the spawned enemy
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.projectilePrefab = bulletPrefab;  // Load projectilePrefab
            }
            currentEnemyCount++;

            // Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Call this method to decrease the enemy count when an enemy is destroyed
    public void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
