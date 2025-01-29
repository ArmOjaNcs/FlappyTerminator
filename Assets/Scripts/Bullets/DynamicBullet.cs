using UnityEngine;

public class DynamicBullet : EnemyBullet
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _linearVelocity;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private protected override void OnEnable()
    {
        base.OnEnable();
        _linearVelocity = Vector2.zero;
        SetDynamicRigidbody();
    }

    public override void Stop()
    {
        base.Stop();

        SetKinematicRigidbody();
    }

    public override void Resume()
    {
        base.Resume();

        SetDynamicRigidbody();
    }

    private void SetKinematicRigidbody()
    {
        _linearVelocity = _rigidbody2D.linearVelocity;
        _rigidbody2D.linearVelocity = Vector2.zero;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    private void SetDynamicRigidbody()
    {
        _rigidbody2D.linearVelocity = _linearVelocity;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private protected override void PerformHit()
    {
        base.PerformHit();
        SetKinematicRigidbody();
    }
}