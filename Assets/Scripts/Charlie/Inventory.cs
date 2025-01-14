using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<int> keys = new List<int>();
    private Lighter lighter;

    public void AddKey(int keyID)
    {
        keys.Add(keyID);
    }

    public bool HasKey(int keyID)
    {
        return keys.Contains(keyID);
    }

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
