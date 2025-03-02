using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLightsTurningOff : MonoBehaviour
{
    public List<Light> lights;
    public float timeBetweenLights = 20.0f;
    private bool isTurningOff = false;
    private int currentLightIndex = 0;
    private float timeSinceLastLight = 0.0f;
    
    
    private void Start()
    {
        foreach (Light light in lights)
        {
            light.enabled = true;
        }
    }
    
    private void Update()
    {
        if (lights.Count == 0)
        {
            return;
        }

        timeSinceLastLight += Time.deltaTime;
        if (timeSinceLastLight >= timeBetweenLights)
        {
            int randomIndex = Random.Range(0, lights.Count);
            lights[randomIndex].enabled = false;
            lights.RemoveAt(randomIndex);
            timeSinceLastLight = 0.0f;
        }
    }
}
