using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<int> keys = new List<int>();

    public void AddKey(int keyID)
    {
        keys.Add(keyID);
    }

    public bool HasKey(int keyID)
    {
        return keys.Contains(keyID);
    }
}
