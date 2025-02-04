using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : ObjectToSpawn, IPauseable
{
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private Transform _weaponContainer;
    [SerializeField] private Health _health;
    [SerializeField] private ObjectAnimator _objectAnimator;

    private AudioSource _hitSource;
    private Coroutine _deathCoroutine;

    public override event Action<ObjectToSpawn> LifeTimeFinished;
    public event Action StopShoot;
    public event Action StartShoot;
    public event Action<Enemy> ReturnToPool;
    public event Action EnemyKilled;
    
    public Transform WeaponContainer => _weaponContainer;
    public Health Health => _health;

    private void Awake()
    {
        _hitSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StopShoot?.Invoke();
        _deathCoroutine = null;
        Debug.Log(Health.MaxValue);
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
        _hitSource.Pause();
    }

    public void Resume()
    {
        _objectAnimator.Resume();
        _hitSource.UnPause();
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
        if (_health.CurrentValue == 0 && _deathCoroutine == null && isActiveAndEnabled)
            StartCoroutine(PerformDeath());
    }

    private protected override void Release()
    {
        ReturnToPool?.Invoke(this);
        LifeTimeFinished?.Invoke(this);
    }

    private IEnumerator PerformDeath()
    {
        EnemyKilled?.Invoke();
        ReturnToPool?.Invoke(this);
        _objectAnimator.SetHitTrigger();
        _hitSource.Play();
        yield return null;
    }
}