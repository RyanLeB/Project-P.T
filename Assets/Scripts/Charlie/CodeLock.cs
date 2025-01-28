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
            ExitKeyPad();
        }
        else
        {
            Debug.Log("Wrong password");
            ClearInput();
        }
    }

    /// <summary>
    /// Closes the keypadUI.
    /// </summary>
    public void ExitKeyPad()
    {
        ClearInput();
        player.cameraLocked = false;
        gameObject.SetActive(false);
    }
}
