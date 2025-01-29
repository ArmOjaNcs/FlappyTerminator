using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    private EnemyTarget _enemyTarget;
    private bool _isCanBeDamaged;

    public event Action HealthUpdate;
    public event Action HealthEnded;

    public float MaxValue => _maxValue;
    public float CurrentValue { get; private set; }
    public EnemyTarget EnemyTarget => _enemyTarget;

    private void Awake()
    {
        TryGetComponent(out EnemyTarget component);
        _enemyTarget = component;
        CurrentValue = MaxValue;
        _isCanBeDamaged = true;
    }

    private void OnEnable()
    {
        CurrentValue = MaxValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MedPack medPack) && _enemyTarget != null)
            if (CurrentValue < MaxValue)
                TakeHeal(medPack.GetHealing());
    }

    public void TakeHeal(float heal)
    {
        CurrentValue += heal;

        if (CurrentValue > MaxValue)
            CurrentValue = MaxValue;

        HealthUpdate?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if(_isCanBeDamaged == true)
        {
            CurrentValue -= damage;

            if (CurrentValue < 0)
            {
                CurrentValue = 0;
                HealthEnded?.Invoke();
            }

            HealthUpdate?.Invoke();
        }
    }

    public void SetIsCsnBeDamaged(bool isCsnBeDamaged)
    {
        _isCanBeDamaged = isCsnBeDamaged;
    }
}