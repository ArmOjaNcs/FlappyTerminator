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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isPaused == false)
        {
            if (collision.TryGetComponent(out Player player) && _isCanBeDamaged && isActiveAndEnabled)
                StartCoroutine(WaitForCanBeDamaged(player.Health));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isPaused == false)
        {
            if (collision.TryGetComponent(out Player player))
                player.Health.TakeDamage(_damageOnStay);
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