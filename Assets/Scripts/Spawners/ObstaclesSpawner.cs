using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : SinglePrefabSpawnerWithContainers<DangerObject>
{
    [SerializeField] private Pause _pause;

    public void SpawnObstacle()
    {
        if (TryGetFreeContainers(out List<Container> containers) &&
           IsContainersInEnoughDistance(containers, out List<Container> validContainers))
        {
            DangerObject obstacle = Pool.GetFreeElement();

            if(obstacle != null)
            {
                obstacle.SetStartPosition(GetRandomContainer(validContainers));
                Initialize(obstacle);
            }
        }
    }

    private protected override void Initialize(DangerObject dangerSign)
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