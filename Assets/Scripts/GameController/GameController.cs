using UnityEngine;

public class GameController : MonoBehaviourSingleton<GameController>
{
    [Header("Scene Controller")]
    public SceneController sceneController;
    
    [Header("UI Controller")]
    [Space(10)]
    public UIController uiController;

    [HideInInspector] public int levelIndex = 0;
        
    public override void InitializeSingleton()
    {
        SetAsPersistentSingleton();
    }
}