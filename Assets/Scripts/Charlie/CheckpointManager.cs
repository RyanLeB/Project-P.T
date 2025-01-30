using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    // For now the player will just go back to the previous checkpoint if they die or whatever,
    // but later I will make a more robust save system that can revert everything back to the way it was

    // THE TELEPORTATION IS WEIRD SO I WILL NEED A TRANSITION FOR IT

    public PlayerController player;

    public Checkpoint currentCheckpoint;
    private Vector3 initalPosition;

    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<PlayerController>();
        initalPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    OnDeath();
        //}
    }

    // SO I HAD TO DO SOMETHING A LITTLE WIERD HERE BECAUSE THE PLAYER WOULD NOT TELEPORT CORRECTLY
    // IF ANYONE KNOWS A BETTER WAY TO DO THIS PLEASE LET ME KNOW

    /// <summary>
    /// Moves the player to the current checkpoint.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator MovePlayerToCurrentCheckpoint()
    {
        player.characterController.enabled = false;
        if (currentCheckpoint != null)
        {
            player.transform.position = currentCheckpoint.transform.position;
        }
        else
        {
            player.transform.position = initalPosition;
        }
        yield return new WaitForSeconds(0.1f);
        player.characterController.enabled = true;
    }

    /// <summary>
    /// TEST PLAYER DEATH METHOD
    /// </summary>
    public void OnDeath()
    {
        StartCoroutine(MovePlayerToCurrentCheckpoint());
    }
}
