using System;
using System.Collections;
using UnityEngine;

public class PlayerWeapon : Shooter
{
    public readonly int MaxBulletsValue = 50;
    public readonly float TimeForReload = 1.5f;

    private bool _isHaveBullets;
    private WaitForSeconds _waitForReload;
    private Coroutine _reloadCoroutine;

    public event Action<int> BulletsValueChanged;
    public event Action<bool> Reloaded;

    public int CurrentBulletsValue { get; private set; }

    private void Awake()
    {
        CurrentBulletsValue = MaxBulletsValue;
        _isHaveBullets = true;
        _waitForReload = new WaitForSeconds(TimeForReload);
    }

    private protected override void Update()
    {
        if(IsPaused == false)
        {
            if (IsTimeToShoot() && IsCanShoot && _isHaveBullets)
            {
                SetDirection(GetDirection());
                Shoot();
                CurrentBulletsValue--;
                BulletsValueChanged?.Invoke(CurrentBulletsValue);

                if(ShotSource != null)
                {
                    Debug.Log("Shot");
                    ShotSource.Play();
                }
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

    private Vector2 GetDirection()
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