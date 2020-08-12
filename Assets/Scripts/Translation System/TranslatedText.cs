using System;
using UnityEngine;

/// <summary>
/// Переведенный текст.
/// </summary>
[Serializable]
public class TranslatedText
{
    [SerializeField] private string _key = string.Empty; // Ключ переведенного текста.
    /// <summary>
    /// Значение строки перевода.
    /// </summary>
    public string Value => Translator.GetTranslationLine(_key);
    
    public TranslatedText(string key)
    {
        _key = key;
    }
}