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
    public PlayerController playerController;

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
        playerController.movementLocked = false;
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
        playerController.movementLocked = true;
        playerController.cameraLocked = true;
        uIManager.GamePauseUI();
        player.SetActive(true);
    }

    void Settings()
    {
        Cursor.visible = true;
        uIManager.SettingsUI();
        if (previousGameState == GameState.GamePause)
        {
            player.SetActive(true);
        }
        else
        {
            player.SetActive(false);
        }
    }

    void Keypad()
    {
        Cursor.visible = true;
        // LINK KEYPAD UI TO THE GAME MANAGER INSTEAD OF SETTING IT ACTIVE MANUALLY, this will make able to unpause the game correctly.
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
            if (previousGameState == GameState.KeyPad)
            {
                ChangeGameState(GameState.GamePlay);
                playerController.cameraLocked = false;
            }
            else
            {
                ChangeToPreviousGameState();
                playerController.cameraLocked = false;
            }
        }
        else
        {
            ChangeGameState(GameState.GamePause);
        }
    }

    public void PlayGame()
    {
        ChangeGameState(GameState.GamePlay);
        playerController.cameraLocked = false;
        SceneManager.LoadScene("Charlie");
    }

    public void OpenSettings()
    {
        ChangeGameState(GameState.Settings);
    }

    public void OpenMainMenu()
    {
        ChangeGameState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        ChangeGameState(GameState.GamePlay);
        playerController.cameraLocked = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
