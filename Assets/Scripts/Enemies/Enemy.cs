using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : ObjectToSpawn
{
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private Transform _weaponContainer;
    [SerializeField] private Health _health;
    [SerializeField] private ObjectToSpawnAnimator _objectToSpawnAnimator;

    public override event Action<ObjectToSpawn> LifeTimeFinished;
    public event Action<bool> CanShootChanged;
    public event Action<Enemy> ReturnToPool;
    public event Action EnemyKilled;
    
    public Transform WeaponContainer => _weaponContainer;

    private void Start()
    {
        _detectionZone.TargetInZone += OnTargetInZone;
        _health.HealthUpdate += OnHealthUpdate;
        _objectToSpawnAnimator.HitPerformed += Release;
    }

    private void OnTargetInZone(bool isTargetInZone)
    {
        CanShootChanged?.Invoke(isTargetInZone);
    }

    private void OnHealthUpdate()
    {
        if(_health.CurrentValue == 0)
        {
            EnemyKilled?.Invoke();
            ReturnToPool?.Invoke(this);
            _objectToSpawnAnimator.SetHitTrigger();
        }
    }

    private protected override void Release()
    {
        ReturnToPool?.Invoke(this);
        LifeTimeFinished?.Invoke(this);
    }
}