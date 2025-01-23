using UnityEngine;

public class SinglePrefabPool<T> : ObjectPool<T>
{
    private T _prefab;

    public SinglePrefabPool(T prefab, int count, Transform container)
    {
        _prefab = prefab;
        Container = container;
        CreatePull(count);
    }
}
