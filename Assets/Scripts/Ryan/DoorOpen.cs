using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animator doorAnimator;
    public string openAnimationName = "DoorOpen";
    public Collider triggerCollider;
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other == triggerCollider)
        {
            doorAnimator.Play(openAnimationName);
        }
    }
}
