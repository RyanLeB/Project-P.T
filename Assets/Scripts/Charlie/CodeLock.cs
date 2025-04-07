using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CodeLock : MonoBehaviour
{
    private PlayerController player;
    private Button clickedButton;

    public GameManager gameManager;

    public TextMeshProUGUI inputField;

    public Keypad currentKeypad;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }
    
    

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            player.cameraLocked = true;
        }
    }

    /// <summary>
    /// Adds a digit to the input field depending on which button was clicked.
    /// </summary>
    public void AddDigit()
    {
        if (currentKeypad.input.Length < currentKeypad.passwordLength)
        {
            clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            currentKeypad.input += clickedButton.GetComponentInChildren<TextMeshProUGUI>().text.Trim();
            inputField.text = currentKeypad.input;
            if (currentKeypad.GetComponent<AudioSource>() == null)
            {
                currentKeypad.keypadAudio = currentKeypad.gameObject.AddComponent<AudioSource>();
            }
            currentKeypad.keypadAudio.clip = currentKeypad.keypadEnterAudio;
            currentKeypad.keypadAudio.Play();
        }
        else
        {
            Debug.Log("Password cannot be longer than 4 digits");
        }
    }

    /// <summary>
    /// Clears the input field.
    /// </summary>
    public void ClearInput()
    {
        currentKeypad.input = "";
        inputField.text = currentKeypad.input;
    }

    /// <summary>
    /// Checks if the input matches the password.
    /// </summary>
    public void CheckInput()
    {
        if (currentKeypad.input == currentKeypad.password)
        {
            Debug.Log("Unlocked");
            ClearInput();
            currentKeypad.doorToOpen.isLocked = false;
            currentKeypad.keypadAudio.clip = currentKeypad.keypadSuccessAudio;
            currentKeypad.keypadAudio.Play();
            
            ExitKeyPad();
            if (currentKeypad.padLock != null)
            {
                Destroy(currentKeypad.padLock);
            }
        }
        else
        {
            Debug.Log("Wrong password");
            currentKeypad.keypadAudio.clip = currentKeypad.keypadErrorAudio;
            currentKeypad.keypadAudio.Play();
            ClearInput();
        }
    }

    /// <summary>
    /// Closes the keypadUI.
    /// </summary>
    public void ExitKeyPad()
    {
        ClearInput();
        gameManager.ChangeGameState(GameManager.GameState.GamePlay);
        currentKeypad.GetComponent<InteractableObject>().isInteractable = true;
        player.cameraLocked = false;
        gameObject.SetActive(false);
    }
}
