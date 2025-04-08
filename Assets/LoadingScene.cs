using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreenImage;
    public Image LoadingBar;
    public GameObject loadingSlider;
    
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
        LoadingBar.gameObject.SetActive(true);
        loadingSlider.gameObject.SetActive(true);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBar.fillAmount = progress;
            Debug.Log("Loading progress: " + progress);
            Debug.Log(operation.progress);
            yield return null;
        }
        
        LoadingScreenImage.SetActive(false);
        LoadingBar.gameObject.SetActive(false);
        loadingSlider.gameObject.SetActive(false);
    }
}
