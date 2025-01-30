using UnityEngine;

public class PlayerBullet : Bullet
{
    private protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (ReliaseCoroutine == null && isActiveAndEnabled)
                ReliaseCoroutine = StartCoroutine(DealDamage(enemy.Health));
        }

        if (collision.TryGetComponent(out EnemyBullet bullet) && bullet.IsPerformHit == false)
        {
            if (ReliaseCoroutine == null && isActiveAndEnabled)
                ReliaseCoroutine = StartCoroutine(HitObstacle());
        }
    }
}