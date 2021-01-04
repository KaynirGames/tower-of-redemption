using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource _audioSource;
    private Dictionary<AudioClip, float> _audioClipTimeDic;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        _audioClipTimeDic = new Dictionary<AudioClip, float>();
    }

    public void PlayMusic(AudioClip clip)
    {
        float playbackTime = 0;

        if (_audioClipTimeDic.TryGetValue(clip, out float time))
        {
            playbackTime = time;
        }

        _audioSource.clip = clip;
        _audioSource.time = playbackTime;
        _audioSource.Play();
    }

    public void StopMusic(bool storePlaybackTime)
    {
        if (storePlaybackTime)
        {
            AudioClip current = _audioSource.clip;

            if (!_audioClipTimeDic.ContainsKey(current))
            {
                _audioClipTimeDic.Add(current, 0);
            }

            _audioClipTimeDic[current] = _audioSource.time;
        }

        _audioSource.Stop();
    }

    public void ClearPlaybackTimeStorage()
    {
        _audioClipTimeDic.Clear();
    }
}
