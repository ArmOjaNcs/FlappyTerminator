using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private protected T Prefab;
    [SerializeField] private protected int MaxCapacity;

    public ObjectPool<T> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<T>(Prefab, MaxCapacity, transform);
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