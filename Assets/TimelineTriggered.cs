using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineTriggered : MonoBehaviour
{
    public PlayableDirector timeline;
    private bool trigged = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            return;
        }
        
        if (!trigged)
        {
            timeline.Play();
            trigged = true;
        }
        
    }
}
