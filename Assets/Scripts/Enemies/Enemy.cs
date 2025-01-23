using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : ObjectToSpawn
{
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private Transform _weaponContainer;
    [SerializeField] private Health _health;

    public override event Action<ObjectToSpawn> LifeTimeFinished;
    public event Action<bool> CanShootChanged;
    public event Action<Enemy> ReturnToPool;
    
    public Transform WeaponContainer => _weaponContainer;

    private void Start()
    {
        _detectionZone.TargetInZone += OnTargetInZone;
        _health.HealthUpdate += OnHealthUpdate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ObjectRemover _))
        {
            ReturnToPool?.Invoke(this);
            LifeTimeFinished?.Invoke(this);
        }
    }

    private void OnTargetInZone(bool isTargetInZone)
    {
        CanShootChanged?.Invoke(isTargetInZone);
    }

    private void OnHealthUpdate()
    {
        if(_health.CurrentValue == 0)
        {
            ReturnToPool?.Invoke(this);
            LifeTimeFinished?.Invoke(this);
        }
    }
}