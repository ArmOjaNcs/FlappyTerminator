using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _parentBody;
    [SerializeField] private Pause _pause;

    private WaitForSeconds _time;
    private float _defaultDamage;
    private float _damagePercent = 1;

    private void Start()
    {
        _time = new WaitForSeconds(_lifeTime);
        _defaultDamage = _damage;
    }

    public float DecreaseSpeed(float speed)
    {
        if (speed < 0)
            return _speed;

        return _speed -= speed;
    }

    public float AddSpeed(float speed)
    {
        if(speed < 0)
            return _speed;

        return _speed += speed;
    }

    public void AddDamagePercent(float percent)
    {
        _damagePercent += percent;
        _damage = _defaultDamage * _damagePercent;
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
            _pause.Register(bullet);
            _pause.Register(bullet.ObjectAnimator);
            bullet.SubscribeBySpawner();
        }
    }
}