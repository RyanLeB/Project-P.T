using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public UIManager uIManager;

    public enum GameState
    {
        MainMenu,
        GamePlay,
        GameOver,
        GamePause,
        Settings
    }

    public GameState currentGameState;
    public GameState previousGameState;


    void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleGameState();
    }

    void HandleGameState()
    {
        switch (currentGameState)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.GamePlay:
                GamePlay();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GamePause:
                GamePause();
                break;
            case GameState.Settings:
                Settings();
                break;
        }
    }

    #region Game States
    void MainMenu()
    {
        Cursor.visible = true;
        uIManager.MainMenuUI();
    }

    void GamePlay()
    {
        Cursor.visible = false;
        uIManager.GamePlayUI();
    }

    void GameOver()
    {
        Cursor.visible = true;
        uIManager.GameOverUI();
    }

    void GamePause()
    {
        Cursor.visible = true;
        uIManager.GamePauseUI();
    }

    void Settings()
    {
        Cursor.visible = true;
        uIManager.SettingsUI();
    }
    #endregion

    public void ChangeGameState(GameState newGameState)
    {
        previousGameState = currentGameState;
        currentGameState = newGameState;
    }

    public void ChangeToPreviousGameState()
    {
        GameState temp = currentGameState;
        currentGameState = previousGameState;
        previousGameState = temp;
    }

    public void PlayGame()
    {
        ChangeGameState(GameState.GamePlay);
    }

    public void OpenSettings()
    {
        ChangeGameState(GameState.Settings);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
