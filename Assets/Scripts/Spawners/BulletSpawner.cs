using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    [SerializeField] private float _force;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _parentBody;
    [SerializeField] private Pause _pause;

    private WaitForSeconds _time;
    private float _defaultDamage;
    private float _damagePercent = 1;

    public float Force => _force;

    private void Start()
    {
        _time = new WaitForSeconds(_lifeTime);
        _defaultDamage = _damage;
    }

    public float DecreaseForce(float force)
    {
        if (force < 0)
            return _force;

        return _force -= force;
    }

    public float AddForce(float force)
    {
        if(force < 0)
            return _force;

        return _force += force;
    }

    public void AddDamagePercent(float percent)
    {
        _damagePercent += percent;
        _damage = _defaultDamage * _damagePercent;
    }

    public void Initialize(Bullet bullet, Transform firePoint, Quaternion rotation)
    {
        bullet.SetStartPosition(firePoint);
        bullet.SetDamage(_damage);
        bullet.SetLifeTime(_time, _lifeTime);
        bullet.SetRotation(rotation);
        bullet.Rigidbody2D.linearVelocity = Vector2.zero;
        bullet.Rigidbody2D.angularVelocity = 0;
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