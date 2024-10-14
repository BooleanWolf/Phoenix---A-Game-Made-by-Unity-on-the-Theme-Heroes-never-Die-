using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TeleportController : MonoBehaviour
{
    private bool isPlayerInTeleportZone = false;
    public Text countDownText;

    private AudioSFXManager audioSFXManager;
    //private Coroutine fadeOutCoroutine;

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
    }
    private void Update()
    {
        // Check if player is in the teleport zone and the "T" key is pressed
        if (isPlayerInTeleportZone)
        {
           
            Debug.Log(countDownText.text);  
            if (Input.GetKeyDown(KeyCode.T)) {
                GameStatController.Instance.IncreaseMorality(30);
                audioSFXManager.PlayMusicBlessing(); 
            }
            
        } 
        else
        {
           
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit it");
            // Set flag to true when player enters teleport zone
            isPlayerInTeleportZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reset flag when player leaves teleport zone
            isPlayerInTeleportZone = false;
        }
    }
}
