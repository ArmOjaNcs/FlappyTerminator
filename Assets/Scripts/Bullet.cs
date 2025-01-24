using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Bullet : ObjectToSpawn
{
    [SerializeField] private ObjectToSpawnAnimator _objectToSpawnAnimator;

    private float _damage;
    private float _speed;
    private Vector2 _direction;
    private Coroutine _lifeCoroutine;
    private Coroutine _reliaseCoroutine;
    private WaitForSeconds _lifeTime;
    private bool _isStop;
    private bool _isPlayerTarget;

    public override event Action<ObjectToSpawn> LifeTimeFinished;

    private void Start()
    {
        _objectToSpawnAnimator.HitPerformed += Release;
    }

    private protected virtual void OnEnable()
    {
        _lifeCoroutine = StartCoroutine(BeginLifeTime());
        _reliaseCoroutine = null;
        _isStop = false;
        _objectToSpawnAnimator.transform.position = transform.position;
    }

    private void OnDisable()
    {
        if (_reliaseCoroutine != null)
            StopCoroutine(_reliaseCoroutine);

        if (_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);
    }

    private void Update()
    {
        if (_isStop == false)
            Move();
    }

    private protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.TryGetComponent(out HitZone hitZone) && hitZone.EnemyTarget == null == _isPlayerTarget)
        {
            if (_reliaseCoroutine == null && enabled)
                _reliaseCoroutine = StartCoroutine(DealDamage(hitZone));
        }
        else if (collision.TryGetComponent(out Obstacle obstacle))
        {
            if (_reliaseCoroutine == null && enabled)
                _reliaseCoroutine = StartCoroutine(HitObstacle(obstacle));
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void SetLifeTime(WaitForSeconds lifeTime)
    {
        _lifeTime = lifeTime;
    }

    public void SetStartPosition(Transform shotPoint)
    {
        transform.position = shotPoint.position;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void SetIsPlayerTarget(bool isPlayerTarget)
    {
        _isPlayerTarget = isPlayerTarget;
    }

    private protected override void Release()
    {
        transform.parent = null;
        LifeTimeFinished?.Invoke(this);
    }

    private void Move()
    {
        transform.Translate(_direction.normalized * _speed * Time.deltaTime);
    }

    private IEnumerator BeginLifeTime()
    {
        yield return _lifeTime;
        Release();
    }

    private IEnumerator DealDamage(HitZone hitZone)
    {
        if (_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);

        hitZone.TakeDamage(_damage);
        yield return null;
        PerformHit(hitZone.transform);
    }

    private IEnumerator HitObstacle(Obstacle obstacle)
    {
        if (_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);

        yield return null;
        PerformHit(obstacle.transform);
    }

    private protected virtual void PerformHit(Transform collisionTransform)
    {
        _isStop = true;
        transform.SetParent(collisionTransform);
        _objectToSpawnAnimator.SetHitTrigger();
    }
}