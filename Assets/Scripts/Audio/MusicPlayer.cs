using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private List<AudioClip> _music;

    private int _indexOfClip;

    public AudioSource MusicSource => _musicSource;

    private void Awake()
    {
        _indexOfClip = Random.Range(0, _music.Count);
        _musicSource.clip = _music[_indexOfClip];
    }

    private void Start()
    {
        _musicSource.Play();
    }

    private void Update()
    {
        if (_musicSource.isPlaying == false)
            ChangeToNextClip();
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
}