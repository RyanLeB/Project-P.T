using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    bool lightOn;
    public GameObject[] lightObjects;

    public void ToggleSwitch()
    {
        lightOn = !lightOn;
    }

    void Update()
    {
        foreach (GameObject lightObject in lightObjects)
        {
            lightObject.SetActive(lightOn);
        }
    }
}
