using UnityEngine;

public class MainSpawner : MonoBehaviour
{
    [SerializeField] private MainEnvironmentChanger _changer;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ObstaclesSpawner[] _obstaclesSpawners;
    [SerializeField] private Player _player;

    private int _maxEnemiesCount;
    private int _maxObstaclesCount;
    private bool _isPlayerDead;

    public EnemySpawner EnemySpawner => _enemySpawner;

    private void Awake()
    {
        _maxEnemiesCount = GameUtils.StartEnemiesCount;
        _maxObstaclesCount = GameUtils.StartObstaclesCount;
        _isPlayerDead = false;
    }

    private void OnEnable()
    {
        _changer.GroundChanged += OnGroundChanged;
        _player.PlayerDead += OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        _isPlayerDead = true;
    }

    private void OnDisable()
    {
        _changer.GroundChanged -= OnGroundChanged;
    }

    private void Start()
    {
        SpawnMaxEnemyCount();
        SpawnMaxObstaclesCount();
    }

    public void AddMaxEnemiesCount()
    {
        _maxEnemiesCount++;
    }

    public void AddMaxOstaclesCount()
    {
        _maxObstaclesCount++;
    }

    private void OnGroundChanged()
    {
        if(_isPlayerDead == false)
        {
            SpawnMaxEnemyCount();
            SpawnMaxObstaclesCount();
        }
    }

    private void SpawnMaxEnemyCount()
    {
        for (int i = 0; i < _maxEnemiesCount; i++)
            _enemySpawner.SpawnEnemy();
    }

    private void SpawnMaxObstaclesCount()
    {
        for (int i = 0; i < _maxObstaclesCount; i++)
        {
            int randomIndex = Random.Range(0, _obstaclesSpawners.Length);
            _obstaclesSpawners[randomIndex].SpawnObstacle();
        }
    }
}