using UnityEngine;

public class BushController : MonoBehaviour
{
    public Animator playerAnimator;       // Reference to the player's Animator component
    public SpriteRenderer playerSprite;   // Reference to the player's SpriteRenderer component
    private Color originalColor;          // Store the player's original color

    private AudioSFXManager audioSFXManager;
    public bool isHiding = false; 
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
        // Save the original color of the player at the start
        if (playerSprite != null)
        {
            originalColor = playerSprite.color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Set hiding animation and change color to gray
            playerAnimator.SetBool("isHiding", true);
            playerSprite.color = Color.gray;
            isHiding = true;
            audioSFXManager.PlayMusicBush();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Revert hiding animation and restore original color
            playerAnimator.SetBool("isHiding", false);
            playerSprite.color = originalColor;
            isHiding = false;
            audioSFXManager.audioSource.Stop();
        }
    }
}
