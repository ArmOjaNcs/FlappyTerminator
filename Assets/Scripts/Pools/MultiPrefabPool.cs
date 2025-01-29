using System.Collections.Generic;
using UnityEngine;

public class MultiPrefabPool<T> where T : MonoBehaviour
{
    private List<T> _pool;
    private List<T> _prefabs;

    public MultiPrefabPool(List<T> prefabs, Transform container)
    {
        _prefabs = new List<T>(prefabs);
        Container = container;
        CreatePull();
    }

    public Transform Container { get; }

    public bool HasFreeElements(out List<T> elements)
    {
        elements = new List<T>();

        foreach (var objectToSpawn in _pool)
        {
            if (objectToSpawn.gameObject.activeInHierarchy == false)
                elements.Add(objectToSpawn);
        }

        return elements.Count > 0;
    }

    public List<T> GetFreeElements()
    {
        if (HasFreeElements(out List<T> elements))
            return elements;

        return CreateObjects();
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

    private void CreatePull()
    {
        _pool = new List<T>();
        CreateObjects();
    }

    private List<T> CreateObjects()
    {
        List<T> newObjects = new List<T>();

        for (int i = 0; i < _prefabs.Count; i++)
        {
            var createdObject = MonoBehaviour.Instantiate(_prefabs[i]);
            createdObject.gameObject.SetActive(false);
            newObjects.Add(createdObject);
        }

        _pool.AddRange(newObjects);
        return newObjects;
    }
}