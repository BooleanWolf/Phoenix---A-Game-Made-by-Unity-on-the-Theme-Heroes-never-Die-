using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HelpMenuController : MonoBehaviour
{
    public Image slideshowImage;

    public Sprite[] images;

    public float displayTime = 2f;
    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onGoNextButtonClick()
    {
        if (currentIndex == 0) {
            currentIndex = 1;   
            slideshowImage.sprite = images[1];
        }
        else
        {
            currentIndex = 0; 
            slideshowImage.sprite = images[0];
        }
       
    }

    public void onGoBackClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
