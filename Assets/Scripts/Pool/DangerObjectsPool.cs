using UnityEngine;

public class DangerObjectsPool : ObjectPool<DangerObject>
{
    public DangerObjectsPool(DangerObject prefab, int count, Transform container) : base(prefab, count, container) { }
}