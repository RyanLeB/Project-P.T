using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GlobalVariables;

public class Lightswitch : MonoBehaviour
{

    /// <summary>
    /// The type of lightswitch.
    /// </summary>
    public enum LightswitchType
    {
        LightswitchPuzzle,
        Trigger
    }

    public LightswitchType lightswitchType;

    bool lightOn;
    public GameObject[] lightObjects;
    public int lastStatesToRemember = defaultStatesToRemember;
    public List<bool> lastStates = new List<bool>();
    public List<bool> requiredLastStates = new List<bool>();


    public bool triggerActivated = false;

    public bool stateToActivate = true; // this will be the state the lightswitch will have to be for something else to happen

    /// <summary>
    /// Toggles the lightswitch on or off.
    /// </summary>
    public void ToggleSwitch()
    {
        lightOn = !lightOn;
        if (lastStates.Count >= lastStatesToRemember)
        {
            lastStates.RemoveAt(0);
        }
        lastStates.Add(lightOn);
    }

    /// <summary>
    /// Something happens when the lightswitch is activated.
    /// </summary>
    public void ThingHappens()
    {
        Debug.Log("Something happens");
    }

    private void Start()
    {
        lastStates.Add(lightOn);
    }

    void Update()
    {
        foreach (GameObject lightObject in lightObjects)
        {
            lightObject.SetActive(lightOn);
            // lightObject.GetComponent<Light>().color = new Color(-1f, -1f, -1f); if this is on the light will remove light from the area. could be used for something cool?
        }

        switch (lightswitchType)
        {
            case LightswitchType.LightswitchPuzzle:
                CheckLightswitchPuzzle();
                break;
            case LightswitchType.Trigger:
                CheckTrigger();
                break;
        }
    }

    /// <summary>
    /// Checks if the lightswitch puzzle has been solved.
    /// </summary>
    public void CheckLightswitchPuzzle()
    {
        if (lastStates.SequenceEqual(requiredLastStates) && !triggerActivated)
        {
            ThingHappens();
            triggerActivated = true;
        }
    }

    /// <summary>
    /// Checks if the lightswitch has been activated.
    /// </summary>
    public void CheckTrigger()
    {
        if (lightOn == stateToActivate && !triggerActivated)
        {
            ThingHappens();
            triggerActivated = true;
        }
    }
}
