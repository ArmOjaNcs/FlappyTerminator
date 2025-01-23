using UnityEngine;

public abstract class SinglePrefabSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private protected T Prefab;
    [SerializeField] private protected int MaxCapacity;

    public SinglePrefabPool<T> Pool;

    private void Awake()
    {
        Pool = new SinglePrefabPool<T>(Prefab, MaxCapacity, transform);
    }

    private protected void Release(ObjectToSpawn element)
    {
        element.Deactivate();
    }

    private protected void Release(T element)
    {
        element.gameObject.SetActive(false);
    }
}