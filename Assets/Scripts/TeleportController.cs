using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    private bool isPlayerInTeleportZone = false;

    private void Update()
    {
        // Check if player is in the teleport zone and the "T" key is pressed
        if (isPlayerInTeleportZone && Input.GetKeyDown(KeyCode.T))
        {
            GameStatController.Instance.IncreaseMorality(30); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
