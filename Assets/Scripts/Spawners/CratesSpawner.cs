using UnityEngine;

public class CratesSpawner : MonoBehaviour
{
    [SerializeField] private Crate _prefab;
    [SerializeField] private int _maxCapacity;
    [SerializeField] private Pause _pause;
    [SerializeField] private Transform _target;

    private readonly int _maxCratesByX = 3;
    private readonly int _minCratesByX = 1;
    private readonly int _maxCratesByY = 5;
    private readonly int _minCratesByY = 3;
    private readonly int _multiplier = 3;

    private CratesPool _cratesPool;
    private Vector2 _colliderSize;
    private Vector2 _tempPosition;
    private float _yPosition;
    private float _damageOnEnter;
    private float _damageOnStay;

    private void Awake()
    {
        _cratesPool = new CratesPool(_prefab, _maxCapacity, transform);
        _colliderSize = _prefab.GetComponent<BoxCollider2D>().size;
        _yPosition = transform.position.y;
        _damageOnEnter = UpgradeUtils.StartDamageOnEnterForObstacle;
        _damageOnStay = UpgradeUtils.StartDamageOnStayForObstacle;
    }

    private void Start()
    {
        SpawnCrates();
    }

    public void SpawnCrates()
    {
        transform.position = new Vector2(transform.position.x, _yPosition);
        _tempPosition = transform.position;

        if (transform.position.x < GameUtils.MaxXPositionForCratesSpawner &&
            transform.position.x > GameUtils.MinXPositionForCratesSpawner)
        {
            int maxCratesByX = Random.Range(_minCratesByX, _maxCratesByX);
            int maxCratesByY = Random.Range(_minCratesByY, _maxCratesByY);
            float delta = _colliderSize.x * _multiplier;

            for (int i = 0; i < maxCratesByX; i++)
            {
                for (int j = 0; j < maxCratesByY; j++)
                {
                    Crate crate = _cratesPool.GetFreeElement();
                    Initialize(crate);
                    crate.transform.position = _tempPosition;
                    _tempPosition = new Vector2(crate.transform.position.x, crate.transform.position.y + delta);
                }

                _tempPosition = new Vector2(transform.position.x + delta, transform.position.y);
            }
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

    private void Initialize(Crate crate)
    {
        crate.Activate();

        if (crate.IsSpawnerSubscribed == false)
        {
            crate.LifeTimeFinished += Release;
            crate.SetTarget(_target);
            crate.Obstacle.SetDamageOnEnter(_damageOnEnter);
            crate.Obstacle.SetDamageOnStay(_damageOnStay);
            _pause.Register(crate);

            if (crate.Obstacle != null)
                _pause.Register(crate.Obstacle);

            crate.SubscribeBySpawner();
        }
    }

    private void Release(ObjectToSpawn crate)
    {
        crate.Deactivate();
    }
}