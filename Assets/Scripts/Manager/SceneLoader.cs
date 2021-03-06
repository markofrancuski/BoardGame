using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public static UnityAction SceneLoaded;

    public const int MainMenuSceneIndex = 0;
    public const int GameSceneIndex = 1;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneLoaded?.Invoke();
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(int sceneIndex)
    {
        StartCoroutine(_LoadSceneAsync(sceneIndex));
    }    
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(_LoadSceneAsync(sceneName));
    }

    private IEnumerator _LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        // Make your own proggress bar and loading assets, etc...
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator _LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        // Make your own proggress bar and loading assets, etc...
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        yield return null;
    } 
}
