using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeSettings : MonoBehaviour
{
    public Volume volume;
    
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Volume>(); // Find the Volume component in the scene
        if (volume == null)
        {
            Debug.LogError("Volume component not found in the scene.");
            return;
        }
    }

    private void Update()
    {
        // Volume settings name is Global Volume, this should be found in another scene
        // and not in the scene this script is attached to.
        if (volume == null)
        {
            volume = FindObjectOfType<Volume>();
            if (volume == null)
            {
                Debug.LogError("Volume component not found in the scene.");
                return;
            }
        }
    }

    public void ToggleBloom()
    {
        if (volume.profile.TryGet<Bloom>(out var bloom))
        {
            bloom.active = !bloom.active;
            Debug.Log("Bloom toggled: " + bloom.active);
        }
        else
        {
            Debug.LogError("Bloom component not found in the volume profile.");
        }
    }

    
}
