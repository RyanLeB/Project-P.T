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
    public bool isLocked = true;
    public int requiredKeyID;
    public Inventory inventory;

    public Animator doorAnimator; 
    public AudioSource doorOpenSound;
    
    /// <summary>
    /// Tries to open the door. If the door is locked, it will check if the player has the required key.
    /// </summary>
    public void TryOpen()
    {
        if (isLocked)
        {
            if (doorType == DoorType.Lock)
            {
                OpenLock();
            }
            else
            {
                Debug.Log("This door is locked");
            }
        }
        else if (!isLocked)
        {
            Debug.Log("Door opens");
            doorAnimator.Play("DoorOpen");
            doorOpenSound.Play();
        }
    }

    /// <summary>
    /// Manually unlocks a door.
    /// </summary>
    public void UnlockDoor()
    {
        isLocked = false;
    }

    /// <summary>
    /// Opens a locked door if the player has the required key.
    /// </summary>
    public void OpenLock()
    {
        if (inventory.HasKey(requiredKeyID))
        {
            isLocked = false;
            Debug.Log("Door is unlocked");
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
}
