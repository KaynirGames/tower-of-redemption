using KaynirGames.Tools;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Система управления переводом текстов.
/// </summary>
public static class TranslationSystem
{
    /// <summary>
    /// Путь к папке с файлами переводов.
    /// </summary>
    private static readonly string _folderPath = string.Format("{0}/Resources/Translations", Application.dataPath);
    /// <summary>
    /// Названия файлов с переводами.
    /// </summary>
    private static readonly Dictionary<SystemLanguage, string> _fileNames = new Dictionary<SystemLanguage, string>
    {
        { SystemLanguage.Russian, "translation_RU" },
        { SystemLanguage.English, "translation_EN" }
    };
    /// <summary>
    /// Язык перевода по умолчанию.
    /// </summary>
    private static readonly SystemLanguage _defaultLanguage = SystemLanguage.English;
    /// <summary>
    /// Получить путь к файлу с переводом.
    /// </summary>
    public static string GetFilePath(SystemLanguage language)
    {
        return string.Format("{0}/{1}.json", _folderPath, GetFileName(language));
    }
    /// <summary>
    /// Получить путь к файлу.
    /// </summary>
    public static string GetFilePath(string fileName)
    {
        return string.Format("{0}/{1}.json", _folderPath, fileName);
    }
    /// <summary>
    /// Загрузить данные перевода в словарь.
    /// </summary>
    public static Dictionary<string, string> LoadTranslationData(SystemLanguage language)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        TranslationData data = (TranslationData)JsonLoader.LoadData(
            GetFilePath(language),
            typeof(TranslationData));

        if (data != null)
        {
            foreach (TranslationLine line in data.TranslationLines)
            {
                dictionary.Add(line.Key, line.Value);
            }
        }

        return dictionary;
    }
    /// <summary>
    /// Получить название файла с переводом.
    /// </summary>
    private static string GetFileName(SystemLanguage language)
    {
        if (_fileNames.ContainsKey(language))
        {
            return _fileNames[language];
        }
        else
        {
            Debug.LogWarning(
                $"{language} translation file is missing. Loading by default: {SystemLanguage.Russian}.");

            return _fileNames[SystemLanguage.Russian];
        }
    }

#if UNITY_EDITOR
    private static Dictionary<string, string> _translationRU;
    private static Dictionary<string, string> _translationEN;
    /// <summary>
    /// Инициализировать систему управления переводом.
    /// </summary>
    public static void Init()
    {
        if (!Directory.Exists(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);
        }

        _translationRU = LoadTranslationData(SystemLanguage.Russian);
        _translationEN = LoadTranslationData(SystemLanguage.English);
    }
    /// <summary>
    /// Добавить/обновить строку перевода.
    /// </summary>
    public static void AddLine(string key, string value, SystemLanguage language)
    {
        Dictionary<string, string> translation = GetDictionary(language);

        if (translation.ContainsKey(key))
        {
            UpdateLine(key, value, language);
        }
        else
        {
            translation.Add(key, value);
        }
    }
    /// <summary>
    /// Удалить строку перевода.
    /// </summary>
    public static void RemoveLine(string key, SystemLanguage language)
    {
        Dictionary<string, string> translation = GetDictionary(language);

        if (translation.ContainsKey(key))
        {
            translation.Remove(key);
        }
    }
    /// <summary>
    /// Обновить ключ перевода.
    /// </summary>
    public static void UpdateKey(string oldKey, string newKey, SystemLanguage language)
    {
        Dictionary<string, string> translation = GetDictionary(language);

        if (translation.ContainsKey(oldKey))
        {
            string oldValue = translation[oldKey];
            RemoveLine(oldKey, language);
            AddLine(newKey, oldValue, language);
        }
    }
    /// <summary>
    /// Сохранить изменения в файл перевода (с опциональной резервной копией).
    /// </summary>
    public static void SaveChanges(SystemLanguage language, string backupFileName)
    {
        Dictionary<string, string> translation = GetDictionary(language);
        List<TranslationLine> lines = new List<TranslationLine>();

        foreach (var line in translation)
        {
            lines.Add(new TranslationLine(line.Key, line.Value));
        }

        TranslationData data = new TranslationData(lines.ToArray());

        JsonLoader.SaveData(GetFilePath(language), data);

        if (backupFileName != string.Empty)
        {
            JsonLoader.SaveData(GetFilePath(backupFileName), data);
        }

        AssetDatabase.Refresh();
        Translator.SetTranslation(_defaultLanguage);
    }
    /// <summary>
    /// Получить значение переведенного текста.
    /// </summary>
    public static string GetValue(string key, SystemLanguage language)
    {
        Dictionary<string, string> translation = GetDictionary(language);

        if (translation.ContainsKey(key))
        {
            return GetDictionary(language)[key];
        }
        else
        {
            return "Translation is missing.";
        }
    }
    /// <summary>
    /// Обновить строку перевода.
    /// </summary>
    private static void UpdateLine(string key, string newValue, SystemLanguage language)
    {
        Dictionary<string, string> translation = GetDictionary(language);

        if (translation.ContainsKey(key))
        {
            translation[key] = newValue;
        }
    }
    /// <summary>
    /// Получить словарь перевода.
    /// </summary>
    private static Dictionary<string, string> GetDictionary(SystemLanguage language)
    {
        if (language == SystemLanguage.Russian)
        {
            return _translationRU;
        }
        else if (language == SystemLanguage.English)
        {
            return _translationEN;
        }

        return null;
    }
#endif
}
