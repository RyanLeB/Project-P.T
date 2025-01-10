using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public InteractableFinder interactableFinder;

    // Start is called before the first frame update
    void Start()
    {
        interactableFinder = GetComponentInChildren<InteractableFinder>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // so when we interact with an object we want it to just tp to a spot in your "hotbar" if its a key.
    // the lighter can go in the other corner of the hotbar.
    // lighter fluid can just fill a bar or increase a number % style in the UI somewhere.

    public void OnInteract()
    {
        if (interactableFinder.currentObject != null)
        {
            if (interactableFinder.currentObject.type == InteractableObject.InteractableType.Key)
            {
                Debug.Log("Key");
            }
            else if (interactableFinder.currentObject.type == InteractableObject.InteractableType.Lighter)
            {
                Debug.Log("Lighter");
            }
            else if (interactableFinder.currentObject.type == InteractableObject.InteractableType.LighterFluid)
            {
                Debug.Log("Lighter Fluid");
            }
        }
        else
        {
            Debug.Log("Nothing here");
        }
    }

}
