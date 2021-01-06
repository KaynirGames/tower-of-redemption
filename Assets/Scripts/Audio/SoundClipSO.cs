using UnityEngine;

[CreateAssetMenu(fileName = "New SoundClipSO", menuName = "Scriptable Objects/Audio/Sound Clip SO")]
public class SoundClipSO : ScriptableObject
{
    [SerializeField] private AudioClip _clip = null;
    [SerializeField] private string _name = null;
    [SerializeField] private bool _loop = false;
    [SerializeField] private bool _allowPlaybackSave = false;
    [SerializeField, Range(0f, 1f)] private float _volume = 1f;
    [SerializeField, Range(-3, 3)] private int _pitch = 1;
    [SerializeField, Range(-1f, 1f)] private float _stereoPan = 0f;

    public AudioClip Clip => _clip;
    public string Name => _name;
    public bool AllowPlaybackSave => _allowPlaybackSave;

    public void Play(AudioSource audioSource, float playbackTime = 0)
    {
        audioSource.clip = _clip;
        audioSource.loop = _loop;
        audioSource.volume = _volume;
        audioSource.pitch = _pitch;
        audioSource.panStereo = _stereoPan;
        audioSource.time = playbackTime;
        audioSource.Play();
    }

    public void PlayOneShot(AudioSource audioSource)
    {
        float volumeScale = audioSource.volume > _volume
            ? _volume / audioSource.volume
            : 1f;

        audioSource.PlayOneShot(_clip, volumeScale);
    }
}
