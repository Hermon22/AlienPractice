using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Windows")]
    
    [Header("Loading")]
    [Space(10)]
    [SerializeField]private WindowsController loadingScreen;
    
    [Header("Main Menu")]
    [Space(10)]
    [SerializeField]private WindowsController mainMenu;

    private void Awake()
    {
        StartCoroutine(StartUp());
    }

    private void Start()
    {
        GameController.Instance.sceneController.SceneChanged += ShowLoadingScreen;
        GameController.Instance.sceneController.NewSceneLoaded += HideLoadingScreen;
    }

    private void ShowLoadingScreen()
    {
        StartCoroutine(MainMenuDelay());
    }
    
    private void HideLoadingScreen()
    {
        StartCoroutine(LoadingDelay());
    }

    public void GoToLevel(int level)
    {
        GameController.Instance.levelIndex = level;
        GameController.Instance.sceneController.ChangeScene("GameScene");
    }
    
    public void CloseGame()
    {
       Application.Quit();
    }
    
    public void GoToMenu()
    {
        GameController.Instance.sceneController.ChangeScene("MainMenu");
    }

    private void OnDestroy()
    {
        GameController.Instance.sceneController.SceneChanged -= ShowLoadingScreen;
        GameController.Instance.sceneController.NewSceneLoaded -= HideLoadingScreen;
    }
    
    private IEnumerator MainMenuDelay()
    {
        loadingScreen.OpenWindow();
        yield return new WaitForSeconds(0.5f);
        mainMenu.CloseWindow();
    }
    
    private IEnumerator LoadingDelay()
    {
        yield return new WaitForSeconds(2);
        loadingScreen.CloseWindow();
    }

    private IEnumerator StartUp()
    {
        loadingScreen.OpenWindow();
        yield return new WaitForSeconds(1);
        mainMenu.OpenWindow();
        yield return new WaitForSeconds(1);
        loadingScreen.CloseWindow();
    }
}
