using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Переводчик игровых текстов.
/// </summary>
public static class Translator
{
    public static SystemLanguage CurrentLanguage { get; private set; } = SystemLanguage.English;

    private static Dictionary<string, string> _translation;

    public static void SetTranslation(SystemLanguage language)
    {
        CurrentLanguage = language;
        _translation = TranslationSystem.LoadTranslationData(language);
    }

    public static string GetTranslationLine(string key)
    {
        return _translation == null || !_translation.ContainsKey(key)
            ? "Translation is missing."
            : _translation[key];
    }
}