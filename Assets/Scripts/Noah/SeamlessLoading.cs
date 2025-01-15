using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeamlessLoading : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Roomm";
    [SerializeField] private Vector3 levelSpawningPoint;
    [SerializeField] private Quaternion rotateLevel;
    private bool isLoading = false;
    private bool isLoaded = false;
    private AsyncOperation loadingOperation;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLoading && !isLoaded)
        {
            StartCoroutine(LoadNextRoom());
        }
    }

    private bool PlayerHitsTrigger()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    
    public IEnumerator LoadNextRoom()
    {
        isLoading = true;
        
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        loadingOperation.allowSceneActivation = false;
        
        yield return new WaitForSeconds(0);
        
        loadingOperation.allowSceneActivation = true;
        while (!loadingOperation.isDone)
        {
            yield return null;
        }
        
        Scene loadedScene = SceneManager.GetSceneByName(sceneToLoad); // Get the loaded scene
        GameObject[] rootObjects = loadedScene.GetRootGameObjects(); // Get all root objects in the scene
        if (rootObjects.Length > 0)
        {
            GameObject rootObject = rootObjects[0]; // Get the first root object
            rootObject.transform.position = levelSpawningPoint; // Move the level
            rootObject.transform.rotation = rotateLevel; // Rotate the level
            
            for (int i = 1; i < rootObjects.Length; i++) // Start at 1 to skip the level
            {
                Destroy(rootObjects[i]); // Destroy all other root objects, expect the level
            }
        }
        
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()); // Unload the current scene
        isLoading = false;
        isLoaded = true;
    }
}
