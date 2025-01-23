using UnityEngine;

public abstract class SinglePrefabSpawnerWithContainers<T> : SpawnerWithContainers where T : MonoBehaviour
{
    [SerializeField] private protected T Prefab;
    [SerializeField] private protected int MaxCapacity;

    public SinglePrefabPool<T> Pool;

    private void Awake()
    {
        Pool = new SinglePrefabPool<T>(Prefab, MaxCapacity, transform);
    }

    private protected abstract void Initialize(T element);
}