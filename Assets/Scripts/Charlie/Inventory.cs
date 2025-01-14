using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<int> keys = new List<int>();
    private Lighter lighter;

    /// <summary>
    /// Adds a key to the player's inventory.
    /// </summary>
    /// <param name="keyID">The id number of the key to add</param>
    public void AddKey(int keyID)
    {
        keys.Add(keyID);
    }

    /// <summary>
    /// Checks if the player has a specified key in their inventory.
    /// </summary>
    /// <param name="keyID">The id number of the key to check</param>
    /// <returns>true if the player has the specified key</returns>
    public bool HasKey(int keyID)
    {
        return keys.Contains(keyID);
    }

    /// <summary>
    /// Gets the list of keys in the player's inventory.
    /// </summary>
    /// <returns>The list of keys</returns>
    public List<int> GetKeys()
    {
        return keys;
    }

    private void Start()
    {
        lighter = FindObjectOfType<Lighter>();
    }

    private void Update()
    {
        lighter.CheckLighter();
    }
}
