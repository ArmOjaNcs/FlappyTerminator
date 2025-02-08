using UnityEngine;

public abstract class Shooter : MonoBehaviour, IPauseable
{
    [SerializeField] private protected Transform FirePoint;
    [SerializeField] private protected float Delay;

    private BulletSpawner _bulletSpawner;
    private float _currentTime;

    private protected AudioSource ShotSource;
    private protected bool IsPaused;
    private protected bool IsCanShoot;

    public float DefaultDelay {  get; private set; }

    private void Start()
    {
        ShotSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (DefaultDelay <= 0)
            DefaultDelay = Delay;
    }

    private protected virtual void Update()
    {
        if (IsPaused == false)
        {
            if (IsTimeToShoot() && IsCanShoot)
            {
                Shoot();
                ShotSource.Play();
            }
        }
    }

    public void SetBulletSpawner(BulletSpawner bulletPrefabSpawner)
    {
        _bulletSpawner = bulletPrefabSpawner;
    }

    public void SetDelay(float delay)
    {
        Delay = delay;
    }

    public virtual void Stop()
    {
        IsPaused = true;
        ShotSource.Pause();
    }

    public virtual void Resume()
    {
        IsPaused = false;
        ShotSource.UnPause();
    }

    public void StopShoot()
    {
        IsCanShoot = false;
    }

    public void StartShoot()
    {
        IsCanShoot = true;
    }

    private protected bool IsTimeToShoot()
    {
        _currentTime += Time.deltaTime;

        return _currentTime > Delay;
    }

    private protected void Shoot()
    {
        Bullet bullet = _bulletSpawner.Pool.GetFreeElement();

        if (bullet != null)
        {
            _bulletSpawner.Initialize(bullet, FirePoint, transform.rotation);
            bullet.Rigidbody2D.linearVelocity = _bulletSpawner.Force * GetDirection();
            _currentTime = 0;
        }
    }

    private protected abstract Vector2 GetDirection();
}