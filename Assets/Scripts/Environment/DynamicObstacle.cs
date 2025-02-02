using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DynamicObstacle : Obstacle
{
    [SerializeField] private Transform _parent;

    public Rigidbody2D Rigidbody2D { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.SetParent(_parent);
    }
}