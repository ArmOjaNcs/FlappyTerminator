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
    [SerializeField] private List<ObstaclesSpawner> _obstaclesSpawners;
    [SerializeField] private CratesSpawner _cratesSpawner;
    [SerializeField] private TimeAccelerator _timeAccelerator;

    private int _currentEnemiesKilled;

    public event Action FlyForceOnMaxLevel;
    public event Action FireRateOnMaxLevel;
    public event Action DamageOnMaxLevel;
    public event Action HealthOnMaxLevel;
    public event Action ReloadOnMaxLevel;
    public event Action BulletsOnMaxLevel;
    public event Action LevelAccepted;
    public event Action CanUpgarde;

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
        _player.AddFlyForceLevel(UpgradeUtils.PercentForPlayerFlyUpgrade);

        if (_player.CurrentFlyForceLevel == UpgradeUtils.MaxAbilityLevel)
            FlyForceOnMaxLevel?.Invoke();

        UpgradeUtils.AcceptPlayerLevel();
        LevelAccepted?.Invoke();
    }

    public void UgradeFireRate()
    {
        if(_player.CurrentFireRateLevel == UpgradeUtils.MaxAbilityLevel - 1)
            _player.AddFireRateLevel(UpgradeUtils.PercentForPlayerLastLevelUpgrade);
        else
            _player.AddFireRateLevel(UpgradeUtils.PercentForPlayerUpgrade);


        if (_player.CurrentFireRateLevel == UpgradeUtils.MaxAbilityLevel)
            FireRateOnMaxLevel?.Invoke();

        UpgradeUtils.AcceptPlayerLevel();
        LevelAccepted?.Invoke();
    }

    public void UgradeDamage()
    {
        _player.AddDamageLevel(UpgradeUtils.PercentForPlayerDamageUpgrade);

        if (_player.CurrentDamageLevel == UpgradeUtils.MaxAbilityLevel)
            DamageOnMaxLevel?.Invoke();

        UpgradeUtils.AcceptPlayerLevel();
        LevelAccepted?.Invoke();
    }

    public void UgradeHealth()
    {
        _player.AddHealthLevel(UpgradeUtils.PercentForPlayerHealthUpgrade);

        if (_player.CurrentHealthLevel == UpgradeUtils.MaxAbilityLevel)
            HealthOnMaxLevel?.Invoke();

        UpgradeUtils.AcceptPlayerLevel();
        LevelAccepted?.Invoke();
    }

    public void UgradeReload()
    {
        if (_player.CurrentReloadLevel == UpgradeUtils.MaxAbilityLevel - 1)
            _player.AddReloadLevel(UpgradeUtils.PercentForPlayerLastLevelUpgrade);
        else
            _player.AddReloadLevel(UpgradeUtils.PercentForPlayerUpgrade);

        if (_player.CurrentReloadLevel == UpgradeUtils.MaxAbilityLevel)
            ReloadOnMaxLevel?.Invoke();

        UpgradeUtils.AcceptPlayerLevel();
        LevelAccepted?.Invoke();
    }

    public void UpgradeMaxBulletsValue()
    {
        _player.AddMaxBulletsLevel(UpgradeUtils.BulletsCountOnUpgrade);

        if (_player.CurrentMaxBulletsLevel == UpgradeUtils.MaxAbilityLevel)
            BulletsOnMaxLevel?.Invoke();

        UpgradeUtils.AcceptPlayerLevel();
        LevelAccepted?.Invoke();
    }

    private void OnLevelChanged()
    {
        _armory.DecreaseAllWeaponsDelay(UpgradeUtils.PercentForEnemyUpgrade);

        foreach (var bulletSpawner in _enemyBulletsSpawners)
            bulletSpawner.AddDamagePercent(UpgradeUtils.PercentForEnemyUpgrade);

        foreach (var obstaclesSpawner in _obstaclesSpawners)
        {
            obstaclesSpawner.AddDamageOnEnter(UpgradeUtils.DamageOnEnterForObstacle);
            obstaclesSpawner.AddDamageOnStay(UpgradeUtils.DamageOnStayForObstacle);
        }

        _cratesSpawner.AddDamageOnEnter(UpgradeUtils.DamageOnEnterForObstacle);
        _cratesSpawner.AddDamageOnStay(UpgradeUtils.DamageOnStayForObstacle);
           
        _enemySpawner.AddCurrentMaxHealthPercent(UpgradeUtils.PercentForEnemyHealthUpgrade);
    }

    private void OnEnemiesKilledUpdate(int enemiesCount)
    {
        if(enemiesCount >= UpgradeUtils.EnemiesForNextLevel + _currentEnemiesKilled && 
            _player.CurrentLevel < UpgradeUtils.MaxPlayerLevel)
        {
            _player.AddLevel();
            UpgradeUtils.AddNotAcceptedPlayerLevel();
            UpgradeUtils.AddEnemiesForNextLevel();
            _currentEnemiesKilled = enemiesCount;
            CanUpgarde?.Invoke();
        }
    }
}