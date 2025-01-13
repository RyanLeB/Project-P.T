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

    private Button clickedButton;
    
    public TextMeshProUGUI inputField;

    public Door doorToOpen;

    // Start is called before the first frame update
    void Start()
    {

    }

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

    public void ClearInput()
    {
        input = "";
        inputField.text = input;
    }

    public void CheckInput()
    {
        if (input == password)
        {
            Debug.Log("Unlocked");
            ClearInput();
            doorToOpen.isLocked = false;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Wrong password");
            ClearInput();
        }
    }
}
