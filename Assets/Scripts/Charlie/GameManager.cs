using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GlobalVariables;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public UIManager uIManager;

    bool keyPressed = false;

    public enum GameState
    {
        MainMenu,
        GamePlay,
        GameOver,
        GamePause,
        Settings,
        KeyPad,
        Credits,
        Intro
    }

    public GameState currentGameState;
    public GameState previousGameState;

    public GameObject player;
    public PlayerController playerController;

    public Inventory playerInventory;

    public int currentLevel = 0;

    void OnDestroy()
    {
        if (manager == this)
        {
            manager = null;
        }
    }

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
        //playerController = FindObjectOfType<PlayerController>();
        //playerInventory = FindObjectOfType<Inventory>();
    }

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    //public static void EntryPoint()
    //{
    //    if (manager == null)
    //    {
    //        SceneManager.LoadScene(0);
    //    }
    //}

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
            case GameState.Credits:
                Credits();
                break;
            case GameState.Intro:
                Intro();
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
        if (playerController != null)
        {
            playerController.cameraLocked = false;
            playerController.movementLocked = false;
        }
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

    void Credits()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.SetActive(false);
        uIManager.CreditsUI();
    }

    void Intro()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.SetActive(false);
        uIManager.IntroUI();

        if (Input.anyKey && !keyPressed)
        {
            keyPressed = true;
            ChangeGameState(GameState.MainMenu);
        }
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
        SceneManager.LoadScene(firstLevelSceneName);
    }

    public void OpenSettings()
    {
        ChangeGameState(GameState.Settings);
    }

    public void OpenMainMenu()
    {
        ChangeGameState(GameState.MainMenu);
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OpenCredits()
    {
        ChangeGameState(GameState.Credits);
    }

    public void OpenIntro()
    {
        ChangeGameState(GameState.Intro);
        keyPressed = false;
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

    public void ResetValues()
    {
        currentLevel = 0;
        playerInventory.GetKeys().Clear();
    }
}
