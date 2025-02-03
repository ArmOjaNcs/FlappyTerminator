using UnityEngine;

public class ObstaclesSpawner : SpawnerWithContainers
{
    [SerializeField] private DangerObject _prefab;
    [SerializeField] private int _maxCapacity;
    [SerializeField] private Pause _pause;

    private DangerObjectsPool _dangerObjectsPool;

    private void Awake()
    {
        _dangerObjectsPool = new DangerObjectsPool(_prefab, _maxCapacity, transform);
    }

    public void SpawnObstacle()
    {
        if (TryFillUpValidContainers())
        {
            DangerObject obstacle = _dangerObjectsPool.GetFreeElement();

            obstacle.SetStartPosition(GetRandomContainer());
            Initialize(obstacle);
        }
    }

    private void Initialize(DangerObject dangerSign)
    {
        dangerSign.Activate();

        if (dangerSign.IsSpawnerSubscribed == false)
        {
            dangerSign.LifeTimeFinished += Release;

            if (dangerSign.Obstacle != null)
                _pause.Register(dangerSign.Obstacle);

            dangerSign.SubscribeBySpawner();
        }
    }
}