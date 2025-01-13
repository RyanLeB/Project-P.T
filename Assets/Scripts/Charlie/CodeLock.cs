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

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddDigit()
    {
        clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        input += clickedButton.GetComponentInChildren<TextMeshProUGUI>().text.Trim();
        inputField.text = input;
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
        }

    }
}
