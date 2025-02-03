using UnityEngine;

public abstract class Shooter : MonoBehaviour, IPauseable
{
    [SerializeField] private protected Transform FirePoint;
    [SerializeField] private protected float Delay;

    private BulletSpawner _bulletSpawner;
    private Vector2 _direction;
    private float _currentTime;

    private protected AudioSource ShotSource;
    private protected bool IsPaused;
    private protected bool IsCanShoot;

    private void Start()
    {
        ShotSource = GetComponent<AudioSource>();
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

    public void Stop()
    {
        IsPaused = true;
        ShotSource.Pause();
    }

    public void Resume()
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

    private protected void SetDirection(Vector2 direction)
    {
        _direction = direction;
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
            _bulletSpawner.Initialize(bullet, _direction, FirePoint, transform.rotation);
            _currentTime = 0;
        }
    }
}