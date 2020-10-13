using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Переводчик игровых текстов.
/// </summary>
public static class Translator
{
    private static Dictionary<string, string> _translation;

    public static void SetTranslation(SystemLanguage language)
    {
        _translation = TranslationSystem.LoadTranslationData(language);
    }

    public static string GetTranslationLine(string key)
    {
        return _translation == null || !_translation.ContainsKey(key)
            ? "Translation is missing."
            : _translation[key];
    }
}