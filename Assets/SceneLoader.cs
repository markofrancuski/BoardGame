using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public static UnityAction SceneLoaded;

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

    }
    public void LoadScene(string sceneName)
    {

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

        yield return null;
    }

    private IEnumerator _LoadSceneAsync(string sceneName)
    {

        yield return null;
    } 
}
