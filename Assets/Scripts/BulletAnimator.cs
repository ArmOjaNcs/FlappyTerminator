using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BulletAnimator : MonoBehaviour
{
    private const string Hit = nameof(Hit);
    private const string Exit = nameof(Exit);

    private Animator _animator;

    public event Action HitPerformed;

    public SpriteRenderer Renderer { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void SetHitTrigger()
    {
        _animator.SetTrigger(Hit);
        Debug.Log("Hit");
    }

    public void InvokeHitPerformed()
    {
        _animator.SetTrigger(Exit);
        HitPerformed?.Invoke();
        Debug.Log("Hit Performed");
    }
}