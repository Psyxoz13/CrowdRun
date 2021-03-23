using UnityEngine;
using UnityEngine.Events;

public class LevelScore : MonoBehaviour
{
    [Readonly] public int Score;

    public delegate void LevelScoreEvent(int score);
    public static event LevelScoreEvent OnAddScore;

    [Header("Settings")]
    [SerializeField] private int AddScoreCount = 1;

    public void AddScore()
    {
        Score += AddScoreCount;

        OnAddScore?.Invoke(Score);
    }
}
