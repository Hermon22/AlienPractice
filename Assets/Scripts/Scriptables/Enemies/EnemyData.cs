using UnityEngine;

[CreateAssetMenu(fileName ="Enemy",menuName ="SpaceGame/Gameplay/Enemy")]
[System.Serializable]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Information")] 
    [SerializeField]private GameEnums.TypeOfEnemy type;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int health;
    [SerializeField] private int scorePoints;
    
    
    public GameEnums.TypeOfEnemy Type => type;
    public GameObject Prefab => prefab;
    public int Health => health;
    public int ScorePoints => scorePoints;
}
