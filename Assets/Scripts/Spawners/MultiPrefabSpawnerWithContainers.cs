using System.Collections.Generic;
using UnityEngine;

public abstract class MultiPrefabSpawnerWithContainers<T> : SpawnerWithContainers where T : MonoBehaviour
{
    [SerializeField] private protected List<T> Prefabs;

    public MultiPrefabPool<T> Pool;

    private void Awake()
    {
        Pool = new MultiPrefabPool<T>(Prefabs, transform);
    }

    public T GetRandomElement(List<T> elements)
    {
        return elements[Random.Range(0, elements.Count)];
    }

    private protected abstract void Initialize(T element);
}