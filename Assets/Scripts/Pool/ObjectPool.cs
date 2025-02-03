using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool;
    private T _prefab;

    public ObjectPool(T prefab, int count, Transform container)
    {
        _prefab = prefab;
        Container = container;
        CreatePull(count);
    }

    public Transform Container { get; }

    public bool HasFreeElements(out T element)
    {
        foreach (var objectToSpawn in _pool)
        {
            if (objectToSpawn.gameObject.activeInHierarchy == false)
            {
                element = objectToSpawn;
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElements(out T element))
            return element;

        return CreateObject();
    }

    private void CreatePull(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private T CreateObject()
    {
        var createdObject = MonoBehaviour.Instantiate(_prefab);
        createdObject.gameObject.SetActive(false);
        _pool.Add(createdObject);
        return createdObject;
    }
}