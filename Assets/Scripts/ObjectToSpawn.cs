using System;
using UnityEngine;

public abstract class ObjectToSpawn : MonoBehaviour
{
    private bool _isSpawnerSubscribed = false;

    public abstract event Action<ObjectToSpawn> LifeTimeFinished;

    public bool IsSpawnerSubscribed => _isSpawnerSubscribed;

    public void SetStartPosition(Container container)
    {
        transform.position = container.transform.position;
        transform.SetParent(container.transform);
        container.SetElement(this);
    }

    public void SubscribeBySpawner()
    {
        _isSpawnerSubscribed = true;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}