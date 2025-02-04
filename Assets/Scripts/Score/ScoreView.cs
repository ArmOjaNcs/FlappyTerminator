using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private const string Score = nameof(Score);

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Score _score;

    private void OnEnable()
    {
        _score.ScoreValueUpdate += OnScoreUpdate;
    }

    private void OnDisable()
    {
        _score.ScoreValueUpdate -= OnScoreUpdate;
    }

    private void Start()
    {
        _text.text = Score + SignUtils.DoubleDot + _score.Value;
    }

    private void OnScoreUpdate(int score)
    {
        _text.text = Score + SignUtils.DoubleDot + score;
    }
}