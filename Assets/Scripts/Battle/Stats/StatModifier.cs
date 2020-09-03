using UnityEngine;

/// <summary>
/// Модификатор стата. 
/// </summary>
[System.Serializable]
public class StatModifier
{
    [SerializeField] private float _value = 0f;

    public float Value => _value;

    public StatModifier(float value)
    {
        _value = value;
    }

    public float Apply(float valueToModify)
    {
        return valueToModify + _value;
    }
}
