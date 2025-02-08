using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Bullet : ObjectToSpawn, IPauseable
{
    [SerializeField] private ObjectAnimator _objectAnimator;

    private float _damage;
    private float _currentLifeTime;
    private float _defaultLifeTime;
    private Coroutine _lifeCoroutine;
    private WaitForSeconds _lifeTime;
    private Transform _parentBody;
    private Vector2 _linearVelocity;
    private Rigidbody2D _rigidbody2D;
    private bool _isPaused;

    private protected Coroutine ReliaseCoroutine;
    private protected AudioSource HitSource;

    public bool IsPerformHit { get; private set; }

    public override event Action<ObjectToSpawn> LifeTimeFinished;

    public ObjectAnimator ObjectAnimator => _objectAnimator;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _objectAnimator.HitPerformed += Release;
        TryGetComponent(out AudioSource hitSource);
        HitSource = hitSource;
    }

    private void OnEnable()
    {
        _lifeCoroutine = StartCoroutine(BeginLifeTime());
        ReliaseCoroutine = null;
        IsPerformHit = false;
        _objectAnimator.transform.position = transform.position;
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
        if(_isPaused == false)
            _currentLifeTime -= Time.deltaTime;
    }

    private protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DynamicObstacle obstacle))
        {
            if (ReliaseCoroutine == null && isActiveAndEnabled)
            {
                obstacle.Rigidbody2D.AddForceAtPosition(_rigidbody2D.linearVelocity, transform.position);
                ReliaseCoroutine = StartCoroutine(HitObstacle());
            }
        }

        if (collision.TryGetComponent(out Obstacle _))
        {
            if (ReliaseCoroutine == null && isActiveAndEnabled)
                ReliaseCoroutine = StartCoroutine(HitObstacle());
        }
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
        _isPaused = true;

        if(_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);

        if(HitSource != null)
            HitSource.Pause();

        SetKinematicRigidbody();
    }

    public virtual void Resume()
    {
        _isPaused = false;

        if (isActiveAndEnabled)
            _lifeCoroutine = StartCoroutine(BeginLifeTime(_currentLifeTime));

        if (HitSource != null)
            HitSource.UnPause();

        SetDynamicRigidbody();
    }

    private protected override void Release()
    {
        transform.parent = null;
        LifeTimeFinished?.Invoke(this);
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

        if (HitSource != null)
            HitSource.Play();

        health.TakeDamage(_damage);

        yield return null;
        PerformHit();
    }

    private protected IEnumerator HitObstacle()
    {
        if (_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);

        if (HitSource != null)
            HitSource.Play();

        yield return null;
        PerformHit();
    }

    private void SetKinematicRigidbody()
    {
        _linearVelocity = Rigidbody2D.linearVelocity;
        Rigidbody2D.linearVelocity = Vector2.zero;
        Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    private void SetDynamicRigidbody()
    {
        Rigidbody2D.linearVelocity = _linearVelocity;
        Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private protected void PerformHit()
    {
        IsPerformHit = true;
        _objectAnimator.SetHitTrigger();
        transform.SetParent(_parentBody);
        SetKinematicRigidbody();
    }
}