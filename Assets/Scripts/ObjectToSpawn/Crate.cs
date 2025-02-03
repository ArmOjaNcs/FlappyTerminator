using System;
using UnityEngine;

public class Crate : ObjectToSpawn, IPauseable
{
    private Transform _targetToFollow;
    private bool _isPaused;

    public override event Action<ObjectToSpawn> LifeTimeFinished;

    public DynamicObstacle Obstacle { get; private set; }

    private void Awake()
    {
        Obstacle = GetComponent<DynamicObstacle>();
    }

    private void Update()
    {
        if (_isPaused == false)
            transform.position = Vector2.MoveTowards(transform.position, _targetToFollow.position, 
            GameUtils.CurrentGroundSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ObjectRemover _))
            Release();
    }

    public void SetTarget(Transform target)
    {
        _targetToFollow = target;
    }

    public void Stop()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }

    private protected override void Release()
    {
        LifeTimeFinished?.Invoke(this);
    }
}