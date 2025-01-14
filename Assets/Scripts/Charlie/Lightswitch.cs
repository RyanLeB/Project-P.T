using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{

    public enum LightswitchType
    {
        LightswitchPuzzle,
        Trigger
    }

    public LightswitchType lightswitchType;

    bool lightOn;
    public GameObject[] lightObjects;
    public int lastStatesToRemember = 5;
    public List<bool> lastStates = new List<bool>();
    public List<bool> requiredLastStates = new List<bool>();


    public bool triggerActivated = false;

    public bool stateToActivate = true; // this will be the state the lightswitch will have to be for something else to happen

    public void ToggleSwitch()
    {
        lightOn = !lightOn;
        if (lastStates.Count >= lastStatesToRemember)
        {
            lastStates.RemoveAt(0);
        }
        lastStates.Add(lightOn);
    }

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

    public void CheckLightswitchPuzzle()
    {
        if (lastStates.SequenceEqual(requiredLastStates) && !triggerActivated)
        {
            ThingHappens();
            triggerActivated = true;
        }
    }

    public void CheckTrigger()
    {
        if (lightOn == stateToActivate && !triggerActivated)
        {
            ThingHappens();
            triggerActivated = true;
        }
    }
}
