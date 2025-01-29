using System.Collections.Generic;

public class MedPackSpawner : SinglePrefabSpawnerWithContainers<MedPack>
{
    public void SpawnMedPack()
    {
        if (IsContainersInEnoughDistance(Containers, out List<Container> validContainers))
        {
            MedPack medPack = Pool.GetFreeElement();

            if (medPack != null)
            {
                medPack.SetStartPosition(GetRandomContainer(validContainers));
                Initialize(medPack);
            }
        }
    }

    private protected override void Initialize(MedPack medPack)
    {
        medPack.Activate();

        if(medPack.IsSpawnerSubscribed == false)
        {
            medPack.LifeTimeFinished += Release;
            medPack.SubscribeBySpawner();
        }
    }
}