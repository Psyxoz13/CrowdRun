using UnityEngine;

public class Progress : MonoBehaviour
{
    public static int LastLevelIndex
    {
        get
        {
            return PlayerPrefs.GetInt("LastLevelIndex", 0);
        }
        private set
        {
            PlayerPrefs.SetInt("LastLevelIndex", value);
        }
    }

    private bool _isGameEnded;

    private void Awake()
    {
        LevelManagement.Instance.SelectLevel(LastLevelIndex);

        LevelManagement.Instance.OnLevelChanged += (levelIndex) => 
            LastLevelIndex = levelIndex;
    }

    public void LoadNextLevel()
    {
        if (LevelManagement.Instance.HasNextLevel == false)
        {
            _isGameEnded = true;
        }

        if (_isGameEnded)
        {
            LevelManagement.Instance.SelectRandomLevel();
        }
        else
        {
            LevelManagement.Instance.NextLevel();
        }
    }
}
