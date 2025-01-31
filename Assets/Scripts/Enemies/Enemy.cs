using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : ObjectToSpawn, IPauseable
{
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private Transform _weaponContainer;
    [SerializeField] private Health _health;
    [SerializeField] private ObjectAnimator _objectAnimator;

    private Animator _weaponAnimator;

    public override event Action<ObjectToSpawn> LifeTimeFinished;
    public event Action StopShoot;
    public event Action StartShoot;
    public event Action<Enemy> ReturnToPool;
    public event Action EnemyKilled;
    
    public Transform WeaponContainer => _weaponContainer;
    public Health Health => _health;

    private void Awake()
    {
        _weaponAnimator = _weaponContainer.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StopShoot?.Invoke();
    }

    private void Start()
    {
        _detectionZone.TargetInZone += OnTargetInZone;
        _health.HealthUpdate += OnHealthUpdate;
        _objectAnimator.HitPerformed += Release;
    }

    public void Stop()
    {
        _objectAnimator.Stop();
        _weaponAnimator.enabled = false;
    }

    public void Resume()
    {
        _objectAnimator.Resume();
        _weaponAnimator.enabled = true;
    }

    private void OnTargetInZone(bool isTargetInZone)
    {
        if (isTargetInZone)
            StartShoot?.Invoke();
        else
            StopShoot?.Invoke();
    }

    private void OnHealthUpdate()
    {
        if(_health.CurrentValue == 0)
        {
            EnemyKilled?.Invoke();
            ReturnToPool?.Invoke(this);
            _objectAnimator.SetHitTrigger();
        }
    }

    private protected override void Release()
    {
        ReturnToPool?.Invoke(this);
        LifeTimeFinished?.Invoke(this);
    }
}