using System;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class EnvironmentBody : MonoBehaviour
{
    private bool _isFirstContact = true;

    public event Action FinishReached;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ObjectRemover _) && _isFirstContact)
            InvokeFinishAction();
    }

    public void SetFirstContact()
    {
        _isFirstContact = true;
    }

    private void InvokeFinishAction()
    {
        _isFirstContact = false;
        FinishReached?.Invoke();
    }
}