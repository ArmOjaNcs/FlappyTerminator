using System.Collections;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.TryGetComponent(out Player player))
        {
            if (ReliaseCoroutine == null && isActiveAndEnabled)
                ReliaseCoroutine = StartCoroutine(DealDamage(player.Health));
        }

        if (collision.TryGetComponent(out PlayerBullet bullet) && bullet.IsPerformHit == false)
        {
            if (ReliaseCoroutine == null && isActiveAndEnabled)
                ReliaseCoroutine = StartCoroutine(HitObstacle());
        }
    }
}