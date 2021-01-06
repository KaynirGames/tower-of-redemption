using UnityEngine;

[RequireComponent(typeof(SoundController))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private SoundController _sounds;

    private void Awake()
    {
        Instance = this;

        _sounds = GetComponent<SoundController>();
    }

    public void PlayMusic(string clipName)
    {
        _sounds.PlaySound(clipName);
    }

    public void PlayMusic(SoundClipSO soundSO)
    {
        _sounds.PlaySound(soundSO);
    }
}
