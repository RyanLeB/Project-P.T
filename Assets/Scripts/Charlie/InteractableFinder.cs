using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableFinder : MonoBehaviour
{
    [SerializeField] public InteractableObject currentObject;

    public TextMeshProUGUI interactText;

    /// <summary>
    /// Checks if the player is looking at an interactable object.
    /// </summary>
    /// <param name="other">The object the player is looking at</param>
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactText.gameObject.SetActive(true);
            currentObject = other.GetComponent<InteractableObject>();

            // This is the button prompt we can change this more if we want to.
            interactText.text = "Press E to " + currentObject.interactionType + " " + currentObject.type;
        }
    }

    /// <summary>
    /// Clears the current object when the player is no longer looking at it.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        interactText.gameObject.SetActive(false);
        currentObject = null;
    }
}
