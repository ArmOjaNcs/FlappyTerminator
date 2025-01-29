using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MedPack : ObjectToSpawn
{
    private readonly float _healingPower = 100;

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _wait;
    private bool _isFirstTouch;

    public override event Action<ObjectToSpawn> LifeTimeFinished;

    private void Awake()
    {
        //_audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_wait = new WaitForSeconds(_audioSource.clip.length);
    }

    private void OnEnable()
    {
        _isFirstTouch = true;
        _spriteRenderer.enabled = true;
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
        //_audioSource.Play();
        _spriteRenderer.enabled = false;
        yield return null;
        Release();
    }

    private protected override void Release()
    {
        LifeTimeFinished?.Invoke(this);
    }
}