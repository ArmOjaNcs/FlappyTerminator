using System;
using UnityEngine;

public class DangerObject : ObjectToSpawn
{
    [SerializeField] private Obstacle _obstacle;

    public override event Action<ObjectToSpawn> LifeTimeFinished;
    public Obstacle Obstacle { get; private set; }

    private void Awake()
    {
        Obstacle = GetComponent<Obstacle>();   
    }

    private protected override void Release()
    {
        LifeTimeFinished?.Invoke(this);
    }
}