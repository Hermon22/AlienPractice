using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField]private int size = 1;
    [SerializeField]private int columns = 5;
    [SerializeField]private int rows = 5;
  
    
    public void DrawGrid(int currentLevelIndex)
    {
        var enemiesCount = 0;
        for (var x = 0; x < columns; x += size)
        {
            for (var z = 0; z < rows; z += size)
            {
                if (GameController.Instance.listOfLevels[currentLevelIndex].levelLayout[z][x].Type ==
                    GameEnums.TypeOfEnemy.None) continue;
                var objTrans = transform;
                var position = objTrans.position;
                var point = GetNearestPointOnGrid(new Vector3((position.x + x), (position.y + z), 0f), Vector3.zero);
                var tmp = Instantiate(GameController.Instance.listOfLevels[currentLevelIndex].levelLayout[z][x].Prefab, point, Quaternion.identity);
                tmp.transform.parent = gameObject.transform;
                tmp.GetComponent<EnemyController>().index = enemiesCount;
                enemiesCount++;
                GameController.Instance.enemiesOnScreen.Add(tmp);
            }
        }
    }
    
    private Vector3 GetNearestPointOnGrid(Vector3 position, Vector3 originalPosition)
    {
        if (CheckDistanceInRange(position))
        {
            var objPosition = transform.position;
            position -= objPosition;

            var xCount = Mathf.RoundToInt(position.x / size);
            var yCount = Mathf.RoundToInt(position.y / size);
            var zCount = Mathf.RoundToInt(position.z / size);

            var result = new Vector3(
                (float)xCount * size,
                (float)yCount * size,
                (float)zCount * size);

            result += objPosition;

            return result;
        }
        else
        {
            return originalPosition;
        }

    }
    
    private bool CheckDistanceInRange(Vector3 newPosition)
    {
        var position = transform.position;
        return (!((position.x + (size * columns)) <= newPosition.x)) && (!(newPosition.x < position.x)) && (!((position.y + (size * rows)) <= newPosition.y)) && (!(newPosition.y < position.y));
    }
    
}
