using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private List<AudioClip> _music;
    [SerializeField] private Player _player;
    [SerializeField] private AudioClip _gameOverClip;

    private int _indexOfClip;
    private bool _isPlaying;

    private void Awake()
    {
        _indexOfClip = Random.Range(0, _music.Count);
        _musicSource.clip = _music[_indexOfClip];
        _isPlaying = true;
    }

    private void OnEnable()
    {
        _player.PlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        _player.PlayerDead -= OnPlayerDead;
    }

    private void Start()
    {
        _musicSource.Play();
    }

    private void Update()
    {
        if (_isPlaying)
        {
            if (_musicSource.isPlaying == false)
                ChangeToNextClip();
        } 
    }

    public void ChangeToNextClip()
    {
        _indexOfClip = ++_indexOfClip % _music.Count;
        _musicSource.clip = _music[_indexOfClip];
        _musicSource.Play();
    }

    public void ChangeToPreviousClip()
    {
        _indexOfClip = --_indexOfClip;

        if(_indexOfClip < 0)
            _indexOfClip = _music.Count - 1;

        _musicSource.clip = _music[_indexOfClip];
        _musicSource.Play();
    }

    private void OnPlayerDead()
    {
        _musicSource.clip = _gameOverClip;
        _musicSource.loop = false;
        _musicSource.Play();
        _isPlaying = false;
    }
}