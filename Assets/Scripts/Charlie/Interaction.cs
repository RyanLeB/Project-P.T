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

    // Start is called before the first frame update
    void Start()
    {
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

    public void OnInteract()
    {
        if (currentObject != null)
        {
            if (currentObject.type == InteractableObject.InteractableType.Key)
            {
                Debug.Log("Key");
                Key key = currentObject.GetComponent<Key>();
                inventory.keys.Add(key.keyID);
                currentObject.gameObject.SetActive(false);
                // tp key to spot in hotbar
                // do something to let game know player has key
                // if interacting with door with correct key in hotbar, door unlocks.
            }
            else if (currentObject.type == InteractableObject.InteractableType.Lighter)
            {
                Debug.Log("Lighter");
                lighter.lighterFluid += 100;
                currentObject.gameObject.SetActive(false);
            }
            else if (currentObject.type == InteractableObject.InteractableType.LighterFluid)
            {
                Debug.Log("Lighter Fluid");
                lighter.lighterFluid += 50;
                currentObject.gameObject.SetActive(false);
            }
            else if (currentObject.type == InteractableObject.InteractableType.Door)
            {
                Debug.Log("Door");
                Door door = currentObject.GetComponent<Door>();
                door.Open();
            }
        }
        else
        {
            Debug.Log("Nothing here");
        }
    }

}
