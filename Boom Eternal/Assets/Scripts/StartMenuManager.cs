using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void BackToStartMenu(){
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
