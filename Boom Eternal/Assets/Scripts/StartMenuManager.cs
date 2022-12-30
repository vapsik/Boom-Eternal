using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] GameObject startMenuPanel, settingsMenuPanel;
    public void StartGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    public void OpenSettings()
    {
        startMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }
    public void BackToStartMenu()
    {
        startMenuPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
    }

    public static void hoverSound()
    {
        GlobalReferences.audioManager.playSound("menuButtonHover");
    }

    public static void clickSound(Boolean startButton)
    {
        if (startButton)
            GlobalReferences.audioManager.playSound("menuButtonStart");
        else
            GlobalReferences.audioManager.playSound("menuButtonClick");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
