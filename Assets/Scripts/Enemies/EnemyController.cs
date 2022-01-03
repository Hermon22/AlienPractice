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
    
}
