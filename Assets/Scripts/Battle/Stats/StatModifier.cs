using UnityEngine;

/// <summary>
/// Модификатор стата. 
/// </summary>
[System.Serializable]
public class StatModifier
{
    [SerializeField] private float _value = 0f;

    public float Value => _value;

    public object Source { get; private set; }

    public StatModifier(float value, object source)
    {
        _value = value;
        Source = source;
    }

    public StatModifier(float value) : this(value, null) { }
}