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
    /// <summary>
    /// Источник модификатора.
    /// </summary>
    public Object Source { get; private set; }

    public StatModifier(float value, Object source)
    {
        _value = value;
        Source = source;
    }

    public StatModifier(float value) : this(value, null) { }

    /// <summary>
    /// Применить модификатор к текущему значению стата.
    /// </summary>
    public float Apply(float currentValue)
    {
        return currentValue + _value;
    }
}
