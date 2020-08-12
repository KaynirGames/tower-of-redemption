using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Переводчик игровых текстов.
/// </summary>
public static class Translator
{
    private static Dictionary<string, string> _translation; // Текущий перевод игровых текстов.
    /// <summary>
    /// Установить текущий перевод.
    /// </summary>
    public static void SetTranslation(SystemLanguage language)
    {
        _translation = TranslationSystem.LoadTranslationData(language);
    }
    /// <summary>
    /// Получить строку перевода.
    /// </summary>
    public static string GetTranslationLine(string key)
    {
        return _translation == null || !_translation.ContainsKey(key)
            ? "Перевод отсутствует."
            : _translation[key];
    }
}