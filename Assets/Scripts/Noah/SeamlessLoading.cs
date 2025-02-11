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


    /// <summary>
    /// Checks if the player enters the next level trigger
    /// </summary>
    /// <param name="other">The collided object</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLoading && !isLoaded) // && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LoadNextRoom());
        }
    }

    //private bool PlayerHitsTrigger()
    //{
    //    return Input.GetKeyDown(KeyCode.Space);
    //}


    /// <summary>
    /// Loads the next room seamlessly when the player activates the trigger.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator LoadNextRoom()
    {
        isLoading = true;

        //DebugTextLabel.text = $"Loading... {sceneToLoad} {gameObject.GetInstanceID()}";
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        loadingOperation.allowSceneActivation = false;

        //float lastProgress = 0.0f;

        yield return new WaitForSeconds(0.01f);
        loadingOperation.allowSceneActivation = true;
        while (!loadingOperation.isDone)
        {
            //if (loadingOperation.progress - lastProgress > 0.1f)
            //{
            //    lastProgress = loadingOperation.progress;
            //    //DebugTextLabel.text = "Loading... " + (loadingOperation.progress * 100).ToString("F0") + "%";
            //}

            yield return null;
        }

        Scene loadedScene = SceneManager.GetSceneByName(sceneToLoad); // Get the loaded scene
        GameObject[] rootObjects = loadedScene.GetRootGameObjects(); // Get all root objects in the scene
        //Debug.Log($"Loaded scene found {loadedScene.name} root objects {rootObjects} {(rootObjects != null ? rootObjects.Length: "Null!")}");
        if (rootObjects.Length > 0)
        {
            foreach (var item in rootObjects)
            {
                if (item.name.StartsWith("Event"))
                    continue;
                GameObject rootObject = item; // Get the first root object
                //Debug.Log($"Root object found is {rootObject}");
                rootObject.transform.position = levelSpawningPoint; // Move the level
                rootObject.transform.rotation = rotateLevel; // Rotate the level

                for (int i = 1; i < rootObjects.Length; i++) // Start at 1 to skip the level
                {
                    //Destroy(rootObjects[i]); // Destroy all other root objects, expect the level
                }
                break;
            }

        }

        //DebugTextLabel.text = "";

        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()); // Unload the current scene
        isLoading = false;
        isLoaded = true;
    }
}
