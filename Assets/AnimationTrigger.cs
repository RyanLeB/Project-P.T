using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator animator;
   
    public void OnTriggerEnter(Collider other)
    {
        animator.SetBool("Triggered", true);
    }
}
