using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(LevelManagement))]
public class LevelManagementEditor : Editor
{
    private LevelManagement _levelManagement;
    private GUIStyle _titleStyle;

    private int _prevIndex;
    private bool _isHided;

    private void OnEnable()
    {
        _levelManagement = target as LevelManagement;

        SetTitleStyle();
    }

    public override void OnInspectorGUI()
    {
        DrawLevels();

        if (GUI.changed)
        {
            SetDirty(_levelManagement.gameObject);
        }
    }

    private void SetTitleStyle()
    {
        _titleStyle = new GUIStyle
        {
            fontSize = 24,
            fontStyle = FontStyle.Bold
        };
        _titleStyle.normal.textColor = new Color(.8f, .8f, .8f);
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
            "Level Index",
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
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Levels", _titleStyle);

        if (_isHided == false)
        {
            if (GUILayout.Button("Add", GUILayout.Width(45), GUILayout.Height(25)))
            {
                _levelManagement.Levels.Add(new Level());
            }
        }

        GUILayout.Space(3);


        if (_isHided)
        {
            if (GUILayout.Button("Show", GUILayout.Width(45), GUILayout.Height(25)))
            {
                _isHided = false;
            }
        }
        else
        {
            if (GUILayout.Button("Hide", GUILayout.Width(45), GUILayout.Height(25)))
            {
                _isHided = true;
            }
        }

        EditorGUILayout.EndHorizontal();

        if (_levelManagement.Levels.Count > 0 && _isHided == false)
        {
            for (int i = 0; i < _levelManagement.Levels.Count; i++)
            {
                Level level = _levelManagement.Levels[i];

                if (i == _levelManagement.CurrentLevelIndex)
                {
                    Color cachedColor = GUI.color;
                    GUI.color = new Color(.35f, .35f, .4f);
                    EditorGUILayout.BeginVertical("box");
                    GUI.color = cachedColor;
                }
                else
                    EditorGUILayout.BeginVertical("box");

                level.LevelObject = (GameObject)EditorGUILayout.ObjectField(
                    new GUIContent("Level Object"),
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

                if (GUILayout.Button("Clear", GUILayout.Width(45), GUILayout.Height(25)))
                {
                    _levelManagement.Levels.RemoveAt(i);
                    _levelManagement.Levels.Insert(i, new Level());
                }

                GUILayout.Space(3);

                if (GUILayout.Button("Remove", GUILayout.Width(55), GUILayout.Height(25)))
                {
                    _levelManagement.Levels.Remove(level);
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
