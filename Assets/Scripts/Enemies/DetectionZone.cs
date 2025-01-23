using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DetectionZone : MonoBehaviour
{
    public event Action<bool> TargetInZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            TargetInZone?.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            TargetInZone?.Invoke(false);
    }
}