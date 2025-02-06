using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour, IPauseable
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private UIAnimator _pauseUI;
    [SerializeField] private PauseMenu _pauseMenu;

    private List<IPauseable> _iPauseables = new ();
    private bool _isPlaying;

    private void Awake()
    {
        _isPlaying = true;
    }

    private void OnEnable()
    {
        Subscribe();
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
        InvokeStop();
        _pauseUI.Show();
    }

    public void Resume()
    {
        InvokeResume();

        if(_pauseUI.isActiveAndEnabled)
            _pauseUI.Hide();

        if(_pauseMenu.UpgradeMenu.isActiveAndEnabled)
            _pauseMenu.UpgradeMenu.Hide();
    }

    public void EndGame()
    {
        InvokeStop();
        UnSubscribe();
    }

    private void Subscribe()
    {
        _playerInput.Paused += OnPaused;
        _playerInput.UnPaused += OnUnPaused;
    }

    private void UnSubscribe()
    {
        _playerInput.Paused -= OnPaused;
        _playerInput.UnPaused -= OnUnPaused;
    }

    private void InvokeStop()
    {
        foreach (IPauseable iPauseable in _iPauseables)
            iPauseable.Stop();
    }

    private void InvokeResume()
    {
        foreach (IPauseable iPauseable in _iPauseables)
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