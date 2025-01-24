using System;
using UnityEngine;

public abstract class ObjectToSpawn : MonoBehaviour
{
    private bool _isSpawnerSubscribed = false;

    public abstract event Action<ObjectToSpawn> LifeTimeFinished;

    public bool IsSpawnerSubscribed => _isSpawnerSubscribed;

    private protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ObjectRemover _))
            Release();
    }

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

    private protected abstract void Release();
}