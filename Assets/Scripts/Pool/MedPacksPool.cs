using UnityEngine;

public class MedPacksPool : ObjectPool<MedPack>
{
    public MedPacksPool(MedPack prefab, int count, Transform container) : base(prefab, count, container) { }
}
