using System;
using UnityEngine;

public class DangerSign : ObjectToSpawn
{
    public override event Action<ObjectToSpawn> LifeTimeFinished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ObjectRemover _))
            LifeTimeFinished?.Invoke(this);
    }
}