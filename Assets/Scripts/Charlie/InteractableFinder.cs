using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableFinder : MonoBehaviour
{
    [SerializeField] public InteractableObject currentObject;

    public TextMeshProUGUI interactText;

    private bool additionalMaterialApplied = false;

    //public InteractableObject firstInteractable;

    public Material outlineMaterial;

    // Raycast to check if player is looking at an interactable object
    public float rayDistance = 5f;
    public float rayRadius = 0.5f;
    public float checkInterval = 0.2f;
    
    // THIS WILL CHANGE TO BE A GLOWING EFFECT SOON

    private void FixedUpdate()
    {
        StartCoroutine(CheckForInteractable());
    }

    /// <summary>
    /// Checks if the player is looking at an interactable object.
    /// </summary>
    /// <param name="other">The object the player is looking at</param>
    private IEnumerator CheckForInteractable()
    {
        while (true)
        {
            if (Camera.main != null)
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hit;

                Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

                if (Physics.Raycast(ray, out hit, rayDistance))
                {
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

                        if (interactableObject != null && interactableObject.isInteractable)
                        {
                            if (currentObject != interactableObject)
                            {
                                ClearAdditionalMaterial();
                                currentObject = interactableObject;
                                SetAdditionalMaterial(outlineMaterial);
                            }

                            if (currentObject.name == "FirstDoor")
                            {
                                interactText.gameObject.SetActive(true);
                                interactText.text = "Press E to interact";
                            }

                            if (currentObject.name == "FirstKey")
                            {
                                interactText.gameObject.SetActive(true);
                                interactText.text = "Press E to pick up the key";
                            }

                            if (currentObject.name == "FirstLockDoor") // Shows the player how to use keys
                            {
                                Door door = currentObject.GetComponent<Door>();
                                if (door.isLocked && door.inventory.HasKey(1))
                                {
                                    interactText.gameObject.SetActive(true);
                                    interactText.text = "Press E to use the key";
                                    Debug.Log(
                                        $"Checking for key ID 1: {door.inventory.HasKey(1)}"); // To see if the door is using key ID 1
                                }
                                else if (door.isLocked) // This happens for every locked door
                                {
                                    interactText.gameObject.SetActive(true);
                                    interactText.text = "Find the key to unlock the door";
                                }
                                else
                                {
                                    interactText.gameObject.SetActive(true);
                                    interactText.text = "Press E to open the door";
                                }
                            }
                        }
                        else
                        {
                            ClearAdditionalMaterial();
                            interactText.gameObject.SetActive(false);
                            currentObject = null;
                        }
                    }
                    else
                    {
                        ClearAdditionalMaterial();
                        interactText.gameObject.SetActive(false);
                        currentObject = null;
                    }
                }
                else
                {
                    ClearAdditionalMaterial();
                    interactText.gameObject.SetActive(false);
                    currentObject = null;
                }

                yield return new WaitForSeconds(checkInterval);
            }
        }
    }

    /// <summary>
    /// Adds a material onto the current interactable
    /// </summary>
    /// <param name="material">The material to be added</param>
    public void SetAdditionalMaterial(Material material)
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
        materialsArray[materialsArray.Length - 1] = material;
        currentObject.GetComponent<Renderer>().materials = materialsArray;
        additionalMaterialApplied = true;
    }

    /// <summary>
    /// Removes added material to the current interactable
    /// </summary>
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
