using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPauseable
{
    private readonly float _timeToWait = 1;

    private float _damageOnEnter;
    private float _damageOnStay;
    private WaitForSeconds _timeBeforeDamaged;
    private bool _isCanBeDamaged;
    private bool _isPaused;

    public float DamageOnEnter => _damageOnEnter;
    public float DamageOnStay => _damageOnStay;

    private void Awake()
    {
        _timeBeforeDamaged = new WaitForSeconds(_timeToWait);
        _isCanBeDamaged = true;
        _damageOnEnter = UpgradeUtils.StartDamageOnEnterForObstacle;
        _damageOnStay = UpgradeUtils.StartDamageOnStayForObstacle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isPaused == false)
        {
            if (collision.TryGetComponent(out Player player) && _isCanBeDamaged && isActiveAndEnabled)
                StartCoroutine(WaitForCanBeDamaged(player.Health));

            if (collision.TryGetComponent(out Enemy enemy))
                enemy.Health.TakeDamage(_damageOnEnter);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isPaused == false)
        {
            if (collision.TryGetComponent(out Player player))
                player.Health.TakeDamage(_damageOnStay);

            if (collision.TryGetComponent(out Enemy enemy))
                enemy.Health.TakeDamage(_damageOnStay);
        }
    }

    public void SetDamageOnEnter(float damage)
    {
        _damageOnEnter = damage;
    }

    public void SetDamageOnStay(float damage)
    {
        _damageOnStay = damage;
    }

    public virtual void Stop()
    {
        _isPaused = true;
    }

    public virtual void Resume()
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