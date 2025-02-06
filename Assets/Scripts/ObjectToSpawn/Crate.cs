using System;
using UnityEngine;

public class Crate : ObjectToSpawn, IPauseable
{
    private Transform _target;
    private bool _isPaused;

    public override event Action<ObjectToSpawn> LifeTimeFinished;

    public DynamicObstacle Obstacle { get; private set; }

    private void Awake()
    {
        Obstacle = GetComponent<DynamicObstacle>();
    }

    private void OnEnable()
    {
        Obstacle.Rigidbody2D.linearVelocity = Vector2.zero;
        Obstacle.Rigidbody2D.angularVelocity = 0;
    }

    private void Update()
    {
        if(_isPaused == false)
        {
            var pos = _target.position;
            pos.y = transform.position.y;

            transform.position = Vector2.MoveTowards(transform.position, pos,
                GameUtils.CurrentGroundSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ObjectRemover _))
            Release();
    }

    public void Stop()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private protected override void Release()
    {
        LifeTimeFinished?.Invoke(this);
    }
}