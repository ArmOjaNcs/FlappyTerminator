using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;

public class SinglePrefabPool<T> where T : MonoBehaviour
{
    private List<T> _pool;
    private T _prefab;

    public SinglePrefabPool(T prefab, int count, Transform container)
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

    public List<T> GetAllBusyElements()
    {
        List<T> busyElements = new List<T>();

        foreach (var objectToSpawn in _pool)
        {
            if (objectToSpawn.gameObject.activeInHierarchy == true)
            {
                busyElements.Add(objectToSpawn);
            }
        }

        return busyElements;
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