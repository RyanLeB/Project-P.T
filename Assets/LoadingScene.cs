using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreenImage;
    public Image LoadingBarFilled;
    public GameObject loadingBarObject;
    
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
        Debug.Log("Loading scene: " + sceneId);
        Debug.Log("Loading screen active: " + LoadingScreenImage.activeSelf);
    }
    
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        
        LoadingScreenImage.SetActive(true);
        LoadingBarFilled.gameObject.SetActive(true);
        loadingBarObject.gameObject.SetActive(true);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFilled.fillAmount = progress;

            // Only manually activate when it reaches 90%
            if (operation.progress >= 0.9f)
            {
                LoadingBarFilled.fillAmount = 1f;
                yield return new WaitForSeconds(13f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
        
        LoadingScreenImage.SetActive(false);
        LoadingBarFilled.gameObject.SetActive(false);
        loadingBarObject.gameObject.SetActive(false);
    }
}
