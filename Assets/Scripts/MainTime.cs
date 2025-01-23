using UnityEngine;

public class MainTime : MonoBehaviour
{
    [SerializeField] private MainSpawner _spawner;
    [SerializeField] private Environment[] _environments;
    [SerializeField] private BulletSpawner[] _bulletSpawners;
    [SerializeField] private BulletSpawner _playerBulletSpawner;

    private readonly int _maxBoostLevel = 12; 

    private int _boostLevel;
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
        //_spawner.AddMaxEnemyCount(_boostLevel);

        foreach (Environment environment in _environments)
            environment.AddSpeed(TimeUtils.EnvironmentBoostedSpeed);

        foreach (BulletSpawner bulletSpawner in _bulletSpawners)
            bulletSpawner.AddSpeed(TimeUtils.EnvironmentBoostedSpeed);

        _playerBulletSpawner.DecreaseSpeed(TimeUtils.EnvironmentBoostedSpeed);
    }
}