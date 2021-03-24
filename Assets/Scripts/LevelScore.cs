using UnityEngine;

public class LevelScore : MonoBehaviour
{
    public static int Score { get; private set; }

    public delegate void LevelScoreEvent(int score);
    public static event LevelScoreEvent OnAddScore;

    public static float Multiplier { get; set; }

    [Header("Settings")]
    [SerializeField] private int AddScoreCount = 1;

    public void AddScore()
    {
        Score += AddScoreCount;

        OnAddScore?.Invoke(Score);
    }
}
