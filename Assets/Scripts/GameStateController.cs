using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameStatController : MonoBehaviour
{
    public static GameStatController Instance { get; private set; }

    public int victimsSaved { get; private set; } = 0;     // Number of victims saved
    public int victimsNotSaved { get; private set; } = 0;  // Number of victims that could not be saved
    public int playersMoral { get; private set; } = 0;

    public int notSavedThreshold = 2;

    private Coroutine moralResetCoroutine;

    public Text resetTimeText; 

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
        if (victimsNotSaved >= notSavedThreshold)
            GameOver();
    }

    public void IncreaseMorality(int inc)
    {
        playersMoral = playersMoral + inc; 
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

    public void GameOver()
    {
        Debug.Log("Game Over"); 
        victimsNotSaved = 0;
        victimsSaved = 0;
        playersMoral = 0;


        SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

    // You can add other game statistics methods as needed
}
