using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFinder : MonoBehaviour
{
    [SerializeField] public InteractableObject currentObject;


    /// <summary>
    /// Checks if the player is looking at an interactable object.
    /// </summary>
    /// <param name="other">The object the player is looking at</param>
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentObject = other.GetComponent<InteractableObject>();
        }
    }

    /// <summary>
    /// Clears the current object when the player is no longer looking at it.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        currentObject = null;
    }
}
