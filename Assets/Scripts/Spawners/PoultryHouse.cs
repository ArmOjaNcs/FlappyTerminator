using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoultryHouse : SpawnerWithContainers
{
    [SerializeField] private List<Enemy> _prefabs;
    [SerializeField] private int _maxCapacity;
    [SerializeField] private Armory _armory;
    [SerializeField] private Pause _pause;

    private List<BirdsPool> _birdsPools;
    private float _maxHealthPercent = 1;

    public event Action EnemyKilled;

    private void Awake()
    {
        _birdsPools = new List<BirdsPool>();

        for (int i = 0; i < _prefabs.Count; i++)
        {
            BirdsPool birdsPool = new BirdsPool(_prefabs[i], _maxCapacity, transform);
            _birdsPools.Add(birdsPool);
        }
    }

    public void SpawnEnemies(int count)
    {
        if (TryFillUpValidContainers())
        {
            for (int i = 0; i < count; i++)
            {
                Enemy enemy = _birdsPools[Random.Range(0, _birdsPools.Count)].GetFreeElement();

                enemy.SetStartPosition(GetRandomContainer());
                Initialize(enemy);

                RemoveFromValidContainers();

                if (ValidContainers.Count == 0)
                    break;
            }
        }
    }

    public void AddCurrentMaxHealthPercent(float percent)
    {
        _maxHealthPercent += percent;
    }

    private void Initialize(Enemy enemy)
    {
        EnemyWeapon weapon = _armory.GetRandomWeapon();
        weapon.SetEnemyParent(enemy);
        enemy.Activate();
        enemy.Health.SetMaxHealth(enemy.Health.DefaultMaxValue * _maxHealthPercent);

        if (enemy.IsSpawnerSubscribed == false)
        {
            _pause.Register(enemy);
            _pause.Register(enemy.Health);
            enemy.LifeTimeFinished += Release;
            enemy.EnemyKilled += OnEnemyKilled;
            enemy.SubscribeBySpawner();
        }
    }

    private void OnEnemyKilled()
    {
        EnemyKilled?.Invoke();
    }
}