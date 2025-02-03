using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DynamicObstacle : Obstacle
{
    public Rigidbody2D Rigidbody2D { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void Stop()
    {
        base.Stop();
        Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        Rigidbody2D.linearVelocity = Vector2.zero;
        Rigidbody2D.angularVelocity = 0;
    }

    public override void Resume()
    {
        base.Resume();
        Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}