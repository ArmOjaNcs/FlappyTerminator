using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] private protected Transform FirePoint;
    [SerializeField] private protected float Delay;

    private BulletSpawner _bulletSpawner;
    private Vector2 _direction;
    private float _currentTime;

    private protected bool IsCanShoot;

    private protected virtual void Update()
    {
        if(IsTimeToShoot() && IsCanShoot)
            Shoot();
    }

    public void SetBulletSpawner(BulletSpawner bulletPrefabSpawner)
    {
        _bulletSpawner = bulletPrefabSpawner;
    }

    private protected void SetIsCanShoot(bool canShoot)
    {
        IsCanShoot = canShoot;
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

        if(bullet != null)
        {
            _bulletSpawner.Initialize(bullet, _direction, FirePoint, transform.rotation);
            _currentTime = 0;
        }
    }
}