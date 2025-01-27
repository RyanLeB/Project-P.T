using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public InteractableFinder interactableFinder;
    public InteractableObject currentObject;
    public Inventory inventory;
    public Lighter lighter;
    public GameObject keypadUI;
    public CodeLock codeLock;

    // Start is called before the first frame update
    void Start()
    {
        codeLock = FindObjectOfType<CodeLock>();
        keypadUI.SetActive(false);
        interactableFinder = GetComponentInChildren<InteractableFinder>();
        inventory = FindObjectOfType<Inventory>();
        lighter = FindObjectOfType<Lighter>();
    }

    // Update is called once per frame
    void Update()
    {
        currentObject = interactableFinder.currentObject;
    }
    // when the player picks up the key, we want it to go into the "hotbar"
    // and if they go up to the correct door with the key in the "hotbar" the door opens.

    // so when we interact with an object we want it to just tp to a spot in your "hotbar" if its a key.
    // the lighter can go in the other corner of the hotbar.
    // lighter fluid can just fill a bar or increase a number % style in the UI somewhere.

    /// <summary>
    /// Gets called when the player interacts with an object by pressing the interact key [e] while looking at it.
    /// </summary>
    public void OnInteract()
    {
        if (currentObject != null)
        {
            currentObject.Interact();
        }
        else
        {
            Debug.Log("Nothing here");
        }
    }
}
