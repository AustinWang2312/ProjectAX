using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public void LoadTutorialScene()
    {
        SoundManager.instance.PlaySound("UI Button");
        SceneManager.LoadScene("Tutorial Scene");
        
    }

    public void LoadFreePlayModeScene()
    {
        SoundManager.instance.PlaySound("UI Button");
        SceneManager.LoadScene("Freeplay Scene");
        
    }

    public void LoadMenuScene()
    {
        SoundManager.instance.PlaySound("UI Button");
        SceneManager.LoadScene("Menu Scene");
    }

    public void LoadDeathScene()
    {
        SoundManager.instance.PlaySound("UI Button");
        SceneManager.LoadScene("Death Scene");
    }

    public void QuitGame()
    {
        SoundManager.instance.PlaySound("UI Button");
        Application.Quit();
    }

    
}
