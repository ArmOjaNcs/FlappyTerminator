using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
public class MedPack : ObjectToSpawn
{
    private readonly float _percentOfHealing = 0.33f;

    private SpriteRenderer _spriteRenderer;
    private bool _isFirstTouch;
    private AudioSource _audioSource;
    private WaitForSeconds _wait;

    public override event Action<ObjectToSpawn> LifeTimeFinished;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _wait = new WaitForSeconds(_audioSource.clip.length);
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
                player.Health.TakeHeal(GetHealing(player.Health.MaxValue));
    }

    public float GetHealing(float maxValue)
    {
        float heal = 0;

        if (_isFirstTouch && isActiveAndEnabled)
        {
            _isFirstTouch = false;
            PickUp();
            heal = maxValue * _percentOfHealing;
        }

        return heal;
    }

    private void PickUp()
    {
        StartCoroutine(PickingUp());
    }

    private IEnumerator PickingUp()
    {
        _audioSource.Play();
        _spriteRenderer.enabled = false;
        yield return _wait;
        Release();
    }

    private protected override void Release()
    {
        LifeTimeFinished?.Invoke(this);
    }
}