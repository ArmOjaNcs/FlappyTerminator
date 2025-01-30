using System;

public class DangerSign : ObjectToSpawn
{
    public override event Action<ObjectToSpawn> LifeTimeFinished;

    public Obstacle Obstacle { get; private set; }

    private void Awake()
    {
        TryGetComponent(out Obstacle obstacle);

        Obstacle = obstacle;
    }

    private protected override void Release()
    {
        LifeTimeFinished?.Invoke(this);
    }
}