using UnityEngine;

public class HealthBarMover : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position - _player.position;
    }

    private void Update()
    {
        MoveWithOffset();
    }

    private void MoveWithOffset()
    {
        transform.position = _player.position + _offset;
    }
}