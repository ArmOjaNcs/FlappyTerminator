using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ObjectToSpawnAnimator : MonoBehaviour
{
    private const string Hit = nameof(Hit);
    private const string Exit = nameof(Exit);

    private Animator _animator;

    public event Action HitPerformed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetHitTrigger()
    {
        _animator.SetTrigger(Hit);
    }

    public void InvokeHitPerformed()
    {
        _animator.SetTrigger(Exit);
        HitPerformed?.Invoke();
    }
}