using UnityEngine;

public class BulletSpawner : SinglePrefabSpawner<Bullet>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _parentBody;
    [SerializeField] private Pause _pause;

    private WaitForSeconds _time;

    private void Start()
    {
        _time = new WaitForSeconds(_lifeTime);
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