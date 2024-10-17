using UnityEngine;
using UnityEngine.UI;

public class VictimSpawner : MonoBehaviour
{
    public GameObject dummyVictimPrefab; // Assign the DummyVictim prefab in the Inspector
    public Transform[] spawnPoints;       // Array of predefined spawn positions
    public float respawnDelay = 2f;       // Time to wait before respawning a victim

    public GameObject currentVictim; 
    private GameObject currentDummyVictim; // Reference to the current DummyVictim
    private bool isRespawning = false;      // Tracks if a respawn is in progress


    public Text cText;
    public Text rtText;
    public Text vText;
    public GameObject player; 

    private void Start()
    {
        SpawnDummyVictim();
    }

    private void Update()
    {
        // Check for some condition to respawn the DummyVictim
        // For example, if it gets destroyed
        if (currentDummyVictim == null && !isRespawning)
        {
            // Start the respawn coroutine after the specified delay
            StartCoroutine(RespawnDummyVictim());
        }
    }

    void SpawnDummyVictim()
    {
        // Select a random spawn point from the array
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instantiate the DummyVictim prefab at the selected spawn point
        currentDummyVictim = Instantiate(dummyVictimPrefab, spawnPoint.position, spawnPoint.rotation);
        VictimController victimController = currentDummyVictim.GetComponent<VictimController>();

        if (currentDummyVictim != null) {
            victimController.countdownText = cText;
            victimController.remainingTimeText = rtText;
            victimController.victimPositionText = vText; 
            victimController.player = player;
        }
        currentVictim = currentDummyVictim;

         
    }

    private System.Collections.IEnumerator RespawnDummyVictim()
    {
        isRespawning = true; // Set to true to prevent multiple respawns
        cText.text = "Go Find Another. Follow the direction and distance!";
        // Wait for the respawn delay
        yield return new WaitForSeconds(respawnDelay);

        cText.text = "";

        SpawnDummyVictim(); // Spawn a new victim
        isRespawning = false; // Reset the respawning flag
    }

    // Call this method when the victim is freed
    public void VictimFreed()
    {
        if (currentDummyVictim != null)
        {
            Destroy(currentDummyVictim); // Destroy the current victim
            currentDummyVictim = null;   // Clear the reference
            cText.text = "You saved him!"; 
        }
       
    }
}
