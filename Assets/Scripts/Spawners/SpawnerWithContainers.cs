using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerWithContainers : MonoBehaviour
{
    [SerializeField] private protected List<Container> Containers;

    public bool TryGetFreeContainers(out List<Container> containers)
    {
        containers = new List<Container>();

        foreach (Container container in Containers)
        {
            if (container.IsHasElement == false)
                containers.Add(container);
        }

        return containers.Count > 0;
    }

    public bool IsContainersInEnoughDistance(List<Container> containers, out List<Container> validContainers)
    {
        validContainers = new List<Container>();

        foreach (Container container in containers)
        {
            if (container.transform.position.x >= GameUtils.MinXPosition &&
                container.transform.position.x <= GameUtils.MaxXPosition)
                validContainers.Add(container);
        }

        return validContainers.Count > 0;
    }

    public Container GetRandomContainer(List<Container> containers)
    {
        Container container = containers[Random.Range(0, containers.Count)];
        return container;
    }

    private protected void Release(ObjectToSpawn element)
    {
        element.Deactivate();
    }
}