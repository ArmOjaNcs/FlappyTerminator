using System;
using UnityEngine;

public class EnemyWeapon : Shooter
{
    private bool _isSpawnerSubscribed = false;

    public event Action<EnemyWeapon> LifeTimeFinished;

    public bool IsSpawnerSubscribed => _isSpawnerSubscribed;

    private void Awake()
    {
        SetDirection(-Vector2.right);
    }

    public void SubscribeBySpawner()
    {
        _isSpawnerSubscribed = true;
    }

    public void SetEnemyParent(Enemy enemy)
    {
        transform.position = enemy.WeaponContainer.position;
        transform.SetParent(enemy.WeaponContainer);
        enemy.StopShoot += StopShoot;
        enemy.StartShoot += StartShoot;
        enemy.ReturnToPool += OnEnemyReturnToPool;
    }

    private void OnEnemyReturnToPool(Enemy enemy)
    {
        enemy.StopShoot -= StopShoot;
        enemy.StartShoot -= StartShoot;
        enemy.ReturnToPool -= OnEnemyReturnToPool;
        LifeTimeFinished?.Invoke(this);
    }
}