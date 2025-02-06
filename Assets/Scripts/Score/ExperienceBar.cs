using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    private const string LevelUp = nameof(LevelUp);

    [SerializeField] private Score _score;
    [SerializeField] private Upgrader _upgrader;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Player _player;

    private int _currentEnemiesKilled;

    private void OnEnable()
    {
        _score.EnemiesKilledUpdate += OnEnemiesKilledUpdate;
        _upgrader.LevelAccepted += OnLevelAccepted;
    }

    private void OnDisable()
    {
        _score.EnemiesKilledUpdate -= OnEnemiesKilledUpdate;
        _upgrader.LevelAccepted -= OnLevelAccepted;
    }

    private void Start()
    {
        _text.text = string.Empty;
    }

    private void OnEnemiesKilledUpdate(int enemiesCount)
    {
        if(enemiesCount >= UpgradeUtils.EnemiesForNextLevel + _currentEnemiesKilled &&
            _player.CurrentLevel < UpgradeUtils.MaxPlayerLevel)
        {
            _text.text = LevelUp;
            _slider.value = (float)(enemiesCount - _currentEnemiesKilled) 
                / UpgradeUtils.EnemiesForNextLevel;
            _currentEnemiesKilled = enemiesCount;
        }
        else
        {
            _slider.value = (float)(enemiesCount - _currentEnemiesKilled) 
                / UpgradeUtils.EnemiesForNextLevel;
        }
    }

    private void OnLevelAccepted()
    {
        if (UpgradeUtils.NotAcceptedPlayerLevel > 0)
            _text.text = LevelUp;
        else
            _text.text = string.Empty;
    }
}