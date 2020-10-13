using System;
using UnityEngine;

/// <summary>
/// Переведенный текст.
/// </summary>
[Serializable]
public class TranslatedText
{
    [SerializeField] private string _key = string.Empty;

    public string Value => Translator.GetTranslationLine(_key);
    public string Key => _key;
    
    public TranslatedText(string key)
    {
        _key = key;
    }
}