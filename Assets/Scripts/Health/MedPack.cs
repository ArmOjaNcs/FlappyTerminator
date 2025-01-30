using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MedPack : ObjectToSpawn
{
    private readonly float _healingPower = 100;

    private SpriteRenderer _spriteRenderer;
    private bool _isFirstTouch;

    public override event Action<ObjectToSpawn> LifeTimeFinished;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _isFirstTouch = true;
        _spriteRenderer.enabled = true;
    }

    private protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D (collision);

        if (collision.TryGetComponent(out Player player))
            if (player.Health.CurrentValue < player.Health.MaxValue)
                player.Health.TakeHeal(GetHealing());
    }

    public float GetHealing()
    {
        float heal = 0;

        if (_isFirstTouch)
        {
            _isFirstTouch = false;
            PickUp();
            heal = _healingPower;
        }

        return heal;
    }

    private void PickUp()
    {
        StartCoroutine(PickingUp());
    }

    private IEnumerator PickingUp()
    {
        _spriteRenderer.enabled = false;
        yield return null;
        Release();
    }

    private protected override void Release()
    {
        LifeTimeFinished?.Invoke(this);
    }
}