using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerWeapon _weapon;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Health _health;

    public event Action PlayerDead;

    private void Awake()
    {
        _weapon.SetBulletSpawner(_bulletSpawner);
    }

    private void OnEnable()
    {
        _health.HealthEnded += OnHealthEnded;
    }

    private void OnDisable()
    {
        _health.HealthEnded -= OnHealthEnded;
    }

    private void OnHealthEnded()
    {
        PlayerDead?.Invoke();
    }
}