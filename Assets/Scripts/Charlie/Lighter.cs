using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    public float maxIntensity = 1.1f;

    public float lighterFluid = 100f; // 100% full

    public float lighterFluidDecreaseRate = 0.5f;

    public bool hasLighter = false;

    [SerializeField] private Light lighterLight;

    public void Start()
    {
        lighterLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIntensity();
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
        if (lighterFluid >= 110)
        {
            lighterLight.intensity = maxIntensity;
        }
        else
        {
            lighterLight.intensity = lighterFluid / 100;
        }
    }
}
