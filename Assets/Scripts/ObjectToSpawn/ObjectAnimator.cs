using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ObjectAnimator : MonoBehaviour, IPauseable
{
    private const string Hit = nameof(Hit);
    private const string Exit = nameof(Exit);

    public event Action HitPerformed;

    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void SetHitTrigger()
    {
        Animator.SetTrigger(Hit);
    }

    public void InvokeHitPerformed()
    {
        Animator.SetTrigger(Exit);
        HitPerformed?.Invoke();
    }

    public void Stop()
    {
        Animator.enabled = false;
    }

    public void Resume()
    {
        Animator.enabled = true;
    }
}