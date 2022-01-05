using System;
using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Design elements")]
    [SerializeField]private SpriteRenderer background;
    [SerializeField]private GridController grid;
    
    [Header("Collider Listeners")]
    [Space(10)]
    [SerializeField]private CollisionListener boundaryReachedListenerLeft;
    [SerializeField]private CollisionListener boundaryReachedListenerRight;
    [SerializeField]private float colliderCooldown = 4;

    private GameObject _gridTransform;
    private GameController _gameController;
    private GameEnums.EnemiesDirection _currentEnemiesDirection;
    private bool _loweringEnemies;
    private bool _movedMyEnemiesDown;
    [HideInInspector] public bool enemiesCanMove;

    private void Awake()
    {
        _gameController = GameController.Instance;
        _gridTransform = grid.gameObject;
        _currentEnemiesDirection = GameEnums.EnemiesDirection.Right;
    }

    private void Start()
    {
        background.sprite = _gameController.listOfLevels[_gameController.levelIndex].Background;
        _gameController.RestartScore();
        grid.DrawGrid(_gameController.levelIndex);
        enemiesCanMove = true;
        _loweringEnemies = false;
        _movedMyEnemiesDown = false;
        boundaryReachedListenerLeft.TriggerEnter += BoundaryEnterTriggerLeft;
        boundaryReachedListenerLeft.SetAsTrigger(true);
        boundaryReachedListenerRight.TriggerEnter += BoundaryEnterTriggerRight;
        boundaryReachedListenerRight.SetAsTrigger(true);

        StartCoroutine(MoveEnemies());
    }

    private void BoundaryEnterTriggerLeft(Collider2D collider2D)
    {
        if (!collider2D.GetComponent<EnemyController>()) return;
        DisableColliders();
        _currentEnemiesDirection = GameEnums.EnemiesDirection.Right;
    }
    private void BoundaryEnterTriggerRight(Collider2D collider2D)
    {
        if (!collider2D.GetComponent<EnemyController>()) return;
        DisableColliders();
        _currentEnemiesDirection = GameEnums.EnemiesDirection.Left;
    }

    private void EnableColliders()
    {
        boundaryReachedListenerLeft.gameObject.SetActive(true);
        boundaryReachedListenerRight.gameObject.SetActive(true);
    }
    private void DisableColliders()
    {
        _loweringEnemies = true;
        boundaryReachedListenerLeft.gameObject.SetActive(false);
        boundaryReachedListenerRight.gameObject.SetActive(false);
    }

    private void MoveEnemiesToDirection(GameEnums.EnemiesDirection direction)
    {
        var position = _gridTransform.transform.position;
        position = direction switch
        {
            GameEnums.EnemiesDirection.Left => new Vector3(
                position.x - _gameController.listOfLevels[_gameController.levelIndex].EnemySteps, position.y, 0),
            GameEnums.EnemiesDirection.Right => new Vector3(
                position.x + _gameController.listOfLevels[_gameController.levelIndex].EnemySteps, position.y, 0),
            GameEnums.EnemiesDirection.Down => new Vector3(position.x,
                position.y - _gameController.listOfLevels[_gameController.levelIndex].EnemySteps, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
        _gridTransform.transform.position = position;
    }

    private IEnumerator MoveEnemies()
    {
        while (enemiesCanMove)
        {
            yield return new WaitForSeconds(_gameController.listOfLevels[_gameController.levelIndex].EnemyMovementSpeed);
            if (_loweringEnemies && !_movedMyEnemiesDown)
            {
                _movedMyEnemiesDown = true;
                MoveEnemiesToDirection(GameEnums.EnemiesDirection.Down);
            }
            else
            {
                MoveEnemiesToDirection(_currentEnemiesDirection);
                if (!_loweringEnemies || !_movedMyEnemiesDown) continue;
                _movedMyEnemiesDown = false;
                _loweringEnemies = false;
                StartCoroutine(ColliderEnableCooldown());
            }
        }
    }

    private IEnumerator ColliderEnableCooldown()
    {
        yield return new WaitForSeconds(colliderCooldown);
        EnableColliders();
    }
}
