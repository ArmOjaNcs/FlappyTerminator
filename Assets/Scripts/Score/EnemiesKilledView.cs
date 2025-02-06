using TMPro;
using UnityEngine;

public class EnemiesKilledView : MonoBehaviour
{
    private const string Enemies = nameof(Enemies);

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Score _score;

    private void OnEnable()
    {
        _score.EnemiesKilledUpdate += OnEnemiesKilledUpdate;
    }

    private void OnDisable()
    {
        _score.EnemiesKilledUpdate -= OnEnemiesKilledUpdate;
    }

    private void Start()
    {
        _text.text = Enemies + SignUtils.DoubleDot + _score.EnemiesKilled;
    }

    private void OnEnemiesKilledUpdate(int enemiesCount)
    {
        _text.text = Enemies + SignUtils.DoubleDot + enemiesCount;
    }
}