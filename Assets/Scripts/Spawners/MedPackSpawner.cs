using UnityEngine;

public class MedPackSpawner : SpawnerWithContainers
{
    [SerializeField] private MedPack _prefab;
    [SerializeField] private int _maxCapacity;

    private MedPacksPool _medPacksPool;

    private void Awake()
    {
        _medPacksPool = new MedPacksPool(_prefab, _maxCapacity, transform);
    }

    public void SpawnMedPack()
    {
        if (TryFillUpValidContainers())
        {
            MedPack medPack = _medPacksPool.GetFreeElement();

            medPack.SetStartPosition(GetRandomContainer());
            Initialize(medPack);
        }
    }

    private void Initialize(MedPack medPack)
    {
        medPack.Activate();

        if (medPack.IsSpawnerSubscribed == false)
        {
            medPack.LifeTimeFinished += Release;
            medPack.SubscribeBySpawner();
        }
    }
}