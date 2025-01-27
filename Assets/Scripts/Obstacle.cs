using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private readonly float _damageOnEnter = 30;
    private readonly float _damageOnStay = 2;
    private readonly float _timeToWait = 1;

    private WaitForSeconds _timeBeforeDamaged;
    private bool _isCanBeDamaged;

    private void Awake()
    {
        _timeBeforeDamaged = new WaitForSeconds(_timeToWait);
        _isCanBeDamaged = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player) && _isCanBeDamaged)
        {
            if(player.TryGetComponent(out Health health))
                StartCoroutine(WaitForCanBeDamaged(health));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.TryGetComponent(out Health health))
                health.TakeDamage(_damageOnStay);
        }
    }

    private IEnumerator WaitForCanBeDamaged(Health health)
    {
        _isCanBeDamaged = false;
        health.TakeDamage(_damageOnEnter);
        yield return _timeBeforeDamaged;
        _isCanBeDamaged = true;
    }
}