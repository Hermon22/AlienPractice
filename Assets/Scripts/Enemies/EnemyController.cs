using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyInformation;
    private int _health = 0;
    private int _enemyScore = 0;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _health = enemyInformation.Health;
        _enemyScore = enemyInformation.ScorePoints;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var bullet = other.gameObject.GetComponent<BulletBehavior>();
        if (bullet == null) return;
        _health -= bullet.baseDamage;
        if (_health > 0) return;
        GameController.Instance.UpdateScore(_enemyScore);
        Destroy(gameObject);
    }
}
