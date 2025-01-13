using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public enum DoorType
    {
        Lock,
        Code
    }

    public DoorType doorType;

    public int requiredKeyID;
    public Inventory inventory;

    public void Open()
    {
        if (inventory.HasKey(requiredKeyID))
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("You need the key to open this door!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
