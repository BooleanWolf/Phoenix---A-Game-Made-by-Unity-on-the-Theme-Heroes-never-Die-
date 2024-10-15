using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class TeleportController : MonoBehaviour
{
    private bool isPlayerInTeleportZone = false;
  

    private AudioSFXManager audioSFXManager;

    public GameObject pressTbox; 

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

        pressTbox.SetActive(false);
    }
    private void Update()
    {
        // Check if player is in the teleport zone and the "T" key is pressed
        if (isPlayerInTeleportZone)
        {
           


            if (Input.GetKeyDown(KeyCode.T)) {
                GameStatController.Instance.IncreaseMorality(30);
                audioSFXManager.PlayMusicBlessing(); 
            }

            
        } 

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit it");
            // Set flag to true when player enters teleport zone
            isPlayerInTeleportZone = true;
            pressTbox.SetActive(true);


        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reset flag when player leaves teleport zone
            isPlayerInTeleportZone = false;
            pressTbox.SetActive(false);

        }
    }
}
