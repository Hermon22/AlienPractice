using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviourSingleton<GameController>
{
    [Header("Scene Controller")]
    public SceneController sceneController;
    
    [Header("UI Controller")]
    [Space(10)]
    public UIController uiController;
    
    [Header("List of Levels")]
    [Space(10)]
    public LevelData[] listOfLevels;

    public int totalScore = 0;
    
    [HideInInspector] public int levelIndex = 0;
    [HideInInspector] public List<GameObject> enemiesOnScreen = new List<GameObject>();
        
    public override void InitializeSingleton()
    {
        SetAsPersistentSingleton();
    }
    
    public void UpdateScore(int points)
    {
        totalScore += points;
        uiController.UpdateScore(totalScore);
    }
    
    public void RestartScore()
    {
        totalScore = 0;
        uiController.RestartScore();
    }

    public void CheckForVictory()
    {
        if (enemiesOnScreen.Count <= 0)
        {
            uiController.OpenVictoryWindow();
        }
    }

    public void ClearEnemiesList()
    {
        if (enemiesOnScreen.Count <= 0) return;
        foreach (var t in enemiesOnScreen)
        {
            Destroy(t.gameObject);
        }
        enemiesOnScreen.Clear();
    }
    
}