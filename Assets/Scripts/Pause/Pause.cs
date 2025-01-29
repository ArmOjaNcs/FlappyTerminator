using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour, IPauseable
{
    [SerializeField] private List<EnvironmentMover> _environments;
    [SerializeField] private List<BulletSpawner> _bulletSpawners;
    [SerializeField] private Player _player;
    [SerializeField] private MainTime _mainTime;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private InputController _inputController;

    private List<IPauseable> _iPauseables;
    private bool _isPlaying;

    private void Awake()
    {
        _iPauseables = new List<IPauseable>();
        _iPauseables.AddRange(_environments);
        _iPauseables.AddRange(_bulletSpawners);
        _iPauseables.Add(_player);
        _iPauseables.Add(_enemySpawner);
        _iPauseables.Add(_mainTime);

        _isPlaying = true;
    }

    private void OnEnable()
    {
        _inputController.Paused += OnPaused;
        _inputController.UnPaused += OnUnPaused;
    }

    private void OnDisable()
    {
        _inputController.Paused -= OnPaused;
        _inputController.UnPaused -= OnUnPaused;
    }

    public void Stop()
    {
        foreach (IPauseable iPauseable in _iPauseables)
            iPauseable.Stop();
    }

    public void Resume()
    {
        foreach(IPauseable iPauseable in _iPauseables)
            iPauseable.Resume();
    }

    private void OnPaused()
    {
        if (_isPlaying)
        {
            Stop();
            _isPlaying = false;
        }
    }

    private void OnUnPaused()
    {
        if (_isPlaying == false)
        {
            Resume();
            _isPlaying = true;
        }
    }
}