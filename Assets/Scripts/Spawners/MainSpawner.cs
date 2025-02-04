using System.Collections.Generic;
using UnityEngine;

public class MainSpawner : MonoBehaviour
{
    [SerializeField] private MainEnvironmentChanger _changer;
    [SerializeField] private PoultryHouse _enemySpawner;
    [SerializeField] private List<ObstaclesSpawner> _obstaclesSpawners;
    [SerializeField] private CratesSpawner _cratesSpawner;
    [SerializeField] private MedPackSpawner _medPackSpawner;
    [SerializeField] private Player _player;

    private int _maxEnemiesCount;
    private int _maxObstaclesCount;
    private bool _isPlayerDead;
    private bool _isCanSpawnMedPack;

    public PoultryHouse EnemySpawner => _enemySpawner;

    private void Awake()
    {
        _maxEnemiesCount = GameUtils.StartEnemiesCount;
        _maxObstaclesCount = GameUtils.StartObstaclesCount;
        _isPlayerDead = false;
        _isCanSpawnMedPack = false;
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
        _player.PlayerDead -= OnPlayerDead;
    }

    private void Start()
    {
        _enemySpawner.SpawnEnemies(10);
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

    public void SetIsCanSpawnMedPack()
    {
        _isCanSpawnMedPack = true;
    }

    private void OnGroundChanged()
    {
        if (_isPlayerDead == false)
        {
            SpawnMaxEnemyCount();
            SpawnMaxObstaclesCount();
            _cratesSpawner.SpawnCrates();

            if (_isCanSpawnMedPack)
            {
                _medPackSpawner.SpawnMedPack();
                _isCanSpawnMedPack = false;
            }
        }
    }

    private void SpawnMaxEnemyCount()
    {
        _enemySpawner.SpawnEnemies(_maxEnemiesCount);
    }

    private void SpawnMaxObstaclesCount()
    {
        for (int i = 0; i < _maxObstaclesCount; i++)
        {
            int randomIndex = Random.Range(0, _obstaclesSpawners.Count);
            _obstaclesSpawners[randomIndex].SpawnObstacle();
        }
    }
}