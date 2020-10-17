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
    private static readonly string _folderPath = string.Format("{0}/Resources/Translations", Application.dataPath);

    private static readonly Dictionary<SystemLanguage, string> _translationFileNames = new Dictionary<SystemLanguage, string>
    {
        { SystemLanguage.Russian, "translation_RU" },
        { SystemLanguage.English, "translation_EN" }
    };

    private static readonly SystemLanguage _defaultLanguage = SystemLanguage.English;

    public static string GetFilePath(SystemLanguage language)
    {
        return string.Format("{0}/{1}.json", _folderPath, GetFileName(language));
    }

    public static string GetFilePath(string fileName)
    {
        return string.Format("{0}/{1}.json", _folderPath, fileName);
    }

    public static Dictionary<string, string> LoadTranslationData(SystemLanguage language)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        TranslationData data = (TranslationData)JsonLoader.LoadDataFromJson(GetFilePath(language),
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

    private static string GetFileName(SystemLanguage language)
    {
        if (_translationFileNames.ContainsKey(language))
        {
            return _translationFileNames[language];
        }
        else
        {
            Debug.LogWarning(
                $"{language} translation file is missing. Loading by default: {_defaultLanguage}.");

            return _translationFileNames[_defaultLanguage];
        }
    }

#if UNITY_EDITOR
    private static Dictionary<string, string> _translationRU;
    private static Dictionary<string, string> _translationEN;

    private static bool _isInitialized = false;

    public static void Initialize()
    {
        if (!_isInitialized)
        {
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            _translationRU = LoadTranslationData(SystemLanguage.Russian);
            _translationEN = LoadTranslationData(SystemLanguage.English);

            _isInitialized = true;
        }
    }

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

    public static void RemoveLine(string key, SystemLanguage language)
    {
        Dictionary<string, string> translation = GetDictionary(language);

        if (translation.ContainsKey(key))
        {
            translation.Remove(key);
        }
    }

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

    public static void SaveChanges(SystemLanguage language, string backupFileName)
    {
        Dictionary<string, string> translation = GetDictionary(language);
        List<TranslationLine> lines = new List<TranslationLine>();

        foreach (var line in translation)
        {
            lines.Add(new TranslationLine(line.Key, line.Value));
        }

        TranslationData data = new TranslationData(lines.ToArray());

        JsonLoader.SaveDataToJson(GetFilePath(language), data);

        if (backupFileName != string.Empty)
        {
            JsonLoader.SaveDataToJson(GetFilePath(backupFileName), data);
        }

        AssetDatabase.Refresh();
        Translator.SetTranslation(_defaultLanguage);
    }

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

    private static void UpdateLine(string key, string newValue, SystemLanguage language)
    {
        Dictionary<string, string> translation = GetDictionary(language);

        if (translation.ContainsKey(key))
        {
            translation[key] = newValue;
        }
    }

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
