using System;
using UnityEngine;

public class DangerObject : ObjectToSpawn
{
    [SerializeField] private Obstacle _obstacle;

    public override event Action<ObjectToSpawn> LifeTimeFinished;

    public Obstacle Obstacle => _obstacle;

    private protected override void Release()
    {
        LifeTimeFinished?.Invoke(this);
    }
}