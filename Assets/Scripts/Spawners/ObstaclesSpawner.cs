using UnityEngine;

public class ObstaclesSpawner : SpawnerWithContainers
{
    [SerializeField] private DangerObject _prefab;
    [SerializeField] private int _maxCapacity;
    [SerializeField] private Pause _pause;

    private DangerObjectsPool _dangerObjectsPool;
    private float _damageOnEnter;
    private float _damageOnStay;

    private void Awake()
    {
        _dangerObjectsPool = new DangerObjectsPool(_prefab, _maxCapacity, transform);
        _damageOnEnter = UpgradeUtils.StartDamageOnEnterForObstacle;
        _damageOnStay = UpgradeUtils.StartDamageOnStayForObstacle;
    }

    public void SpawnObstacle()
    {
        if (TryFillUpValidContainers())
        {
            DangerObject obstacle = _dangerObjectsPool.GetFreeElement();

            obstacle.SetStartPosition(GetRandomContainer());
            Initialize(obstacle);
        }
    }

    public void AddDamageOnEnter(float damage)
    {
        _damageOnEnter += damage;
    }

    public void AddDamageOnStay(float damage)
    {
        _damageOnStay += damage;
    }

    private void Initialize(DangerObject dangerSign)
    {
        dangerSign.Obstacle.SetDamageOnEnter(_damageOnEnter);
        dangerSign.Obstacle.SetDamageOnStay(_damageOnStay);
        dangerSign.Activate();

        if (dangerSign.IsSpawnerSubscribed == false)
        {
            dangerSign.LifeTimeFinished += Release;

            if (dangerSign.Obstacle != null)
                _pause.Register(dangerSign.Obstacle);

            dangerSign.SubscribeBySpawner();
        }
    }
}