using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private static bool isGameStarted;
    public static bool IsGameStarted => isGameStarted;

    private void OnEnable()
    {
        Observer.Instance.OnStartGame += delegate { isGameStarted = true; };

        Observer.Instance.OnWinLevel += delegate { isGameStarted = false; };
        Observer.Instance.OnLoseLevel += delegate { isGameStarted = false; };
    }
    private void Start()
    {
        Observer.Instance.OnLoadMainMenu();
    }

    public void StartGame()
    {
        Observer.Instance.OnStartGame?.Invoke();
    }
}
