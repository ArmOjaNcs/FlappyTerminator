using UnityEngine;

public class BirdsPool : ObjectPool<Enemy>
{
    public BirdsPool(Enemy prefab, int count, Transform container) : base(prefab, count, container) { }
}