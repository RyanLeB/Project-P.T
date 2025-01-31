using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GlobalVariables;

public class Lighter : MonoBehaviour
{
    public float currentLighterFluid;

    public bool hasLighter = false;

    // for testing purposes:
    public TextMeshProUGUI lighterFluidText;
    public TextMeshProUGUI lighterIntensityText;

    [SerializeField] private Light lighterLight;

    public void Start()
    {
        currentLighterFluid = startingLighterFluid;
        lighterLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIntensity();

        //DEBUG
        if (lighterIntensityText != null)
        {
            lighterIntensityText.text = "Lighter Intensity: " + lighterLight.intensity;
        }
        if (lighterFluidText != null)
        {
            lighterFluidText.text = "Lighter Fluid: " + currentLighterFluid;
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
                currentLighterFluid -= lighterFluidDecreaseRate * Time.deltaTime;
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
        // These magic numbers are all % calcs(short for calculations btw) so I will not change them.

        if (currentLighterFluid >= maxIntensity * 100)
        {
            lighterLight.intensity = maxIntensity;
        }
        else if (currentLighterFluid <= minIntensity * 100)
        {
            lighterLight.intensity = minIntensity;
        }
        else
        {
            lighterLight.intensity = currentLighterFluid / 100;
        }
    }

    /// <summary>
    /// Adds lighter fluid to the lighter. If the lighter fluid is already full, it will not add any more.
    /// </summary>
    /// <param name="lf">Amount of lighter fluid to add</param>
    public void AddLighterFluid(float lf)
    {
        if (currentLighterFluid + lf > maxLighterFluid)
        {
            currentLighterFluid = maxLighterFluid;
        }
        else
        {
            currentLighterFluid += lf;
        }
    }
}
