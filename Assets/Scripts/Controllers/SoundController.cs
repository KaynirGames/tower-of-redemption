using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] private List<SoundEffect> _soundEffects = new List<SoundEffect>();

    private AudioSource _audioSource;

    private Dictionary<string, SoundEffect> _soundEffectDic;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _soundEffectDic = new Dictionary<string, SoundEffect>();

        foreach (SoundEffect sound in _soundEffects)
        {
            _soundEffectDic.Add(sound.ToString(), sound);
        }
    }

    public void PlaySound(string clipName)
    {
        if (_soundEffectDic.TryGetValue(clipName, out SoundEffect sound))
        {
            sound.Play(_audioSource);
        }
    }

    [System.Serializable]
    private struct SoundEffect
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private string _clipName;

        public void Play(AudioSource audioSource)
        {
            audioSource.PlayOneShot(_audioClip);
        }

        public override string ToString()
        {
            return _clipName;
        }
    }
}
