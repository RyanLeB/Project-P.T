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
            if (player != null)
            {
                player.cameraLocked = true;
                player.movementLocked = true;
                player.footstepSounds.ToList().ForEach(footstep => footstep.IsDestroyed()); // Later thing
            }
            else
            {
                Debug.Log("PlayerController not found on player object");
            }
            
            StartCoroutine(WaitAndLoadScene(5f, 0));
        }
    }
    
    private IEnumerator WaitAndLoadScene(float waitTime, int sceneId) // No idea how the animation is going to work so this will have to do for now
    {
        //UIManager uiManager = GameObject.FindObjectOfType<UIManager>();
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        PlayerController player = GameObject.FindObjectOfType<PlayerController>();
        yield return new WaitForSeconds(waitTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId);
        //uiManager.MainMenuUI();
        //player.playerCamera.gameObject.SetActive(false);
        GameManager.manager.currentGameState = GameManager.GameState.MainMenu;

    }
}
