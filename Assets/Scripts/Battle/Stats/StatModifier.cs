using UnityEngine;

/// <summary>
/// Модификатор стата. 
/// </summary>
[System.Serializable]
public class StatModifier
{
    [SerializeField] private float _value = 0f;
    [SerializeField] private ModifierType _type = ModifierType.Flat;
    /// <summary>
    /// Значение модификатора.
    /// </summary>
    public float Value => _value;
    /// <summary>
    /// Тип модификатора.
    /// </summary>
    public ModifierType Type => _type;
    /// <summary>
    /// Приоритет применения модификатора.
    /// </summary>
    public int Priority { get; private set; }
    /// <summary>
    /// Источник модификатора.
    /// </summary>
    public Object Source { get; private set; }

    public StatModifier(float value, ModifierType type, int priority, Object source)
    {
        _value = value;
        _type = type;
        Priority = priority;
        Source = source;
    }

    public StatModifier(float value, ModifierType type, Object source) : this(value, type, (int)type, source) { }

    public StatModifier(float value, ModifierType type) : this(value, type, (int)type, null) { }

    public StatModifier()
    {
        Priority = (int)_type;
        Source = null;
    }
    /// <summary>
    /// Применить модификатор к текущему значению стата.
    /// </summary>
    public float Apply(float valueToModify, float modifier, ModifierType type)
    {
        return type == ModifierType.Flat 
            ? CalculateFlat(valueToModify, modifier) 
            : CalculatePercent(valueToModify, modifier);
    }
    /// <summary>
    /// Рассчитать для прямого модификатора.
    /// </summary>
    private float CalculateFlat(float valueToModify, float modifier)
    {
        return valueToModify + modifier;
    }
    /// <summary>
    /// Рассчитать для процентного модификатора.
    /// </summary>
    private float CalculatePercent(float valueToModify, float modifier)
    {
        return valueToModify * (1 + (modifier / 100f));
    }
}
