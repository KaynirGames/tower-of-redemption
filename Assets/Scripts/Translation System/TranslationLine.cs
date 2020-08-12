using System;
using UnityEngine;

/// <summary>
/// Строка перевода.
/// </summary>
[Serializable]
public struct TranslationLine
{
    [SerializeField] private string _key;
    [SerializeField] private string _value;
    /// <summary>
    /// Ключ строки перевода.
    /// </summary>
    public string Key => _key;
    /// <summary>
    /// Значение строки перевода.
    /// </summary>
    public string Value => _value;

    public TranslationLine(string key, string value)
    {
        _key = key;
        _value = value;
    }
}
