using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableFinder : MonoBehaviour
{
    [SerializeField] public InteractableObject currentObject;

    public TextMeshProUGUI interactText;

    private bool additionalMaterialApplied = false;

    public Material outlineMaterial;


    // THIS WILL CHANGE TO BE A GLOWING EFFECT SOON


    /// <summary>
    /// Checks if the player is looking at an interactable object.
    /// </summary>
    /// <param name="other">The object the player is looking at</param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentObject = other.GetComponent<InteractableObject>();

            if (currentObject != null)
            {
                if (currentObject.isInteractable)
                {
                    //interactText.gameObject.SetActive(true);
                    //interactText.text = "Press E to " + currentObject.interactionType + " " + currentObject.type;
                    SetAdditionalMaterial();
                    // its either this or on InteractableObject i put a public string for the text that will appear here
                    // so it would be something like interactText.text = currentObject.interactText;.
                }
                else
                {
                    ClearAdditionalMaterial();
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
        //interactText.gameObject.SetActive(false);
        ClearAdditionalMaterial();
        currentObject = null;
    }
    public void SetAdditionalMaterial()
    {
        if (additionalMaterialApplied)
        {
            //Debug.LogError("Tried to add additional material even though it was already added on " + name);
            return;
        }
        Material[] materialsArray = new Material[(currentObject.GetComponent<Renderer>().materials.Length + 1)]; // 2 length
        //Debug.Log(currentObject.GetComponent<Renderer>().materials.Length + 1); // 2
        Debug.Log("MaterialsArrayLength:" + materialsArray.Length); // 2
        currentObject.GetComponent<Renderer>().materials.CopyTo(materialsArray, 0);
        materialsArray[materialsArray.Length - 1] = outlineMaterial;
        currentObject.GetComponent<Renderer>().materials = materialsArray;
        additionalMaterialApplied = true;
    }

    public void ClearAdditionalMaterial()
    {
        if (!additionalMaterialApplied)
        {
            //Debug.LogError("Tried to delete additional material even though none was added before on " + name);
            return;
        }
        Material[] materialsArray = new Material[(currentObject.GetComponent<Renderer>().materials.Length - 1)];
        for (int i = 0; i < currentObject.GetComponent<Renderer>().materials.Length - 1; i++)
        {
            materialsArray[i] = currentObject.GetComponent<Renderer>().materials[i];
        }
        currentObject.GetComponent<Renderer>().materials = materialsArray;
        additionalMaterialApplied = false;
    }
}
