using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Action SceneChanged;
    public Action NewSceneLoaded;

    private bool _isLoading;

    public void ChangeScene(string sceneName)
    {
        if (_isLoading) return;
        if (sceneName == SceneManager.GetActiveScene().name) return;
        SceneChanged?.Invoke();
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        _isLoading = true;
        var currentSceneName = SceneManager.GetActiveScene().name;
        yield return new WaitForSeconds(.3f);
        var sceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!sceneLoad.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return UnloadSceneAsync(currentSceneName);
    }

    private IEnumerator UnloadSceneAsync(string sceneName)
    {
        var sceneUnloaded = SceneManager.UnloadSceneAsync(sceneName);
        while (!sceneUnloaded.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        _isLoading = false;
        NewSceneLoaded?.Invoke();
    }
}
