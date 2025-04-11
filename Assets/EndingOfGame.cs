using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
            Inventory inventory = player.GetComponent<Inventory>();
            player.cameraLocked = true;
            player.movementLocked = true;
            
            if (player != null)
            {
                player.cameraLocked = true;
                player.movementLocked = true;
                player.characterController.enabled = false;
                inventory.lighter.hasLighter = false;
                if (player.audioSource.isPlaying)
                {
                    player.audioSource.Stop();
                }
                if (player.footstepCoroutine != null)
                {
                    player.StopCoroutine(player.footstepCoroutine);
                    player.footstepCoroutine = null;
                }
            }
            else
            {
                Debug.Log("PlayerController not found on player object");
            }
            
            StartCoroutine(WaitAndLoadScene(33f, 0));
        }
    }
    
    private IEnumerator WaitAndLoadScene(float waitTime, int sceneId) // No idea how the animation is going to work so this will have to do for now
    {
        //UIManager uiManager = GameObject.FindObjectOfType<UIManager>();
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        PlayerController player = GameObject.FindObjectOfType<PlayerController>();
        CharacterController characterController = player.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
        }
        else
        {
            Debug.Log("CharacterController not found on player object");
        }
        yield return new WaitForSeconds(waitTime);
        endingObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId);
        GameManager.manager.currentGameState = GameManager.GameState.MainMenu;
        Cursor.lockState = CursorLockMode.None;
        UIManager uiManager = GameObject.FindObjectOfType<UIManager>();
        uiManager.MainMenuUI();
        Cursor.visible = true;
        // GameManager.manager.ResetValues(); 
        player.PlayerResetPosition();
    }
}
