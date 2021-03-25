using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelScoreView : MonoBehaviour
{
    private Text _textField;

    private void Awake()
    {
        LevelScore.OnAddScore += UpdateText;
    }

    private void OnEnable()
    {
        _textField = GetComponent<Text>();
    }

    private void UpdateText(int score)
    {
        _textField.text = score.ToString();
    }
}
