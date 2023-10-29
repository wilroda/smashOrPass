using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
    }

    public void BackCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
