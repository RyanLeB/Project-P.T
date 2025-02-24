using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<int> keys = new List<int>();
    [SerializeField] private List<KeyPiece> keyPieces = new List<KeyPiece>();

    private Lighter lighter;

    public GameObject keyGameObject;

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

    public void AddKeyPiece(KeyPiece keyPiece)
    {
        keyPieces.Add(keyPiece);
    }

    public List<KeyPiece> GetKeyPieces()
    {
        return keyPieces;
    }


    private void CheckKeyPieces()
    {
        var groupedKeyPieces = keyPieces.GroupBy(kp => kp.keyID)
                                        .Where(g => g.Count() == 2)
                                        .Select(g => g.Key)
                                        .ToList();

        foreach (var keyID in groupedKeyPieces)
        {
            AddKey(keyID);
            keyGameObject.SetActive(true);
            keyPieces.RemoveAll(kp => kp.keyID == keyID);
        }
    }

    private void Start()
    {
        lighter = FindObjectOfType<Lighter>();
        keyGameObject.SetActive(false);
    }

    private void Update()
    {
        lighter.CheckLighter();
        CheckKeyPieces();
    }
}
