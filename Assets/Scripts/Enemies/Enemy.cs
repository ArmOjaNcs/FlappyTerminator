using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : ObjectToSpawn, IPauseable
{
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private Transform _weaponContainer;
    [SerializeField] private Health _health;
    [SerializeField] private ObjectToSpawnAnimator _objectToSpawnAnimator;

    private EnemyWeapon _weapon;
    private Animator _weaponAnimator;

    public override event Action<ObjectToSpawn> LifeTimeFinished;
    public event Action<bool> CanShootChanged;
    public event Action<Enemy> ReturnToPool;
    public event Action EnemyKilled;
    
    public Transform WeaponContainer => _weaponContainer;

    private void Awake()
    {
        _weaponAnimator = _weaponContainer.GetComponent<Animator>();
    }

    private void Start()
    {
        _detectionZone.TargetInZone += OnTargetInZone;
        _health.HealthUpdate += OnHealthUpdate;
        _objectToSpawnAnimator.HitPerformed += Release;
    }

    public void SetWeapon(EnemyWeapon weapon)
    {
        _weapon = weapon;
    }

    public void Stop()
    {
        _objectToSpawnAnimator.Stop();
        _weaponAnimator.enabled = false;

        if (_weapon != null)
            _weapon.Stop();
    }

    public void Resume()
    {
        _objectToSpawnAnimator.Resume();
        _weaponAnimator.enabled = true;

        if (_weapon != null)
            _weapon.Resume();
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