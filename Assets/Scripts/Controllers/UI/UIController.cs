using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Windows")]
    
    [Header("Loading")]
    [Space(10)]
    [SerializeField]private WindowsController loadingScreen;
    
    [Header("Main Menu")]
    [Space(10)]
    [SerializeField]private WindowsController mainMenu;
    
    [Header("Gameplay UI")]
    [Space(10)]
    [SerializeField]private WindowsController gameplayUI;
    
    [Header("Loading")]
    [Space(10)]
    [SerializeField]private WindowsController loseWindow;
    
    [Header("Loading")]
    [Space(10)]
    [SerializeField]private WindowsController victoryWindow;
    
    [Header("Loading")]
    [Space(10)]
    [SerializeField]private WindowsController pauseWindow;
    
    [Header("Texts")]
    
    [Header("Score UI")]
    [Space(10)]
    [SerializeField]private TextMeshProUGUI  scoreText;
    [SerializeField]private TextMeshProUGUI  scoreTextLose;
    [SerializeField]private TextMeshProUGUI  scoreTextVictory;

    private bool _goingToGameScene = true;

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
        StartCoroutine(MainMenuDelay(_goingToGameScene));
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
    
    public void PauseGame()
    {
        pauseWindow.OpenWindow();
        StartCoroutine(WaitToPause());
    }

    public void UpdateScore(int score)
    {
        scoreText.text = ""+score;
        scoreTextLose.text = ""+score;
        scoreTextVictory.text = ""+score;
    }
    
    public void RestartScore()
    {
        scoreText.text = ""+0;
        scoreTextLose.text = ""+0;
        scoreTextVictory.text = ""+0;
    }

    public void OpenLoseWindow()
    {
        loseWindow.OpenWindow();
    }
    public void OpenVictoryWindow()
    {
        victoryWindow.OpenWindow();
    }
    
    public void CloseGame()
    {
       Application.Quit();
    }
    
    public void GoToMenu()
    {
        _goingToGameScene = false;
        GameController.Instance.sceneController.ChangeScene("MainMenu");
    }
    
    public void RestartScene()
    {
        GameController.Instance.ClearEnemiesList();
        StartCoroutine(RestartSceneVisual());
    }
    
    public void ResumeGame()
    {
        StartCoroutine(WaitToUnPause());
    }

    private void OnDestroy()
    {
        GameController.Instance.sceneController.SceneChanged -= ShowLoadingScreen;
        GameController.Instance.sceneController.NewSceneLoaded -= HideLoadingScreen;
    }

    private IEnumerator RestartSceneVisual()
    {
        Time.timeScale = 1;
        loadingScreen.OpenWindow();
        yield return new WaitForSeconds(1f);
        GameController.Instance.totalScore = 0;
        RestartScore();
        pauseWindow.CloseWindow();
        loseWindow.CloseWindow();
        victoryWindow.CloseWindow();
        SceneManager.LoadScene("GameScene");
        StartCoroutine(LoadingDelay());
    }

    private IEnumerator WaitToPause()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
    }

    private IEnumerator WaitToUnPause()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.5f);
        pauseWindow.CloseWindow();
    }
    
    private IEnumerator MainMenuDelay(bool goingToGameplay)
    {
        if (goingToGameplay)
        {
            loadingScreen.OpenWindow();
            yield return new WaitForSeconds(0.5f);
            mainMenu.CloseWindow();
            yield return new WaitForSeconds(0.5f);
            gameplayUI.OpenWindow();
        }
        else
        {
            Time.timeScale = 1;
            _goingToGameScene = true;
            loadingScreen.OpenWindow();
            yield return new WaitForSeconds(1);
            pauseWindow.CloseWindow();
            loseWindow.CloseWindow();
            victoryWindow.CloseWindow();
            gameplayUI.CloseWindow();
            yield return new WaitForSeconds(0.5f);
            mainMenu.OpenWindow();
        }
      
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
