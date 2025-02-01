using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour, IPauseable
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private UIAnimator _pauseMenu;

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
        UnSubscribe();
    }

    public void Register(IPauseable pauseable)
    {
        _iPauseables.Add(pauseable);
    }

    public void Stop()
    {
        foreach (IPauseable iPauseable in _iPauseables)
            iPauseable.Stop();

        _pauseMenu.Show();
    }

    public void Resume()
    {
        foreach(IPauseable iPauseable in _iPauseables)
            iPauseable.Resume();

        _pauseMenu.Hide();
    }

    public void EndGame()
    {
        foreach (IPauseable iPauseable in _iPauseables)
            iPauseable.Stop();

        UnSubscribe();
    }

    private void UnSubscribe()
    {
        _playerInput.Paused -= OnPaused;
        _playerInput.UnPaused -= OnUnPaused;
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