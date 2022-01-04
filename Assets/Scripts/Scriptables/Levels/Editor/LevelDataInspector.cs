using UnityEngine;
using UnityEditor;
 
[CustomEditor(typeof(LevelData))]
public class SomeClassInspector : Editor
{
    int firstDimensionSize;
    int secondDimensionSize;
    string confirmation;
    bool editMode;
 
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
        if(GUILayout.Button("Create A New Level Layout")) editMode = true;
        if(GUILayout.Button("Cancel")) editMode = false;
        EditorGUILayout.EndHorizontal();
 
        return editMode;
    }

    private void CreateNewArray(LevelData someClass)
    {
        GetDimensions();
        if(ConfirmedCanCreate()) CreateArray(someClass);
    }

    private void GetDimensions()
    {
        firstDimensionSize = 5;//EditorGUILayout.IntField("First Dimension Size", firstDimensionSize);
        secondDimensionSize = 11; //EditorGUILayout.IntField("Second Dimension Size", secondDimensionSize);
    }

    private bool ConfirmedCanCreate()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Type YES and press Create to create new level. This will clear your old level!!", EditorStyles.wordWrappedLabel);
        confirmation = EditorGUILayout.TextField(confirmation);
        EditorGUILayout.EndHorizontal();
 
        EditorGUILayout.BeginHorizontal();
        var canCreate = (GUILayout.Button("Create New Level Array") && confirmation == "YES");
        EditorGUILayout.EndHorizontal();

        if (!canCreate) return false;
        confirmation = "";
        editMode = false;
        return true;
    }

    private void CreateArray(LevelData someClass)
    {
        someClass.levelLayout = new ArrayEnemy[firstDimensionSize];
        for(var i = 0; i < firstDimensionSize; i++)
        {
            someClass.levelLayout[i] = new ArrayEnemy(secondDimensionSize);
        }
    }

    private static void SetupArray(LevelData someClass)
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