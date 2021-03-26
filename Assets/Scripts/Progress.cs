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

    private void Awake()
    {
        LevelManagement.Instance.OnLevelChanged += (levelIndex) => 
            LastLevelIndex = levelIndex;
    }
}
