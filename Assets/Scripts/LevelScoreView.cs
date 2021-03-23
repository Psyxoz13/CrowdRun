using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelScoreView : MonoBehaviour
{
    private Text _textField;

    private void Awake()
    {
        _textField = GetComponent<Text>();
        LevelScore.OnAddScore += UpdateText;
    }

    private void Start()
    {
    }

    private void UpdateText(int score)
    {
        _textField.text = score.ToString();
    }
}
