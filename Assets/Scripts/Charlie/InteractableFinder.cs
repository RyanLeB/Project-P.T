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

    public Material outlineMaterial;

    public float rayDistance = 5f;
    public float rayRadius = 0.5f;
    public float checkInterval = 0.2f;

    private Coroutine checkForInteractableCoroutine;

    private Renderer currentRenderer;
    private Material[] originalMaterials;

    private void OnEnable()
    {
        if (checkForInteractableCoroutine == null)
        {
            checkForInteractableCoroutine = StartCoroutine(CheckForInteractable());
        }
    }

    private void OnDisable()
    {
        if (checkForInteractableCoroutine != null)
        {
            StopCoroutine(checkForInteractableCoroutine);
            checkForInteractableCoroutine = null;
        }
    }

    private void OnDestroy()
    {
        if (checkForInteractableCoroutine != null)
        {
            StopCoroutine(checkForInteractableCoroutine);
        }
    }

    private IEnumerator CheckForInteractable()
    {
        while (true)
        {
            Camera mainCamera = Camera.main; // Dynamically fetch the camera
            if (mainCamera == null)
            {
                yield return null; // Wait for the next frame if the camera is not available
                continue;
            }

            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance) && hit.collider.CompareTag("Interactable"))
            {
                InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

                if (interactableObject != null && interactableObject.isInteractable)
                {
                    if (currentObject != interactableObject)
                    {
                        ClearAdditionalMaterial();
                        currentObject = interactableObject;
                        currentRenderer = currentObject.GetComponent<Renderer>();
                        originalMaterials = currentRenderer.materials;
                        SetAdditionalMaterial(outlineMaterial);
                    }

                    interactText.gameObject.SetActive(true);
                    interactText.text = GetInteractionText(currentObject);
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

    private string GetInteractionText(InteractableObject interactable)
    {
        switch (interactable.name)
        {
            case "FirstDoor":
                return "Press E to interact";
            case "FirstKey":
                return "Press E to pick up the key";
            case "FirstLockDoor":
                Door door = interactable.GetComponent<Door>();
                if (door.isLocked && door.inventory.HasKey(1))
                {
                    return "Press E to use the key";
                }
                else if (door.isLocked)
                {
                    return "Find the key to unlock the door";
                }
                else
                {
                    return "Press E to open the door";
                }
            default:
                return string.Empty;
        }
    }

    public void SetAdditionalMaterial(Material material)
    {
        if (additionalMaterialApplied || currentRenderer == null)
        {
            return;
        }
        Material[] materialsArray = new Material[originalMaterials.Length + 1];
        originalMaterials.CopyTo(materialsArray, 0);
        materialsArray[materialsArray.Length - 1] = material;
        currentRenderer.materials = materialsArray;
        additionalMaterialApplied = true;
    }

    public void ClearAdditionalMaterial()
    {
        if (!additionalMaterialApplied || currentRenderer == null)
        {
            return;
        }
        currentRenderer.materials = originalMaterials;
        additionalMaterialApplied = false;
    }
}
