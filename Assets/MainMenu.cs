using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadTutorialScene()
    {
        // Replace "TutorialScene" with the name of your tutorial scene
        SceneManager.LoadScene("Tutorial Scene");
    }

    public void LoadFreePlayModeScene()
    {
        // Replace "FreePlayScene" with the name of your free play mode scene
        SceneManager.LoadScene("Freeplay Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
