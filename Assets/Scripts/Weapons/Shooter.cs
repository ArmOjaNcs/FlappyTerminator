using UnityEngine;

public abstract class Shooter : MonoBehaviour, IPauseable
{
    [SerializeField] private protected Transform FirePoint;
    [SerializeField] private protected float Delay;

    private BulletSpawner _bulletSpawner;
    private Vector2 _direction;
    private float _currentTime;

    private protected bool IsPaused;
    private protected bool IsCanShoot;

    private protected virtual void Update()
    {
        if(IsPaused == false)
        {
            if (IsTimeToShoot() && IsCanShoot)
                Shoot();
        }
    }

    public void SetBulletSpawner(BulletSpawner bulletPrefabSpawner)
    {
        _bulletSpawner = bulletPrefabSpawner;
    }

    public void Stop()
    {
        IsPaused = true;
    }

    public void Resume()
    {
        IsPaused = false;
    }

    private protected void StopShoot()
    {
        IsCanShoot = false;
    }

    private protected void StartShoot()
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