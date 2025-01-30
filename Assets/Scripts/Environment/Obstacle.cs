using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPauseable
{
    private readonly float _damageOnEnter = 30;
    private readonly float _damageOnStay = 2;
    private readonly float _timeToWait = 1;

    private WaitForSeconds _timeBeforeDamaged;
    private bool _isCanBeDamaged;
    private bool _isPaused;

    private void Awake()
    {
        _timeBeforeDamaged = new WaitForSeconds(_timeToWait);
        _isCanBeDamaged = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isPaused == false)
        {
            if (collision.gameObject.TryGetComponent(out Player player) && _isCanBeDamaged)
            {
                if (player.TryGetComponent(out Health health))
                    StartCoroutine(WaitForCanBeDamaged(health));
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_isPaused == false)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                if (player.TryGetComponent(out Health health))
                    health.TakeDamage(_damageOnStay);
            }
        }
    }

    public void Stop()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }

    private IEnumerator WaitForCanBeDamaged(Health health)
    {
        _isCanBeDamaged = false;
        health.TakeDamage(_damageOnEnter);
        yield return _timeBeforeDamaged;
        _isCanBeDamaged = true;
    }
}