using UnityEngine;

public class MainTime : MonoBehaviour
{
    [SerializeField] private MainSpawner _spawner;
    [SerializeField] private Environment[] _environments;
    [SerializeField] private BulletSpawner[] _bulletSpawners;
    [SerializeField] private BulletSpawner _playerBulletSpawner;
    [SerializeField] private Score _score;

    private readonly int _maxBoostLevel = 12; 

    private int _boostLevel = 1;
    private float _currentTime;

    private void Update()
    {
        if (_boostLevel < _maxBoostLevel)
            RunTime();
    }

    private void RunTime()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime > TimeUtils.TimeToNextLevel)
        {
            _currentTime = 0;
            _boostLevel++;
            ApplyAcceleration();
        }
    }

    private void ApplyAcceleration()
    {
        AddMaxEnemiesCount();
        AddMaxObstaclesCount();

        foreach (Environment environment in _environments)
            environment.AddSpeed(TimeUtils.EnvironmentBoostedSpeed);

        foreach (BulletSpawner bulletSpawner in _bulletSpawners)
            bulletSpawner.AddSpeed(TimeUtils.EnvironmentBoostedSpeed);

        _playerBulletSpawner.DecreaseSpeed(TimeUtils.EnvironmentBoostedSpeed);
    }

    private void AddMaxEnemiesCount()
    {
        if(_boostLevel % 4 == 0)
            _spawner.AddMaxEnemiesCount();
    }

    private void AddMaxObstaclesCount()
    {
        if (_boostLevel % 6 == 0)
            _spawner.AddMaxOstaclesCount();
    }
}