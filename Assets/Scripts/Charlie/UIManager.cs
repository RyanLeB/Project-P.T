using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject gamePlayUI;
    public GameObject gameOverUI;
    public GameObject gamePauseUI;
    public GameObject settingsUI;

    public void TurnOffUI()
    {
        mainMenuUI.SetActive(false);
        gamePlayUI.SetActive(false);
        gameOverUI.SetActive(false);
        gamePauseUI.SetActive(false);
        settingsUI.SetActive(false);
    }

    public void MainMenuUI()
    {
        TurnOffUI();
        mainMenuUI.SetActive(true);
    }

    public void GamePlayUI()
    {
        TurnOffUI();
        gamePlayUI.SetActive(true);
    }
    public void GameOverUI()
    {
        TurnOffUI();
        gameOverUI.SetActive(true);
    }

    public void GamePauseUI()
    {
        TurnOffUI();
        gamePauseUI.SetActive(true);
    }

    public void SettingsUI()
    {
        TurnOffUI();
        settingsUI.SetActive(true);
    }
}
