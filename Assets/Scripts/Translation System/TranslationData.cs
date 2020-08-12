using System;
using UnityEngine;

/// <summary>
/// Класс для переноса строк перевода и сериализации в json.
/// </summary>
[Serializable]
public class TranslationData
{
    [SerializeField] private TranslationLine[] _translationLines;
    /// <summary>
    /// Строки перевода.
    /// </summary>
    public TranslationLine[] TranslationLines => _translationLines;

    public TranslationData(TranslationLine[] translationLines)
    {
        _translationLines = translationLines;
    }
}