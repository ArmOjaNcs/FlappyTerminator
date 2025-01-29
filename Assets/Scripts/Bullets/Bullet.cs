using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Bullet : ObjectToSpawn, IPauseable
{
    [SerializeField] private ObjectToSpawnAnimator _objectToSpawnAnimator;

    private float _damage;
    private float _currentLifeTime;
    private float _defaultLifeTime;
    private float _speed;
    private Vector2 _direction;
    private Coroutine _lifeCoroutine;
    private WaitForSeconds _lifeTime;
    private Transform _parentBody;
    private bool _isStop;

    private protected Coroutine ReliaseCoroutine;

    public bool IsPerformHit { get; private set; }

    public override event Action<ObjectToSpawn> LifeTimeFinished;

    private void Start()
    {
        _objectToSpawnAnimator.HitPerformed += Release;
    }

    private protected virtual void OnEnable()
    {
        _lifeCoroutine = StartCoroutine(BeginLifeTime());
        ReliaseCoroutine = null;
        _isStop = false;
        IsPerformHit = false;
        _objectToSpawnAnimator.transform.position = transform.position;
        _currentLifeTime = _defaultLifeTime;
    }

    private void OnDisable()
    {
        if (ReliaseCoroutine != null)
            StopCoroutine(ReliaseCoroutine);

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
        if (collision.TryGetComponent(out Obstacle _))
        {
            if (ReliaseCoroutine == null && isActiveAndEnabled)
                ReliaseCoroutine = StartCoroutine(HitObstacle());
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

    public void SetLifeTime(WaitForSeconds lifeTime, float defaultLifeTime)
    {
        _lifeTime = lifeTime;
        _defaultLifeTime = defaultLifeTime;
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

    public void SetParentBody(Transform parentBody)
    {
        _parentBody = parentBody;
    }

    public virtual void Stop()
    {
        _isStop = true;
        _objectToSpawnAnimator.Stop();
        
        if(_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);
    }

    public virtual void Resume()
    {
        _isStop = false;
        _objectToSpawnAnimator.Resume();

        if(isActiveAndEnabled)
            _lifeCoroutine = StartCoroutine(BeginLifeTime(_currentLifeTime));
    }

    private protected override void Release()
    {
        transform.parent = null;
        LifeTimeFinished?.Invoke(this);
    }

    private void Move()
    {
        transform.Translate(_direction.normalized * _speed * Time.deltaTime);
        _currentLifeTime -= Time.deltaTime;
    }

    private IEnumerator BeginLifeTime(float currentLifeTime = 0)
    {
        if(currentLifeTime == 0) 
            yield return _lifeTime;
        else 
            yield return new WaitForSeconds(currentLifeTime);
        
        Release();
    }

    private protected IEnumerator DealDamage(Health health)
    {
        if (_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);

        health.TakeDamage(_damage);
        yield return null;
        PerformHit();
    }

    private protected IEnumerator HitObstacle()
    {
        if (_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);

        yield return null;
        PerformHit();
    }

    private protected virtual void PerformHit()
    {
        _isStop = true;
        IsPerformHit = true;
        transform.SetParent(_parentBody);
        _objectToSpawnAnimator.SetHitTrigger();
    }
}