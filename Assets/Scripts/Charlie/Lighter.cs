using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    public float maxIntensity = 1.1f;
    public float minIntensity = 0.1f;

    public float lighterFluid = 100f; // 100% full

    public float lighterFluidDecreaseRate = 0.5f;

    public bool hasLighter = false;

    // for testing purposes:
    public TextMeshProUGUI lighterFluidText;
    public TextMeshProUGUI lighterIntensityText;

    [SerializeField] private Light lighterLight;

    public void Start()
    {
        lighterLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIntensity();

        if (lighterIntensityText != null)
        {
            lighterIntensityText.text = "Lighter Intensity: " + lighterLight.intensity;
        }
        if (lighterFluidText != null)
        {
            lighterFluidText.text = "Lighter Fluid: " + lighterFluid;
        }
    }

    /// <summary>
    /// Checks if the player has the lighter in their inventory.
    /// </summary>
    public void CheckLighter()
    {
        switch (hasLighter)
        {
            case true:
                gameObject.SetActive(true);
                lighterFluid -= lighterFluidDecreaseRate * Time.deltaTime;
                break;
            case false:
                gameObject.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// Updates the intensity of the lighter's light based on the amount of lighter fluid remaining.
    /// </summary>
    public void UpdateIntensity()
    {
        if (lighterFluid >= maxIntensity * 100)
        {
            lighterLight.intensity = maxIntensity;
        }
        else if (lighterFluid <= minIntensity * 100)
        {
            lighterLight.intensity = minIntensity;
        }
        else
        {
            lighterLight.intensity = lighterFluid / 100;
        }
    }
}
