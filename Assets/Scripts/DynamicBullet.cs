using UnityEngine;

public class DynamicBullet : Bullet
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private protected override void OnEnable()
    {
        base.OnEnable();
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private protected override void PerformHit(Transform collisionTransform)
    {
        base.PerformHit(collisionTransform);
        _rigidbody2D.linearVelocity = Vector3.zero;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }
}