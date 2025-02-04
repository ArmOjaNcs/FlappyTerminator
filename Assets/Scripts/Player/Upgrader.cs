using System;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Score _score;
    [SerializeField] private Armory _armory;
    [SerializeField] private PoultryHouse _enemySpawner;
    [SerializeField] private List<BulletSpawner> _enemyBulletsSpawners;
    [SerializeField] private TimeAccelerator _timeAccelerator;
    [SerializeField] private Pause _pause;
    [SerializeField] private UIAnimator _upgradeWindow;

    private readonly float _percentForEnemyUpgrade = 0.05f;
    private readonly float _percentForPlayerUpgrade = 0.1f;
    private readonly int _enemiesCountWithUpgrade = 2;
    private readonly int _maxPlayerLevel = 60;
    private readonly int _maxAbilityLevel = 10;
    private readonly int _bulletsCountOnUpgrade = 10;

    private int _enemiesForNextLevel = 5;
    private int _currentEnemiesKilled;

    public event Action FlyForceOnMaxLevel;
    public event Action FireRateOnMaxLevel;
    public event Action DamageOnMaxLevel;
    public event Action HealthOnMaxLevel;
    public event Action ReloadOnMaxLevel;

    private void OnEnable()
    {
        _timeAccelerator.LevelChanged += OnLevelChanged;
        _score.EnemiesKilledUpdate += OnEnemiesKilledUpdate;
    }

    private void OnDisable()
    {
        _timeAccelerator.LevelChanged -= OnLevelChanged;
        _score.EnemiesKilledUpdate -= OnEnemiesKilledUpdate;
    }

    public void UgradeFlyForce()
    {
        _player.AddFlyForceLevel(_percentForPlayerUpgrade);

        if (_player.CurrentFlyForceLevel == _maxAbilityLevel)
            FlyForceOnMaxLevel?.Invoke();

        _upgradeWindow.Hide();
        _pause.ApplyPlayerUpgrade();
    }

    public void UgradeFireRate()
    {
        _player.AddFireRateLevel(_percentForPlayerUpgrade);

        if (_player.CurrentFireRateLevel == _maxAbilityLevel)
            FireRateOnMaxLevel?.Invoke();

        _upgradeWindow.Hide();
        _pause.ApplyPlayerUpgrade();
    }

    public void UgradeDamage()
    {
        _player.AddDamageLevel(_percentForPlayerUpgrade);

        if (_player.CurrentDamageLevel == _maxAbilityLevel)
            DamageOnMaxLevel?.Invoke();

        _upgradeWindow.Hide();
        _pause.ApplyPlayerUpgrade();
    }

    public void UgradeHealth()
    {
        _player.AddHealthLevel(_percentForPlayerUpgrade);

        if (_player.CurrentHealthLevel == _maxAbilityLevel)
            HealthOnMaxLevel?.Invoke();

        _upgradeWindow.Hide();
        _pause.ApplyPlayerUpgrade();
    }

    public void UgradeReload()
    {
        _player.AddReloadLevel(_percentForPlayerUpgrade);

        if (_player.CurrentReloadLevel == _maxAbilityLevel)
            ReloadOnMaxLevel?.Invoke();

        _upgradeWindow.Hide();
        _pause.ApplyPlayerUpgrade();
    }

    public void UpgradeMaxBulletsValue()
    {
        _player.AddMaxBulletsLevel(_bulletsCountOnUpgrade);

        if (_player.CurrentMaxBulletsLevel == _maxAbilityLevel)
            ReloadOnMaxLevel?.Invoke();

        _upgradeWindow.Hide();
        _pause.ApplyPlayerUpgrade();
    }

    private void OnLevelChanged()
    {
        _armory.DecreaseAllWeaponsDelay(_percentForEnemyUpgrade);

        foreach (var bulletSpawner in _enemyBulletsSpawners)
            bulletSpawner.AddDamagePercent(_percentForEnemyUpgrade);
           
        _enemySpawner.AddCurrentMaxHealthPercent(_percentForEnemyUpgrade);
    }

    private void OnEnemiesKilledUpdate(int enemiesCount)
    {
        if(enemiesCount >= _enemiesForNextLevel + _currentEnemiesKilled && _player.CurrentLevel < _maxPlayerLevel)
        {
            _pause.UpgradePlayer();
            _upgradeWindow.Show();
            _enemiesForNextLevel += _enemiesCountWithUpgrade;
            _currentEnemiesKilled = enemiesCount;
        }

        Debug.Log(enemiesCount);
    }
}