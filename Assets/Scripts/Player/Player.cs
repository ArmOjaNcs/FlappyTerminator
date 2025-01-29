using System;
using UnityEngine;

public class Player : MonoBehaviour, IPauseable
{
    [SerializeField] private PlayerWeapon _weapon;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Health _health;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Animator _weaponAnimator;

    private Animator _animator;
    private float _defaultAnimatorSpeed;

    public event Action PlayerDead;

    private void Awake()
    {
        _weapon.SetBulletSpawner(_bulletSpawner);
        _animator = GetComponent<Animator>();
        _animator.speed = 1;
    }

    private void OnEnable()
    {
        _health.HealthEnded += OnHealthEnded;
    }

    private void OnDisable()
    {
        _health.HealthEnded -= OnHealthEnded;
    }

    public void Stop()
    {
        _defaultAnimatorSpeed = _animator.speed;
        _animator.speed = 0;
        _weaponAnimator.enabled = false;
        _weapon.Stop();
        _playerMover.Stop();
        _health.SetIsCsnBeDamaged(false);
    }

    public void Resume()
    {
        _animator.speed = _defaultAnimatorSpeed;
        _weaponAnimator.enabled = true;
        _weapon.Resume();
        _playerMover.Resume();
        _health.SetIsCsnBeDamaged(true);
    }

    public void AddAnimatorSpeed(float speed)
    {
        _animator.speed += speed;
    }

    private void OnHealthEnded()
    {
        PlayerDead?.Invoke();
    }
}