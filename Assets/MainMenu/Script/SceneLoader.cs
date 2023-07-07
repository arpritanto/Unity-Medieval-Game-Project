using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen; 
    public Slider loadingBar; 
    public Text loadingText; 
    public static bool loadStatus;
    public void Loadlevel(int sceneIndex) //Fungsi Loadlevel dengan parameter sceneIndex
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(sceneIndex));
        loadStatus = false;
    }

    public void Restart(int sceneIndex) //Fungsi Loadlevel dengan parameter sceneIndex
    {
        StartCoroutine(LoadAsync(sceneIndex));
        loadStatus = false;
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); 
        while (!operation.isDone)
        {
            int progress = (int)Mathf.Clamp01(operation.progress/ .9f); 
            loadingBar.value = progress; 
            loadingText.text = progress * 100 + "%"; 
            yield return null; 
        }
    }

    public void LoadSaveGame(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(sceneIndex));
        loadStatus = true;
    }

    public void Exit(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(sceneIndex));
        loadStatus = true;
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
