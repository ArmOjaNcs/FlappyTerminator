using UnityEngine;

public class HealthBarMover : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position - _player.transform.position;
    }

    private void OnEnable()
    {
        _player.PlayerPerformDead += OnPlayerPerformDead;
    }

    private void OnDisable()
    {
        _player.PlayerPerformDead -= OnPlayerPerformDead;
    }

    private void Update()
    {
        MoveWithOffset();
    }

    private void MoveWithOffset()
    {
        transform.position = _player.transform.position + _offset;
    }

    private void OnPlayerPerformDead()
    {
        gameObject.SetActive(false);
    }
}