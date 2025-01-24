using UnityEngine;

public class MainSpawner : MonoBehaviour
{
    [SerializeField] private MainEnvironmentChanger _changer;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ObstaclesSpawner[] _obstaclesSpawners;

    private int _maxEnemiesCount;
    private int _maxObstaclesCount;

    private void Awake()
    {
        _maxEnemiesCount = TimeUtils.StartEnemiesCount;
        _maxObstaclesCount = TimeUtils.StartObstaclesCount;
    }

    private void OnEnable()
    {
        _changer.GroundChanged += OnGroundChanged;
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
        SpawnMaxEnemyCount();
        SpawnMaxObstaclesCount();
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