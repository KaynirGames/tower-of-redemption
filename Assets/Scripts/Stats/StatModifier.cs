using UnityEngine;

/// <summary>
/// Модификатор стата. 
/// </summary>
[System.Serializable]
public class StatModifier
{
    [SerializeField] private float _value = 0;

    /// <summary>
    /// Значение модификатора.
    /// </summary>
    public float Value => _value;

    public StatModifier(float value)
    {
        _value = value;
    }
    /// <summary>
    /// Применить модификатор к базовому значению стата.
    /// </summary>
    public float ApplyModifier(float baseValue)
    {
        return baseValue + _value;
    }
}
