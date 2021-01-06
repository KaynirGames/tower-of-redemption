using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] private List<SoundClipSO> _soundClips = new List<SoundClipSO>();

    private AudioSource _audioSource;
    private Dictionary<string, SoundClipSO> _soundsDic;
    private Dictionary<SoundClipSO, float> _playbackTimeDic;

    private SoundClipSO _currentClip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _soundsDic = new Dictionary<string, SoundClipSO>();
        _playbackTimeDic = new Dictionary<SoundClipSO, float>();

        foreach (SoundClipSO sound in _soundClips)
        {
            _soundsDic.Add(sound.Name, sound);
        }
    }

    public void PlaySound(string clipName)
    {
        if (_soundsDic.TryGetValue(clipName, out SoundClipSO sound))
        {
            PlaySound(sound);
        }
    }

    public void PlaySound(SoundClipSO sound)
    {
        SaveCurrentPlaybackTime();
        sound.Play(_audioSource, GetPlaybackTime(sound));
        _currentClip = sound;
    }

    public void PlaySoundOneShot(string clipName)
    {
        if (_soundsDic.TryGetValue(clipName, out SoundClipSO sound))
        {
            PlaySoundOneShot(sound);
        }
    }

    public void PlaySoundOneShot(SoundClipSO sound)
    {
        sound.PlayOneShot(_audioSource);
    }

    public float GetPlaybackTime(SoundClipSO sound)
    {
        return _playbackTimeDic.ContainsKey(sound)
            ? _playbackTimeDic[sound]
            : 0f;
    }

    private void SaveCurrentPlaybackTime()
    {
        if (_currentClip != null)
        {
            if (_currentClip.AllowPlaybackSave)
            {
                _playbackTimeDic[_currentClip] = _audioSource.time;
            }
        }
    }

    private void Reset()
    {
        GetComponent<AudioSource>().playOnAwake = false;
    }
}
