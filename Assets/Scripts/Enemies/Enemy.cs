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
            ReturnToPool?.Invoke(this);
            _objectToSpawnAnimator.SetHitTrigger();
        }
    }

    private protected override void Release()
    {
        int childCount = transform.childCount;
        
        if(childCount > 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out Bullet bullet))
                    bullet.transform.parent = null;
            }
        }
        
        ReturnToPool?.Invoke(this);
        LifeTimeFinished?.Invoke(this);
    }
}