using UnityEngine;
using UnityEngine.UI; // Import UI for displaying countdown
using System.Collections;

public class VictimController : MonoBehaviour
{
    public Text countdownText;     // Reference to the UI Text element for countdown display
    public float holdTime = 2f;    // Time required to hold "H" to free the victim
    public float maxHoldTime = 20f; // Time before considering the victim not saved

    private float holdCounter = 0f; // Counter to track how long "H" has been held
    private bool isNearPlayer = false; // Tracks if the player is nearby
    public float notSavedCounter = 20f; // Counter to track time until victim is considered not saved

    private VictimSpawner victimSpawner;

    public Text remainingTimeText;
    public GameObject player;
    public Text victimPositionText; 
    

    private Vector3 offsetAbove = new Vector3(0, 0.1f, 0);   // Offset for above player
    //private Vector3 offsetBelow = new Vector3(0, -0.6f, 0);  // Offset for below player
    //private Vector3 offsetLeft = new Vector3(-0.6f, 0, 0);   // Offset for left of player
    //private Vector3 offsetRight = new Vector3(0.6f, 0, 0);   // Offset for right of player

    private Vector3 upPos;
    private Vector3 downPos;
    private Vector3 leftPos;
    private Vector3 rightPos;

    private AudioSFXManager audioSFXManager;


    void Start()
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

        victimSpawner = FindObjectOfType<VictimSpawner>();
    }

    public void Increase_NotSavedCounter()
    {
        notSavedCounter = notSavedCounter + 5f; 
    }

    void Update()
    {
        notSavedCounter += Time.deltaTime;
        //Debug.Log(notSavedCounter); 

        float rt = maxHoldTime - notSavedCounter; 
        remainingTimeText.text = "" + rt.ToString("F0");


        if (notSavedCounter >= maxHoldTime)
        {
            NotSavedVictim();
        }

        if (isNearPlayer)
        {
            // Update the not saved counter
            

            // Check if the player is pressing the "H" key
            if (Input.GetKey(KeyCode.H) && GameStatController.Instance.playersMoral >= 30)
            {
                // Increment the hold counter while "H" is held
                holdCounter += Time.deltaTime;

                // Update countdown display
                countdownText.text = (holdTime - holdCounter).ToString("F1") + "s";

                // Check if hold time is met to free the victim
                if (holdCounter >= holdTime)
                {
                    FreeVictim();
                }
            }
            else if (GameStatController.Instance.playersMoral >= 30)
            {
                // Reset the hold counter if "H" is released
                holdCounter = 0f;
                countdownText.text = "Hold H to free him"; // Clear countdown display
            }
            else
            {
                // Reset the hold counter if "H" is released
                holdCounter = 0f;
                countdownText.text = "You don't have enough blessings"; // Clear countdown display
            }

            // Check if the not saved timer has reached the maximum hold time
           
        }
        else
        {
            countdownText.text = ""; // Clear countdown display when player is not nearby
            // Reset not saved counter when the player is not near
        }


        upPos = player.transform.position + offsetAbove;
        //downPos = player.transform.position + offsetBelow;
        //leftPos = player.transform.position + offsetLeft;
        //rightPos = player.transform.position + offsetRight;

        

        float distance = Vector3.Distance(player.transform.position,transform.position);
        Vector3 directionToVictim = (transform.position - player.transform.position).normalized;

        string side = CheckWhichSide(player.transform.position, transform.position);
        //Debug.Log(side);


        //if (side == "Above")
        //{
        //    victimPositionText.rectTransform.localPosition = upPos;

        //}
        //else if (side == "Below")
        //{
        //    victimPositionText.rectTransform.localPosition = downPos;

        //}
        //else if (side == "Left")
        //{
        //    victimPositionText.rectTransform.localPosition = leftPos;

        //}
        //else if (side == "Right")
        //{
        //    victimPositionText.rectTransform.localPosition = rightPos;

        //}

        // Update the victim position text with the distance
        victimPositionText.text = side[0] + distance.ToString("F1") + "m";

        //victimPositionText.rectTransform.localPosition = upPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the area near the victim
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player leaves the area near the victim
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = false;
            holdCounter = 0f;
            countdownText.text = ""; // Clear countdown display
        }
    }

    private void FreeVictim()
    {
        // Code to free the victim
        Debug.Log("Victim freed!");
        GameStatController.Instance.DecreaseMorality(30);
        audioSFXManager.PlayMusicRevived(); 
        countdownText.text = ""; // Clear countdown display
        gameObject.SetActive(false); // Hides the victim, marking them as freed

        // Call the GameStatController to increment saved victims
        GameStatController.Instance.IncrementVictimsSaved();

        // Notify the spawner to free the victim
        if (victimSpawner != null)
        {
            victimSpawner.VictimFreed(); // Call the spawner's method to handle freeing the victim
        }
    }

    private void NotSavedVictim()
    {
        // Code to handle victim not saved scenario
        Debug.Log("Legend Not Saved in Time!");
        audioSFXManager.PlayMusicNotSaved();     
        StartCoroutine(DisplayMessage());
        GameStatController.Instance.IncrementVictimsNotSaved();

        
        // Optionally deactivate the victim or handle accordingly
        gameObject.SetActive(false); // Hide the victim, marking them as not saved


        if (victimSpawner != null)
        {
            victimSpawner.VictimFreed(); // Call the spawner's method to handle freeing the victim
        }

       
    }

    private IEnumerator DisplayMessage()
    {
        // Show the message
        countdownText.text = "One Legend died! Hurry up!";

        // Wait for 2 seconds
        yield return new WaitForSeconds(1.5f);

        // Clear the message
        countdownText.text = "";
    }
    public static string CheckWhichSide(Vector3 a, Vector3 b)
    {
        // Calculate the direction vector from a to b
        Vector3 direction = (b - a).normalized;

        // Determine which side b is relative to a based on the direction vector
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // b is more to the left or right
            return direction.x > 0 ? "Right" : "Left";
        }
        else
        {
            // b is more above or below
            return direction.y > 0 ? "Above" : "Below";
        }
    }
}
