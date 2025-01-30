using System.Collections.Generic;
using UnityEngine;

public class TimeAccelerator : MonoBehaviour, IPauseable
{
    [SerializeField] private MainSpawner _spawner;
    [SerializeField] private List<EnvironmentMover> _environments;
    [SerializeField] private List<BulletSpawner> _bulletSpawners;
    [SerializeField] private BulletSpawner _playerBulletSpawner;
    [SerializeField] private Player _player;
    [SerializeField] private Score _score;
    [SerializeField] private Pause _pause;

    private int _boostLevel = 1;
    private float _currentTimeForBoost;
    private float _currentTimeForScore;
    private float _currentTimeForMedPack;
    private bool _isPaused;

    private void Awake()
    {
        _isPaused = false;
        _pause.Register(this);
    }

    private void OnEnable()
    {
        _spawner.EnemySpawner.EnemyKilled += AddScoreForEnemy;
    }

    private void OnDisable()
    {
        _spawner.EnemySpawner.EnemyKilled -= AddScoreForEnemy;
    }

    private void Update()
    {
        RunTime();
    }

    public void Stop()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }

    private void RunTime()
    {
        if(_isPaused == false)
        {
            _currentTimeForScore += Time.deltaTime;
            _currentTimeForMedPack += Time.deltaTime;
        }
        
        if (_currentTimeForScore > GameUtils.TimeToAddScore)
        {
            AddScore(_boostLevel);
            _currentTimeForScore = 0;
        }

        if(_currentTimeForMedPack > GameUtils.TimeToSpawnMedPack)
        {
            _spawner.SetIsCanSpawnMedPack();
            _currentTimeForMedPack = 0;
        }

        if (_boostLevel <= GameUtils.MaxBoostLevel && _isPaused == false)
        {
            _currentTimeForBoost += Time.deltaTime;

            if (_currentTimeForBoost > GameUtils.TimeToNextLevel)
            {
                _currentTimeForBoost = 0;
                _boostLevel++;
                ApplyAcceleration();
            }
        }
    }

    private void ApplyAcceleration()
    {
        AddMaxEnemiesCount();
        AddMaxObstaclesCount();

        foreach (EnvironmentMover environment in _environments)
            environment.AddSpeed(GameUtils.EnvironmentBoostedSpeed);

        foreach (BulletSpawner bulletSpawner in _bulletSpawners)
            bulletSpawner.AddSpeed(GameUtils.EnvironmentBoostedSpeed * GameUtils.EnemyBulletMultiplier);

        _playerBulletSpawner.DecreaseSpeed(GameUtils.EnvironmentBoostedSpeed);
        _player.PlayerAnimator.speed += GameUtils.AnimationBoost;
        _player.WeaponAnimator.speed += GameUtils.AnimationBoost;
    }

    private void AddMaxEnemiesCount()
    {
        if (_boostLevel % GameUtils.DividerForAddMaxEnemyCount == 0)
            _spawner.AddMaxEnemiesCount();
    }

    private void AddMaxObstaclesCount()
    {
        if (_boostLevel % GameUtils.DividerForAddMaxObstaclesCount == 0)
            _spawner.AddMaxOstaclesCount();
    }

    private void AddScore(int score)
    {
        _score.AddValue(score);
    }

    private void AddScoreForEnemy()
    {
        _score.AddValue(GameUtils.ScoreByEnemy);
    }
}