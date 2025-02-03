using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerWithContainers : MonoBehaviour
{
    [SerializeField] private protected List<Container> Containers;

    private int _tempRandom;
    private List<Container> _tempContainers = new();

    private protected List<Container> ValidContainers = new();

    private protected bool TryGetFreeContainers()
    {
        _tempContainers.Clear();

        foreach (Container container in Containers)
        {
            if (container.IsHasElement == false)
                _tempContainers.Add(container);
        }

        return _tempContainers.Count > 0;
    }

    private protected bool IsContainersInEnoughDistance(List<Container> containers)
    {
        ValidContainers.Clear();

        foreach (Container container in containers)
        {
            if (container.transform.position.x >= GameUtils.MinXPosition &&
                container.transform.position.x <= GameUtils.MaxXPosition)
                ValidContainers.Add(container);
        }

        return ValidContainers.Count > 0;
    }

    private protected bool TryFillUpValidContainers()
    {
        if (TryGetFreeContainers() &&
          IsContainersInEnoughDistance(_tempContainers))
            return true;

        return false;
    }

    private protected Container GetRandomContainer()
    {
        _tempRandom = Random.Range(0, ValidContainers.Count);
        Container container = ValidContainers[_tempRandom];
        return container;
    }

    private protected void RemoveFromValidContainers()
    {
        ValidContainers.RemoveAt(_tempRandom);
    }

    private protected void Release(ObjectToSpawn element)
    {
        element.Deactivate();
    }
}