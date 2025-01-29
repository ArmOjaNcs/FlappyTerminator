using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : SinglePrefabSpawner<Bullet>, IPauseable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _parentBody;

    private WaitForSeconds _time;

    private void Start()
    {
        _time = new WaitForSeconds(_lifeTime);
    }

    public float DecreaseSpeed(float speed)
    {
        return _speed -= speed;
    }

    public float AddSpeed(float speed)
    {
        return _speed += speed;
    }

    public void Initialize(Bullet bullet, Vector2 direction, Transform firePoint, Quaternion rotation)
    {
        bullet.SetSpeed(_speed);
        bullet.SetDirection(direction);
        bullet.SetStartPosition(firePoint);
        bullet.SetDamage(_damage);
        bullet.SetLifeTime(_time, _lifeTime);
        bullet.SetRotation(rotation);
        bullet.Activate();

        if (bullet.IsSpawnerSubscribed == false)
        {
            bullet.LifeTimeFinished += Release;
            bullet.SetParentBody(_parentBody);
            bullet.SubscribeBySpawner();
        }
    }

    public void Stop()
    {
        List<Bullet> bullets = Pool.GetAllBusyElements();

        if (bullets.Count > 0)
        {
            foreach (Bullet bullet in bullets)
                bullet.Stop();
        }
    }

    public void Resume()
    {
        List<Bullet> bullets = Pool.GetAllBusyElements();

        if (bullets.Count > 0)
        {
            foreach (Bullet bullet in bullets)
                bullet.Resume();
        }
    }
}