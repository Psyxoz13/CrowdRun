using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void RestartLevel()
    {
        LevelManagement.Instance.RestartLevel();
    }
}
