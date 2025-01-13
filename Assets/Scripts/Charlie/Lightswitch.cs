using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    bool lightOn;
    public GameObject[] lightObjects;
    public int lastStatesToRemember = 5;
    public List<bool> lastStates = new List<bool>();

    public void ToggleSwitch()
    {
        lightOn = !lightOn;
        if (lastStates.Count >= lastStatesToRemember)
        {
            lastStates.RemoveAt(0);
        }
        lastStates.Add(lightOn);
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
    }
}
