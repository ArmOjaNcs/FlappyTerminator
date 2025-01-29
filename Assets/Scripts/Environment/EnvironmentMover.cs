 using System;
using UnityEngine;

public class EnvironmentMover : MonoBehaviour, IPauseable
{
    [SerializeField] private EnvironmentBody _body;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField, Range(1,10)] private float _parallax;

    private float _currentSpeed;
    private float _boostedSpeed;
    private float _defaultSpeed;

    public event Action<EnvironmentMover> Finished;

    public Transform StartPoint => _startPoint;
    public Transform EndPoint => _endPoint;

    private void Awake()
    {
        _body.FinishReached += OnFinished;
        _boostedSpeed = GameUtils.StartEnvironmentSpeed;
        SetCurrentSpeed();
    }

    private void Update()
    {
        Move();
    }

    public void AddSpeed(float speed)
    {
        _boostedSpeed += speed;
        SetCurrentSpeed();
    }

    public void ResetBodyContact()
    {
        _body.SetFirstContact();
    }

    public void Stop()
    {
        _defaultSpeed = _currentSpeed;
        _currentSpeed = 0;
    }

    public void Resume()
    {
        _currentSpeed = _defaultSpeed;
    }

    private void Move()
    {
        transform.Translate(-Vector2.right * _currentSpeed * Time.deltaTime);
    }

    private void SetCurrentSpeed()
    {
        _currentSpeed = _boostedSpeed / _parallax;
    }

    private void OnFinished()
    {
        Finished?.Invoke(this);
    }
}