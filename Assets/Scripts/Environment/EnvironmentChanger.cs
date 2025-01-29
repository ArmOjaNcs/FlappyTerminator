using System;
using System.Collections.Generic;
using System.Linq;

public class EnvironmentChanger
{
    private List<EnvironmentMover> _environments;
    private Queue<EnvironmentMover> _environmentsQueue;

    public event Action GroundChanged;

    public EnvironmentChanger(List<EnvironmentMover> environments)
    {
        _environments = new List<EnvironmentMover>(environments);
        _environmentsQueue = new Queue<EnvironmentMover>();

        foreach (EnvironmentMover environment in _environments)
        {
            environment.Finished += OnFinishReached;
            _environmentsQueue.Enqueue(environment);
        }
    }

    private void OnFinishReached(EnvironmentMover environment)
    {
        EnvironmentMover lastEnvironment = _environmentsQueue.Last();

        if (lastEnvironment != null)
            environment.StartPoint.position = lastEnvironment.EndPoint.position;

        _environmentsQueue.Dequeue();
        _environmentsQueue.Enqueue(environment);
        environment.ResetBodyContact();
        GroundChanged?.Invoke();
    }
}