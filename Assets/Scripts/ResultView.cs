using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    [SerializeField] private float _showSpeed = 5f;
    [SerializeField] private Text _coinsTextField;
    [SerializeField] private Text _multiplierTextField;

    private void OnEnable()
    {
        StartCoroutine(GetResultShow());
    }

    private IEnumerator GetResultShow()
    {
        float coins = 0;
        float multiplier = 0;
        while (true)
        {
            coins = Mathf.MoveTowards(
                coins,
                LevelScore.Score,
                _showSpeed * LevelScore.Score * Time.deltaTime);
            _coinsTextField.text = $"+{coins:0}";

            multiplier = Mathf.MoveTowards(
                multiplier,
                LevelScore.Multiplier,
                _showSpeed * LevelScore.Multiplier * Time.deltaTime);
            _multiplierTextField.text = $"x{multiplier:0}";

            yield return null;
        }
    }
}
