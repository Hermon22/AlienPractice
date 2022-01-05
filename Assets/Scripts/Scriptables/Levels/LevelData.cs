using UnityEngine;
using System;
[CreateAssetMenu(fileName ="Level",menuName ="SpaceGame/Gameplay/Level")]
public class LevelData : ScriptableObject
{
    [Header("Level Information")] 
    [SerializeField]private int index;
    [SerializeField]private Sprite background;
    [SerializeField]private float enemyMovementSpeed;
    [SerializeField]private float enemySteps;
    
    public ArrayEnemy[] levelLayout;

    public int Index => index;
    public Sprite Background => background;
    public float EnemyMovementSpeed => enemyMovementSpeed;
    public float EnemySteps => enemySteps;

}

public class Array<T>
{
    public T[] values;
 
    public Array(int size)
    {
        values = new T[size];
    }
 
    public int Length {get {return values.Length;}}
 
    public T this[int index]
    {
        get{return values[index];}
        set{values[index] = value;}
    }
}
 
[Serializable]
public class ArrayEnemy : Array<EnemyData>
{
    public ArrayEnemy(int size) : base(size){}
}
