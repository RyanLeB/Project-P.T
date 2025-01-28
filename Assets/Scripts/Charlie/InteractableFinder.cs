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
            currentObject = other.GetComponent<InteractableObject>();

            if (currentObject != null)
            {
                if (currentObject.isInteractable)
                {
                    interactText.gameObject.SetActive(true);
                    interactText.text = "Press E to " + currentObject.interactionType + " " + currentObject.type;
                    // its either this or on InteractableObject i put a public string for the text that will appear here
                    // so it would be something like interactText.text = currentObject.interactText;.
                }
                else
                {
                    interactText.gameObject.SetActive(false);
                }
            }
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
