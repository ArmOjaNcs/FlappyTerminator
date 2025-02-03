using UnityEngine;

public class CratesPool : ObjectPool<Crate>
{
    public CratesPool(Crate prefab, int count, Transform container) : base(prefab, count, container) { }
}