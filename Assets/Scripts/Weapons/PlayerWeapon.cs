using System;
using System.Collections;
using UnityEngine;

public class PlayerWeapon : Shooter
{
    private readonly int _maxBulletsValue = 30;

    [SerializeField] private Transform _aim;

    private bool _isHaveBullets;
    private WaitForSeconds _waitForReload;
    private Coroutine _reloadCoroutine;
    private float _reloadPercent = 1;
    private float _defaultTimeForReload;
    private  float _timeForReload = 2; 

    public event Action<int> BulletsValueChanged;
    public event Action<bool> Reloaded;

    public int CurrentBulletsValue { get; private set; }
    public int MaxBulletsValue { get; private set; }

    private void Awake()
    {
        MaxBulletsValue = _maxBulletsValue;
        _defaultTimeForReload = _timeForReload;
        CurrentBulletsValue = MaxBulletsValue;
        _isHaveBullets = true;
        _waitForReload = new WaitForSeconds(_timeForReload);
    }

    private protected override void Update()
    {
        if (IsPaused == false)
        {
            if (IsTimeToShoot() && IsCanShoot && _isHaveBullets)
            {
                Shoot();
                CurrentBulletsValue--;
                BulletsValueChanged?.Invoke(CurrentBulletsValue);
                ShotSource.Play();
            }

            if (CurrentBulletsValue <= 0)
                Reload();
        }
    }

    public void Reload()
    {
        if (IsPaused == false)
        {
            if (_reloadCoroutine == null)
            {
                _reloadCoroutine = StartCoroutine(BeginReload());
                Reloaded?.Invoke(true);
            }
        }
    }

    public void DecreaseReloadPercent(float percent)
    {
        _reloadPercent -= percent;
        _timeForReload = _defaultTimeForReload * _reloadPercent;
        _waitForReload = new WaitForSeconds(_timeForReload);
    }

    public void AddMaxBulletsValue(int value)
    {
        MaxBulletsValue += value;
        CurrentBulletsValue = MaxBulletsValue;
        BulletsValueChanged?.Invoke(CurrentBulletsValue);
    }

    private protected override Vector2 GetDirection()
    {
        return FirePoint.position - transform.position;
    }

    private IEnumerator BeginReload()
    {
        _isHaveBullets = false;
        yield return _waitForReload;
        _isHaveBullets = true;
        CurrentBulletsValue = MaxBulletsValue;
        Reloaded?.Invoke(false);
        _reloadCoroutine = null;
    }
}