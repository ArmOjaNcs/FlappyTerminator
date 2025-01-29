using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MultiPrefabSpawnerWithContainers<Enemy>, IPauseable
{
    [SerializeField] private Armory _armory;

    public event Action EnemyKilled;

    public void SpawnEnemy()
    {
        if (TryGetFreeContainers(out List<Container> containers) &&
           IsContainersInEnoughDistance(containers, out List<Container> validContainers))
        {
            Enemy enemy = GetRandomElement(Pool.GetFreeElements());

            if (enemy != null)
            {
                enemy.SetStartPosition(GetRandomContainer(validContainers));
                Initialize(enemy);
            }
        }
    }

    public void Stop()
    {
        List<Enemy> enemies = Pool.GetAllBusyElements();

        if (enemies.Count > 0)
        {
            foreach (Enemy enemy in enemies)
                enemy.Stop();
        }
    }

    public void Resume()
    {
        List<Enemy> enemies = Pool.GetAllBusyElements();

        if (enemies.Count > 0)
        {
            foreach (Enemy enemy in enemies)
                enemy.Resume();
        }
    }

    private protected override void Initialize(Enemy enemy)
    {
        EnemyWeapon weapon = _armory.GetRandomWeapon();
        weapon.SetEnemyParent(enemy);
        enemy.SetWeapon(weapon);
        enemy.Activate();

        if (enemy.IsSpawnerSubscribed == false)
        {
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