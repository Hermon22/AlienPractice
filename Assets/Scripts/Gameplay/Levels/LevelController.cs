using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]private SpriteRenderer background;
    [SerializeField]private GridController grid;
    
    private GameController _gameController;

    private void Awake()
    {
        _gameController = GameController.Instance;
    }

    private void Start()
    {
        background.sprite = _gameController.listOfLevels[_gameController.levelIndex].Background;
        _gameController.RestartScore();
        grid.DrawGrid(_gameController.levelIndex);
    }
}
