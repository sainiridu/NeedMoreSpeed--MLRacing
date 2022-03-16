using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Michsky.UI.ModernUIPack;

public class UIController : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject homeScreen;
    public ProgressBar progressBar;
    float progress;

    public bool canDisable;
    public bool showLoading;


    public void Awake()
    {
        Time.timeScale = 1.0f;
    }


    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        if(showLoading)
        loadingScreen.SetActive(true);
        if (canDisable)
        {
            homeScreen.SetActive(false);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            progress = operation.progress;
            progressBar.currentPercent = progress * 100;
            Debug.Log(progress * 100);
            yield return null;
        }
    }

}
