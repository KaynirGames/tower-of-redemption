using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundClipSO), true)]
public class SoundClipSOEditor : Editor
{
    private SoundClipSO _soundClipSO;
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _soundClipSO = (SoundClipSO)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Preview Sound"))
        {
            if (_soundClipSO.Clip != null)
            {
                ClearPreviewAudioSource();

                _audioSource = new GameObject("Sound Preview").AddComponent<AudioSource>();
                _audioSource.gameObject.hideFlags = HideFlags.HideAndDontSave;
                _soundClipSO.Play(_audioSource);
            }
        }

        if (GUILayout.Button("Stop Preview"))
        {
            ClearPreviewAudioSource();
        }
    }

    private void ClearPreviewAudioSource()
    {
        if (_audioSource != null)
        {
            DestroyImmediate(_audioSource.gameObject);
            _audioSource = null;
        }
    }

    private void OnDisable()
    {
        ClearPreviewAudioSource();
    }
}
