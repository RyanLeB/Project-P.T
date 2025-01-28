using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Settings,
        KeyPad
    }

    public GameState currentGameState;
    public GameState previousGameState;

    public GameObject player;

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
            case GameState.KeyPad:
                Keypad();
                break;
        }
    }

    #region Game States
    void MainMenu()
    {
        Cursor.visible = true;
        uIManager.MainMenuUI();
        player.SetActive(false);
    }

    void GamePlay()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        uIManager.GamePlayUI();
        player.SetActive(true);
    }

    void GameOver()
    {
        Cursor.visible = true;
        uIManager.GameOverUI();
        player.SetActive(false);
    }

    void GamePause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        uIManager.GamePauseUI();
        player.SetActive(true);
    }

    void Settings()
    {
        Cursor.visible = true;
        uIManager.SettingsUI();
        player.SetActive(false);
    }

    void Keypad()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.SetActive(true);
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

    public void PauseGame()
    {
        if (currentGameState == GameState.GamePause)
        {
            ChangeToPreviousGameState();
        }
        else
        {
            ChangeGameState(GameState.GamePause);
        }
    }

    public void PlayGame()
    {
        ChangeGameState(GameState.GamePlay);
        SceneManager.LoadScene("Charlie");
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
