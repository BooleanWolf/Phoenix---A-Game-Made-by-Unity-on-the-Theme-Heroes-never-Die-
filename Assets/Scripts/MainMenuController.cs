using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnGamePlayButtonClicked()
    {
        SceneManager.LoadScene("AlphaWorld"); // Replace with your actual gameplay scene name
    }

    public void OnStoryButtonClicked()
    {
        // Display story content (can load a new scene or open a UI panel)
        Debug.Log("Story Button Clicked");
        SceneManager.LoadScene("Story");
    }

    public void OnHelpButtonClicked()
    {
        // Display help content (can load a new scene or open a UI panel)
        Debug.Log("Help Button Clicked");
        SceneManager.LoadScene("Help");
    }

    public void OnCreditsButtonClicked()
    {
        // Display credits content (can load a new scene or open a UI panel)
        Debug.Log("Credits Button Clicked");
        SceneManager.LoadScene("Credits");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
