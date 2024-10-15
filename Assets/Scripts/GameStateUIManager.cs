using UnityEngine;
using UnityEngine.UI;

public class GameStatUIManager : MonoBehaviour
{
    public Text vSavedText;      // Reference to the UI Text element for victims saved
    public Text vNotSavedText;   // Reference to the UI Text element for victims not saved
    public Text moralityText;     // Reference to the UI Text element for player morality
    public Text countdownText; 
    public Text healthText;
    public Text regenBox;



    public GameObject textBoxHolder;
    private void Start()
    {
        textBoxHolder = GameObject.Find("TextBoxHolder");

        // Initial update of the UI
        UpdateUI();
    }

    private void Update()
    {
        // Update the UI in every frame (if the values change during the game)
        UpdateUI();
        textBoxHolder.SetActive(countdownText.text.Length > 0);
    }

    private void UpdateUI()
    {
        // Update text fields with the current stats
        vSavedText.text = "" + GameStatController.Instance.victimsSaved;
        vNotSavedText.text = "" + GameStatController.Instance.victimsNotSaved;
        moralityText.text = "" + GameStatController.Instance.playersMoral;
        healthText.text = "" + GameStatController.Instance.health;
        regenBox.text  =  "" + GameStatController.Instance.playerRegen;
        
    }
}
