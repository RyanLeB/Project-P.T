using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingOfGame : MonoBehaviour
{
    public GameObject endingObject;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endingObject.SetActive(true);
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.cameraLocked = true;
                player.movementLocked = true;
            }
            else
            {
                Debug.Log("PlayerController not found on player object");
            }
        }
    }
}
