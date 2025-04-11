using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using static GlobalVariables;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public UIManager uIManager;
    public AudioManager audioManager;

    public SaveManager saveManager;

    public PlayableDirector director;

    bool keyPressed = false;
    bool gameStateChanged = false;

    public enum GameState
    {
        MainMenu,
        GamePlay,
        GameOver,
        GamePause,
        Settings,
        KeyPad,
        Credits,
        Intro,
        Controls,
        Options
    }

    public GameState currentGameState;
    public GameState previousGameState;

    public GameObject player;
    public PlayerController playerController;

    public Inventory playerInventory;

    public Vector3 lastPlayerPosition;

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
        saveManager = FindObjectOfType<SaveManager>();
        director.gameObject.SetActive(true);
        uIManager = FindObjectOfType<UIManager>();
        audioManager.PlayMusic("MainMenu");
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
        
        if (Input.anyKeyDown) 
        {
            HandleGameState(); 
        }

        if (director != null)
        {
            if (director.state == PlayState.Playing)
            {
                playerController.cameraLocked = true;
                playerController.movementLocked = true;

                if (Input.anyKeyDown)
                {
                    playerController.cameraLocked = false;
                    playerController.movementLocked = false;
                    director.Stop();
                    director.gameObject.SetActive(false);
                }
            }
        }
        
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
            case GameState.Controls:
                Controls();
                break;
            case GameState.Options:
                Options();
                break;
        }
    }

    #region Game States
    void MainMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
            CharacterController characterController = player.GetComponent<CharacterController>();
            characterController.enabled = true;
        }
        
        uIManager.GamePlayUI();
        player.SetActive(true);
    }

    void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        Cursor.lockState = CursorLockMode.None;
        uIManager.SettingsUI();
        Debug.Log(GameState.Settings);
        Debug.Log(previousGameState);
        if (previousGameState == GameState.GamePause)
        {
            player.SetActive(true);
        }
        else
        {
            player.SetActive(false);
        }
    }
    
    void Options()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        uIManager.OptionsUI();
        player.SetActive(false);
    }

    void Controls()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        uIManager.ControlsUI();
        player.SetActive(false);
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

        if (Input.anyKeyDown && !keyPressed)
        {
            keyPressed = true;
            saveManager.Load();
            ChangeGameState(GameState.MainMenu);
            
        }
    }
    #endregion

    public void ChangeGameState(GameState newGameState)
    {
        previousGameState = currentGameState;
        currentGameState = newGameState;
        
        HandleGameState();
    }

    public void ChangeToPreviousGameState()
    {
        GameState temp = currentGameState;
        currentGameState = previousGameState;
        previousGameState = temp;
        
        HandleGameState();
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

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.LoadScene(firstLevelSceneName);
        //ResetValues();

        if (director != null)
        {
            director.Play();
        }
    }

    // Callback for when the scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Unsubscribe from the event to avoid duplicate calls
        SceneManager.sceneLoaded -= OnSceneLoaded;
        StartCoroutine(SetPlayerPositionAfterDelay());
    }

    private IEnumerator SetPlayerPositionAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Wait for a short duration to ensure all initialization is complete
        if (playerController != null)
        {
            playerController.characterController.enabled = false;
            playerController.transform.position = lastPlayerPosition;
            playerController.characterController.enabled = true;
            Debug.Log("Player moved to last saved position: " + lastPlayerPosition);

            Key[] keysInScene = FindObjectsOfType<Key>();
            foreach (Key keyObject in keysInScene)
            {
                if (playerInventory.GetKeys().Contains(keyObject.keyID))
                {
                    keyObject.gameObject.SetActive(false); // Disable the key object
                }
            }

        }
        else
        {
            Debug.LogError("PlayerController is null. Cannot set position.");
        }
    }

    public void OpenSettings()
    {
        ChangeGameState(GameState.Settings);
    }

    public void OpenControls()
    {
        ChangeGameState(GameState.Controls);
    }

    public void OpenMainMenu()
    {
        ChangeGameState(GameState.MainMenu);
        lastPlayerPosition = playerController.transform.position;
        director.gameObject.SetActive(true);
        SceneManager.LoadScene(mainMenuSceneName);
        
    }
    
    public void OpenOptions()
    {
        ChangeGameState(GameState.Options);
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
        //PlayerController playerController = FindObjectOfType<PlayerController>();
        //playerController.transform.position = new Vector3(0,-100,0); // Has to find the player
        //playerController.currentSpeed = 0;
        currentLevel = 0;
        playerInventory.GetKeys().Clear();
        lastPlayerPosition = new Vector3(0, 1.35f, 0);
    }

    
    
}
