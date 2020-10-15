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

    public string Key => _key;

    public string Value => _value;

    public TranslationLine(string key, string value)
    {
        _key = key;
        _value = value;
    }
}
