using System;
using System.Collections;
using UnityEngine;

public class PlayerWeapon : Shooter
{
    [SerializeField] private InputController _inputController;

    public readonly int MaxBulletsValue = 30;
    public readonly float TimeForReload = 1.5f;

    private bool _isHaveBullets;
    private WaitForSeconds _waitForReload;
    private Coroutine _reloadCoroutine;

    public event Action<int> BulletsValueChanged;
    public event Action<bool> Reload;

    public int CurrentBulletsValue { get; private set; }

    private void Awake()
    {
        CurrentBulletsValue = MaxBulletsValue;
        _isHaveBullets = true;
        _waitForReload = new WaitForSeconds(TimeForReload);
    }

    private void OnEnable()
    {
        _inputController.Shoot += SetIsCanShoot;
        _inputController.Reload += OnReload;
    }

    private void OnDisable()
    {
        _inputController.Shoot -= SetIsCanShoot;
        _inputController.Reload -= OnReload;
    }

    private protected override void Update()
    {
        if (IsTimeToShoot() && IsCanShoot && _isHaveBullets && IsPaused == false)
        {
            SetDirection(GetDirection());
            Shoot();
            CurrentBulletsValue--;
            BulletsValueChanged?.Invoke(CurrentBulletsValue);
        }

        if(CurrentBulletsValue <= 0)
            OnReload();
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
        Reload?.Invoke(false);
        _reloadCoroutine = null;
    }

    private void OnReload()
    {
        if(IsPaused == false)
        {
            if (_reloadCoroutine == null)
            {
                _reloadCoroutine = StartCoroutine(BeginReload());
                Reload?.Invoke(true);
            }
        }
    }
}