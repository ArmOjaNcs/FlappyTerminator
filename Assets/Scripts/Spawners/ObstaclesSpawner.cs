using System.Collections.Generic;

public class ObstaclesSpawner : SinglePrefabSpawnerWithContainers<DangerSign>
{
    public void SpawnObstacle()
    {
        if (TryGetFreeContainers(out List<Container> containers) &&
           IsContainersInEnoughDistance(containers, out List<Container> validContainers))
        {
            DangerSign obstacle = Pool.GetFreeElement();

            if(obstacle != null)
            {
                obstacle.SetStartPosition(GetRandomContainer(validContainers));
                Initialize(obstacle);
            }
        }
    }

    private protected override void Initialize(DangerSign obstacle)
    {
        obstacle.Activate();

        if (obstacle.IsSpawnerSubscribed == false)
        {
            obstacle.LifeTimeFinished += Release;
            obstacle.SubscribeBySpawner();
        }
    }
}