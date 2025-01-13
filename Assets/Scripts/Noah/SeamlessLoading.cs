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
    
    private IEnumerator LoadNextRoom()
    {
        isLoading = true;
        
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        loadingOperation.allowSceneActivation = false;
        
        yield return new WaitForSeconds(3);
        
        loadingOperation.allowSceneActivation = true;
        while (!loadingOperation.isDone)
        {
            yield return null;
        }
        
        Scene loadedScene = SceneManager.GetSceneByName(sceneToLoad);
        GameObject[] rootObjects = loadedScene.GetRootGameObjects();
        if (rootObjects.Length > 0)
        {
            rootObjects[0].transform.position = levelSpawningPoint;
            rootObjects[0].transform.rotation = rotateLevel;
        }
        
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        isLoading = false;
        isLoaded = true;
    }
}
