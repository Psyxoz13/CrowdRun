using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(LevelManagement))]
public class LevelManagementEditor : Editor
{
    private LevelManagement _levelManagement;
    private int _prevIndex;

    private void OnEnable()
    {
        _levelManagement = target as LevelManagement;
    }

    public override void OnInspectorGUI()
    {
        DrawLevels();

        if (GUI.changed)
        {
            SetDirty(_levelManagement.gameObject);
        }
    }

    private void DrawLevels()
    {
        DrawSelectedLevel();
        DrawLevelsList();
    }

    private void DrawSelectedLevel()
    {
        EditorGUILayout.BeginHorizontal();

        var index = EditorGUILayout.IntField(
            "Current Index",
            _levelManagement.CurrentLevelIndex);

        if (GUILayout.Button("<<", GUILayout.Width(30), GUILayout.Height(20)))
        {
            _levelManagement.PrevLevel();
        }

        if (GUILayout.Button(">>", GUILayout.Width(30), GUILayout.Height(20)))
        {
            _levelManagement.NextLevel();
        }

        if (index != _prevIndex)
        {
            _levelManagement.SelectLevel(index);
            _prevIndex = index;
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawLevelsList()
    {
        EditorGUILayout.BeginHorizontal("box");

        GUILayout.Label("Levels");

        if (GUILayout.Button("Add", GUILayout.Width(40), GUILayout.Height(25)))
        {
            _levelManagement.Levels.Add(new Level());
        }

        EditorGUILayout.EndHorizontal();


        if (_levelManagement.Levels.Count > 0)
        {
            for (int i = 0; i < _levelManagement.Levels.Count; i++)
            {
                Level level = _levelManagement.Levels[i];

                EditorGUILayout.BeginVertical("box");

                level.LevelObject = (GameObject)EditorGUILayout.ObjectField(
                    new GUIContent("Level Prefab"),
                    _levelManagement.Levels[i].LevelObject,
                    typeof(GameObject));

                level.SkyboxMaterial = (Material)EditorGUILayout.ObjectField(
                    new GUIContent("Skybox"),
                    _levelManagement.Levels[i].SkyboxMaterial,
                    typeof(Material));

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Select", GUILayout.Width(60), GUILayout.Height(25)))
                {
                    _levelManagement.SelectLevel(i);
                }

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Remove", GUILayout.Width(55), GUILayout.Height(25)))
                {
                    _levelManagement.Levels.Remove(level);
                }

                if (GUILayout.Button("Clear", GUILayout.Width(50), GUILayout.Height(25)))
                {
                    _levelManagement.Levels.RemoveAt(i);
                    _levelManagement.Levels.Insert(i, new Level());
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }
    }

    private void SetDirty(GameObject gameObject)
    {
        EditorUtility.SetDirty(gameObject);
        EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }
}
