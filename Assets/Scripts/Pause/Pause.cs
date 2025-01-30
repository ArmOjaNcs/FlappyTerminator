using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour, IPauseable
{
    [SerializeField] private PlayerInput _playerInput;

    private List<IPauseable> _iPauseables = new ();
    private bool _isPlaying;

    private void Awake()
    {
        _isPlaying = true;
    }

    private void OnEnable()
    {
        _playerInput.Paused += OnPaused;
        _playerInput.UnPaused += OnUnPaused;
    }

    private void OnDisable()
    {
        _playerInput.Paused -= OnPaused;
        _playerInput.UnPaused -= OnUnPaused;
    }

    public void Register(IPauseable pauseable)
    {
        _iPauseables.Add(pauseable);
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