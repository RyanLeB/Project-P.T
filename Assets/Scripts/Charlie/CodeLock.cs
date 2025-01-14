using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CodeLock : MonoBehaviour
{
    public int passwordLength = 4;

    public string password;
    public string input;

    private PlayerController player;
    private Button clickedButton;

    public TextMeshProUGUI inputField;

    public Door doorToOpen;

    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            player.cameraLocked = true;
        }
    }

    /// <summary>
    /// Adds a digit to the input field depending on which button was clicked.
    /// </summary>
    public void AddDigit()
    {
        if (input.Length < passwordLength)
        {
            clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            input += clickedButton.GetComponentInChildren<TextMeshProUGUI>().text.Trim();
            inputField.text = input;
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
        input = "";
        inputField.text = input;
    }

    /// <summary>
    /// Checks if the input matches the password.
    /// </summary>
    public void CheckInput()
    {
        if (input == password)
        {
            Debug.Log("Unlocked");
            ClearInput();
            doorToOpen.isLocked = false;
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
