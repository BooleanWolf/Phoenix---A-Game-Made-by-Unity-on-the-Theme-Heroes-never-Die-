using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameStatController : MonoBehaviour
{
    public static GameStatController Instance { get; private set; }

    public int victimsSaved { get; private set; } = 0;     // Number of victims saved
    public int victimsNotSaved { get; private set; } = 0;  // Number of victims that could not be saved
    public int playersMoral { get; private set; } = 120;

    public int notSavedThreshold = 2;

    private Coroutine moralResetCoroutine;

    public Text resetTimeText; 
    public int health { get; private set; } = 100;
    public int playerRegen { get; private set; } = 0;

    public GameObject otherMePrefab;
    public GameObject player;

    private bool spawnCooldown = false;

    private AudioSFXManager audioSFXManager;

    public int maxPlayerRegen; 
    private void Awake()
    {
        // Ensure there's only one instance of GameStatController
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

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

    // Method to increment saved victims
    public void IncrementVictimsSaved()
    {
        victimsSaved++;
        Debug.Log(victimsSaved);    
    }

    // Method to increment not saved victims
    public void IncrementVictimsNotSaved()
    {
        victimsNotSaved++;
        Debug.Log(victimsNotSaved);
        //if (victimsNotSaved >= notSavedThreshold)
        //    GameOver();
    }

    public void IncreaseMorality(int inc)
    {
        playersMoral = playersMoral + inc;
        if(playersMoral >=  150)
        {
            playersMoral = 150; 
        }
        Debug.Log(playersMoral);

        if (moralResetCoroutine != null)
        {
            StopCoroutine(moralResetCoroutine); // Reset timer if moral is increased again
        }
        moralResetCoroutine = StartCoroutine(ResetMoralAfterTime(50f)); // Start a new 50-second timer
    }

    private IEnumerator ResetMoralAfterTime(float time)
    {
        float remainingTime = time;

        while (remainingTime > 0)
        {
            resetTimeText.text = remainingTime.ToString("F0") + "s";  // Update countdown display
            yield return new WaitForSeconds(1f);  // Wait for 1 second
            remainingTime -= 1f;
        }
     
        playersMoral = playersMoral - 30;
        resetTimeText.text = "0"; 
        Debug.Log("Moral reset to 0 after 50 seconds");
    }

    public void DecreaseMorality(int dec) { 
        playersMoral = playersMoral - dec; 
        Debug.Log(playersMoral);
    }

   public void decrease_health(int dec)
    {
        health = health - dec;
        if (health <= 0) {
            health = 100;
            SpawnOtherMe(); 
        }
           
        Debug.Log(health);  
    }

    private void SpawnOtherMe()
    {
        if (player != null && otherMePrefab != null && !spawnCooldown)
        {
            GameStatController.Instance.increase_playersRegen(1);
            audioSFXManager.PlayMusicRegen(); 

            Vector3 offsetPosition = player.transform.position + new Vector3(4f, 0, 0);
            GameObject otherMe = Instantiate(otherMePrefab, offsetPosition, Quaternion.identity);

            if(GameStatController.Instance.playerRegen <= 7)
            {
                otherMe.transform.localScale = player.transform.localScale * 0.95f; // Scale it to half the player's size
                player.transform.localScale *= 0.95f;
            }
           
           
            Debug.Log("Spawned OtherMePrefab at half size");
         

            StartCoroutine(SpawnCooldownCoroutine());
        }
    }

    private IEnumerator SpawnCooldownCoroutine()
    {
        spawnCooldown = true; // Activate cooldown
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        spawnCooldown = false; // Reset cooldown
    }

    public void increase_playersRegen(int inc)
    {
        playerRegen = playerRegen + inc;
        if(playerRegen >= maxPlayerRegen)
        {
            GameOver(); 
        }
        Debug.Log(health);
    }

    public void decrease_playersRegen(int dec)
    {
        playerRegen = playerRegen - dec;
        if(playerRegen <= 0)
        {
            playerRegen = 0; 
        }
        Debug.Log(playerRegen);
    }



    public void GameOver()
    {
        Debug.Log("Game Over"); 
        victimsNotSaved = 0;
        victimsSaved = 0;
        playersMoral = 120;
        health = 100;
        playerRegen = 0; 



        SceneManager.LoadScene("GameOver");

    }

    public void onExitClickButton()
    {
        SceneManager.LoadScene("MainMenu");
    }



    // You can add other game statistics methods as needed
}
