using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Модификатор стата. 
/// </summary>
[System.Serializable]
public class StatModifier
{
    /// <summary>
    /// Значение модификатора.
    /// </summary>
    public readonly float Value = 0;

    public StatModifier(float value)
    {
        Value = value;
    }

    /// <summary>
    /// Применить модификатор к базовому значению стата.
    /// </summary>
    public float ApplyModifier(float baseValue)
    {
        return baseValue + Value;
    }
}
