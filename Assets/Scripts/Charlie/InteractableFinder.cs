using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableFinder : MonoBehaviour
{
    [SerializeField] public InteractableObject currentObject;

    //public TextMeshProUGUI interactText;

    private bool additionalMaterialApplied = false;

    public Material outlineMaterial;

    public float raycastDistance = 5f; // Adjust the distance as needed
    public int raycastClusterSize = 3; // Number of rays in each direction (total rays = (2 * raycastClusterSize + 1)^2)
    public float raycastClusterSpacing = 0.1f; // Spacing between rays in the cluster

    private void Update()
    {
        PerformClusterRaycast();
    }

    /// <summary>
    /// Performs a cluster of raycasts to check if the player is looking at an interactable object.
    /// </summary>
    private void PerformClusterRaycast()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        bool foundInteractable = false;

        for (int x = -raycastClusterSize; x <= raycastClusterSize; x++)
        {
            for (int y = -raycastClusterSize; y <= raycastClusterSize; y++)
            {
                Vector3 offset = new Vector3(x * raycastClusterSpacing, y * raycastClusterSpacing, 0);
                Ray ray = new Ray(origin + offset, direction);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, raycastDistance))
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
                            foundInteractable = true;
                            break;
                        }
                    }
                }
            }
            if (foundInteractable)
            {
                break;
            }
        }

        if (!foundInteractable)
        {
            ClearAdditionalMaterial();
            currentObject = null;
        }
    }

    /// <summary>
    /// Adds a material onto the current interactable
    /// </summary>
    /// <param name="material">The material to be added</param>
    public void SetAdditionalMaterial(Material material)
    {
        if (additionalMaterialApplied || currentObject == null)
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
        if (!additionalMaterialApplied || currentObject == null)
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