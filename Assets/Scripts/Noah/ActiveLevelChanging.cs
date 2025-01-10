using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class ActiveLevelChanging : MonoBehaviour
{
    public GameObject FirstLevel;
    public GameObject SecondLevel;
    public GameObject firstDoor;
    public Collider doorCollider;
    
    void Start()
    {
        doorCollider = firstDoor.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstLevel.SetActive(false);
            SecondLevel.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorCollider.isTrigger = false;
        }
    }
}
