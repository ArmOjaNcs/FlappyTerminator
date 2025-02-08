using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IPauseable
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerWeapon _weapon;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Health _health;
    [SerializeField] private PlayerPilot _playerPilot;
    [SerializeField] private PlayerRotator _playerRotator;
    [SerializeField] private Animator _weaponAnimator;
    [SerializeField] private ObjectAnimator _playerAnimator;
    [SerializeField] private Pause _pause;
    [SerializeField] private AudioClip _damageTaken;
    [SerializeField] private AudioClip _danger;
    [SerializeField] private AudioClip _dead;

    private float _defaultAnimatorSpeed;
    private float _fireRatePercent = 1;
    private float _healthPercent = 1;
    private AudioSource _audioSource;
    private Coroutine _soundCoroutine;
    private WaitForSeconds _waitForDamage;
    private WaitForSeconds _waitForDanger;
    private WaitForSeconds _waitForDead;

    public event Action PlayerPerformDead;
    public event Action PlayerDead;

    public Health Health => _health;
    public ObjectAnimator PlayerAnimator => _playerAnimator;
    public Score Score { get; private set; }
    public Animator WeaponAnimator => _weaponAnimator;
    public int CurrentLevel { get; private set; }
    public int CurrentFlyForceLevel { get; private set; }
    public int CurrentDamageLevel { get; private set; }
    public int CurrentFireRateLevel { get; private set; }
    public int CurrentHealthLevel { get; private set; }
    public int CurrentReloadLevel { get; private set; }
    public int CurrentMaxBulletsLevel { get; private set; }

    private void Awake()
    {
        _weapon.SetBulletSpawner(_bulletSpawner);
        Score = GetComponent<Score>();
        _pause.Register(this);
        _pause.Register(_weapon);
        _pause.Register(_health);
        _pause.Register(_playerPilot);
        _pause.Register(_playerRotator);
        _audioSource = GetComponent<AudioSource>();
        _waitForDamage = new WaitForSeconds(_damageTaken.length);
        _waitForDanger = new WaitForSeconds(_danger.length);
        _waitForDead = new WaitForSeconds(_dead.length);
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    public void Stop()
    {
        _defaultAnimatorSpeed = PlayerAnimator.Animator.speed;
        PlayerAnimator.Animator.speed = 0;
        _weaponAnimator.enabled = false;
    }

    public void Resume()
    {
        PlayerAnimator.Animator.speed = _defaultAnimatorSpeed;
        _weaponAnimator.enabled = true;
    }

    public void AddAnimatorSpeed(float speed)
    {
        PlayerAnimator.Animator.speed += speed;
    }

    public void AddFlyForceLevel(float percent)
    {
        CurrentFlyForceLevel++;
        _playerPilot.AddFlyForcePercent(percent);
    }

    public void AddDamageLevel(float percent)
    {
        CurrentDamageLevel++;
        _bulletSpawner.AddDamagePercent(percent);
    }

    public void AddFireRateLevel(float percent)
    {
        CurrentFireRateLevel++;
        _fireRatePercent -= percent;
        _weapon.SetDelay(_weapon.DefaultDelay * _fireRatePercent);
    }

    public void AddHealthLevel(float percent)
    {
        CurrentHealthLevel++;
        _healthPercent += percent;
        Health.SetMaxHealth(Health.DefaultMaxValue * _healthPercent);
    }

    public void AddReloadLevel(float percent)
    {
        CurrentReloadLevel++;
        _weapon.DecreaseReloadPercent(percent);
    }

    public void AddMaxBulletsLevel(int value)
    {
        CurrentMaxBulletsLevel++;
        _weapon.AddMaxBulletsValue(value);
    }

    public void AddLevel()
    {
        CurrentLevel++;
    }

    private void OnRotateToMax(bool isRotate)
    {
        _playerRotator.RotateToMax(isRotate);
    }

    private void OnRotateToMin(bool isRotate)
    {
        _playerRotator.RotateToMin(isRotate);
    }

    private void OnFlyUp(bool isFlyUp)
    {
        _playerPilot.FlyUp(isFlyUp);
    }

    private void OnStopShoot()
    {
        _weapon.StopShoot();
    }

    private void OnStartShoot()
    {
        _weapon.StartShoot();
    }

    private void OnFlyDown(bool isFallDown)
    {
        _playerPilot.FlyDown(isFallDown);
    }

    private void OnReload()
    {
        _weapon.Reload();
    }

    private void UnSubscribe()
    {
        _playerInput.RotateToMax -= OnRotateToMax;
        _playerInput.RotateToMin -= OnRotateToMin;
        _playerInput.FlyUp -= OnFlyUp;
        _playerInput.StopShoot -= OnStopShoot;
        _playerInput.StartShoot -= OnStartShoot;
        _playerInput.Reload -= OnReload;
        _playerInput.FlyDown -= OnFlyDown;
        _health.HealthEnded -= OnHealthEnded;
    }

    private void Subscribe()
    {
        _playerInput.RotateToMax += OnRotateToMax;
        _playerInput.RotateToMin += OnRotateToMin;
        _playerInput.FlyUp += OnFlyUp;
        _playerInput.StopShoot += OnStopShoot;
        _playerInput.StartShoot += OnStartShoot;
        _playerInput.Reload += OnReload;
        _playerInput.FlyDown += OnFlyDown;
        _health.HealthEnded += OnHealthEnded;
        _health.DamageTaken += OnDamageTaken;
        PlayerAnimator.HitPerformed += OnPlayerDead;
    }

    private void OnHealthEnded()
    {
        UnSubscribe();
        PlayerAnimator.SetHitTrigger();
        PlayerPerformDead?.Invoke();

        if (_soundCoroutine != null)
            StopCoroutine(_soundCoroutine);

        _soundCoroutine = StartCoroutine(SoundRoutine(_dead, _waitForDead));
    }

    private void OnPlayerDead()
    {
        PlayerDead?.Invoke();
    }

    private void OnDamageTaken()
    {
        if (_health.CurrentValue < _health.MaxValue * 0.3f && _soundCoroutine == null)
            _soundCoroutine = StartCoroutine(SoundRoutine(_danger, _waitForDanger));
        else if (_soundCoroutine == null)
            _soundCoroutine = StartCoroutine(SoundRoutine(_damageTaken, _waitForDamage));
    }

    private IEnumerator SoundRoutine(AudioClip audioClip, WaitForSeconds wait)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
        yield return wait;
        _soundCoroutine = null;
    }
}