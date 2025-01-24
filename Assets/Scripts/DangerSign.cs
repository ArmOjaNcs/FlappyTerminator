using System;

public class DangerSign : ObjectToSpawn
{
    public override event Action<ObjectToSpawn> LifeTimeFinished;

    private protected override void Release()
    {
        transform.DetachChildren();
        LifeTimeFinished?.Invoke(this);
    }
}