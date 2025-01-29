using UnityEngine;

public class RocketBullet : Bullet
{
    [SerializeField] private Animator _nozzlesAnimator;

    public override void Stop()
    {
        base.Stop();

        _nozzlesAnimator.enabled = false;
    }

    public override void Resume()
    {
        base.Resume();

        _nozzlesAnimator.enabled = true;
    }
}