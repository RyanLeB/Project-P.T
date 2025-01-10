using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFinder : MonoBehaviour
{
    [SerializeField] public InteractableObject currentObject;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentObject = other.GetComponent<InteractableObject>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        currentObject = null;
    }
}
