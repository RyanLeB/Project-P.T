using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    private string[] cheatCode;
    private int index;

    public bool cheatCodeEntered = false;

    public Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();

        // Code is "idkfa", user needs to input this in the right order
        cheatCode = new string[] { "q", "w", "e", "r", "t" };
        index = 0;
    }

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Check if the next key in the code is pressed
            if (Input.GetKeyDown(cheatCode[index]))
            {
                // Add 1 to index to check the next key in the code
                index++;
    
        }
            // Wrong key entered, we reset code typing
            else
            {
                index = 0;
            }
        }

        // If index reaches the length of the cheatCode string, 
        // the entire code was correctly entered
        if (index == cheatCode.Length && cheatCodeEntered == false)
        {
            Debug.Log("Cheat code entered correctly!");
            cheatCodeEntered = true;


            for (int i = 1; i <= 100; i++)
            {
                inventory.AddKey(i);
            }
        }
    }
}
