using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;

public class Keypad : MonoBehaviour
{
    public Door doorToOpen;
    public Object padLock;

    public string password;

    public string input;

    public int passwordLength = defaultPasswordLength;

}
