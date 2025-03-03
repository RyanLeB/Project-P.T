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
    public AudioSource doorCloseSound;
    public AudioSource doorLockSound;
    public AudioSource doorUnlockSound;
    
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
                doorLockSound.Play();
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
        doorUnlockSound.Play();
    }

    public void Open()
    {
        doorAnimator.Play("DoorOpen");
        doorOpenSound.Play();
    }

    public void Close()
    {
        doorAnimator.Play("DoorClose");
        if (doorCloseSound != null)
        {
            doorCloseSound.Play();
        }
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void Crack()
    {
        doorAnimator.Play("DoorCrack");
        doorOpenSound.Play();
    }

    /// <summary>
    /// Opens a locked door if the player has the required key.
    /// </summary>
    public void OpenLock()
    {
        if (inventory.HasKey(requiredKeyID))
        {
            isLocked = false;
            doorUnlockSound.Play();
            inventory.keyGameObject.SetActive(false);
            Debug.Log("Door is unlocked");
        }
        else
        {
            Debug.Log("You need the key to open this door!");
            doorLockSound.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }
}
