using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ButtonManager : MonoBehaviour
{
    public Button myButton;

    private void Start()
    {
        GameObject buttonObject = GameObject.Find("Quit Button"); // replace "My Button" with the name of your button GameObject
        if (buttonObject != null)
        {
            myButton = buttonObject.GetComponent<Button>();
            if (myButton != null)
            {
                myButton.onClick.AddListener(QuitGame);
                Debug.Log("found button");
            }
            else
            {
                Debug.LogError("No Button component found on " + buttonObject.name);
            }
        }
        else
        {
            Debug.LogError("No GameObject named 'Quit Button' found in scene.");
        }
    }



    public void QuitGame()
    {
        // Replace "MainMenuScene" with the name of your main menu scene
        Debug.Log("Button Pressed");
        SceneManager.LoadScene("Menu Scene");
    }
}
