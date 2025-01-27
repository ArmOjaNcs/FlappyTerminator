using UnityEngine;

public class EnemyBullet : Bullet
{
    private protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.TryGetComponent(out Health health) && health.EnemyTarget != null)
        {
            if (ReliaseCoroutine == null && enabled)
                ReliaseCoroutine = StartCoroutine(DealDamage(health));
        }

        if (collision.TryGetComponent(out PlayerBullet bullet) && bullet.IsPerformHit == false)
        {
            if (ReliaseCoroutine == null && enabled)
                ReliaseCoroutine = StartCoroutine(HitObstacle());
        }
    }
}