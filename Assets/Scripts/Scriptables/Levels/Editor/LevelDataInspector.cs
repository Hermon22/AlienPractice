using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelData))]
public class LevelDataInspector : Editor
{
    private int _firstDimensionSize;
    private int _secondDimensionSize;
    private bool _editMode;
    
      public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var someClass = (LevelData)target;
 
        if(CanCreateNewArray()) CreateNewArray(someClass);
 
        SetupArray(someClass);
    }

      private bool CanCreateNewArray()
    {
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Create New Level")) _editMode = true;
        if(GUILayout.Button("Cancel")) _editMode = false;
        EditorGUILayout.EndHorizontal();
 
        return _editMode;
    }

      private void CreateNewArray(LevelData someClass)
    {
        GetDimensions();
        if(ConfirmedCanCreate()) CreateArray(someClass);
    }

      private void GetDimensions()
    {
      /*  _firstDimensionSize = EditorGUILayout.IntField("Rows", _firstDimensionSize);
        _secondDimensionSize = EditorGUILayout.IntField("Columns", _secondDimensionSize);*/
      //_firstDimensionSize = (int)EditorGUILayout.Slider("Rows", _firstDimensionSize,1,5);
      //_secondDimensionSize = (int)EditorGUILayout.Slider("Columns", _secondDimensionSize,1,11);
      _firstDimensionSize = 5;
      _secondDimensionSize = 11;
    }

      private bool ConfirmedCanCreate()
    {
        EditorGUILayout.BeginHorizontal();
        var canCreate = (GUILayout.Button("Set level layout"));
        EditorGUILayout.EndHorizontal();

        if (!canCreate) return false;
        _editMode = false;
        return true;
    }

      private void CreateArray(LevelData someClass)
    {
        someClass.levelLayout = new ArrayEnemy[_firstDimensionSize];
        for(var i = 0; i < _firstDimensionSize; i++)
        {
            someClass.levelLayout[i] = new ArrayEnemy(_secondDimensionSize);
        }
    }

    private void SetupArray(LevelData someClass)
    {
        if (someClass.levelLayout == null || someClass.levelLayout.Length <= 0) return;
        foreach (var t in someClass.levelLayout)
        {
            EditorGUILayout.BeginHorizontal();
 
            for(var j = 0; j < t.Length; j++)
            {
#pragma warning disable 618
                t[j] = (EnemyData)EditorGUILayout.ObjectField(t[j], typeof(EnemyData), GUILayout.Width(35));
#pragma warning restore 618
                GUILayout.Space(4);
            }
 
            EditorGUILayout.EndHorizontal();
        }
    }
}
