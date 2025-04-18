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

    public bool isOpen = false;

    public DoorType doorType;
    public bool isLocked = true;
    public GameObject lockPad;
    public int requiredKeyID;
    public Inventory inventory;

    public Animator doorAnimator;
    public AudioSource doorOpenSound;
    public AudioSource doorCloseSound;
    public AudioSource doorLockSound;
    public AudioSource doorUnlockSound;

    public ActionSubtitles actionSubtitles;

    private InteractableObject interactableObject;
    
    private bool isInteracting = false; // Indicates if the player is currently interacting with the door

    private void Awake()
    {
        actionSubtitles = FindObjectOfType<ActionSubtitles>();
        interactableObject = GetComponent<InteractableObject>();
    }

    /// <summary>
    /// Tries to open the door. If the door is locked, it will check if the player has the required key.
    /// </summary>
    public void TryOpen()
    {
        if (isInteracting) return;
        //isInteracting = true;
        
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
            isOpen = true;
            StartCoroutine(actionSubtitles.ShowSubtitle("Door opened"));
            StartCoroutine(PlayAnimation("DoorOpen", doorOpenSound));
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
        StartCoroutine(PlayAnimation("DoorOpen", doorOpenSound));
        isOpen = true;
    }

    public void Close()
    {
        StartCoroutine(PlayAnimation("DoorClose", doorCloseSound));
        StartCoroutine(actionSubtitles.ShowSubtitle("Door closed"));
        isOpen = false;
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void Crack()
    {
        StartCoroutine(PlayAnimation("DoorCrack", doorOpenSound));
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
            lockPad.SetActive(false);
            StartCoroutine(actionSubtitles.ShowSubtitle("Door unlocked"));
            isInteracting = false;
        }
        else
        {
            StartCoroutine(actionSubtitles.ShowSubtitle("Door locked. Find the correct key to open the door"));
            doorLockSound.Play();
        }
    }

    private IEnumerator PlayAnimation(string animationName, AudioSource sound = null)
    {
        isInteracting = true;
        interactableObject.isInteractable = false;
        doorAnimator.Play(animationName);
        if (sound != null)
        {
            sound.Play();
        }
        
        yield return new WaitUntil(() => doorAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName));

        // Wait until the animation is done
        yield return new WaitForSeconds(doorAnimator.GetCurrentAnimatorStateInfo(0).length);

        interactableObject.isInteractable = true;
        isInteracting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }
}
