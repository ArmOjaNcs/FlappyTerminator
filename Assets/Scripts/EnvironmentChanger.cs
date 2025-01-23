using System;
using System.Collections.Generic;
using System.Linq;

public class EnvironmentChanger
{
    private List<Environment> _environments;
    private Queue<Environment> _environmentsQueue;

    public event Action GroundChanged;

    public EnvironmentChanger(List<Environment> environments)
    {
        _environments = new List<Environment>(environments);
        _environmentsQueue = new Queue<Environment>();

        foreach (Environment environment in _environments)
        {
            environment.Finished += OnFinishReached;
            _environmentsQueue.Enqueue(environment);
        }
    }

    private void OnFinishReached(Environment environment)
    {
        Environment lastEnvironment = _environmentsQueue.Last();

        if (lastEnvironment != null)
            environment.StartPoint.position = lastEnvironment.EndPoint.position;

        _environmentsQueue.Dequeue();
        _environmentsQueue.Enqueue(environment);
        environment.ResetBodyContact();
        GroundChanged?.Invoke();
    }
}