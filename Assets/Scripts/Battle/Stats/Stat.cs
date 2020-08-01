using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float _baseValue = 0; // Базовое значение стата.

    private readonly List<StatModifier> _modifiers; // Список модификаторов базового значения стата.
    private float _currentValue; // Текущее значение стата с модификаторами.
    private bool _modifiersChanged; // Для определения изменений в модификаторах.

    public Stat(float baseValue) : this()
    {
        _baseValue = baseValue;
    }

    public Stat()
    {
        _modifiers = new List<StatModifier>();
        _currentValue = _baseValue;
        _modifiersChanged = false;
    }

    /// <summary>
    /// Возвращает значение стата с учетом модификаторов (если они имеются).
    /// </summary>
    public float GetValue()
    {
        if (_modifiersChanged)
        {
            _currentValue = _baseValue;

            if (_modifiers.Count > 0)
            {
                _modifiers.ForEach(mod => _currentValue = mod.Apply(_currentValue));
                if (_currentValue < 0) _currentValue = Mathf.Max(_currentValue, 0);
            }

            _modifiersChanged = false;
            _currentValue = Mathf.Round(_currentValue);

            return _currentValue;
        }
        else
        {
            return _currentValue;
        }
    }
    /// <summary>
    /// Добавить модификатор стата.
    /// </summary>
    public void AddModifier(StatModifier modifier)
    {
        if (modifier.Value != 0)
        {
            _modifiers.Add(modifier);
            _modifiersChanged = true;
        }
    }
    /// <summary>
    /// Убрать модификатор стата.
    /// </summary>
    public void RemoveModifier(StatModifier modifier)
    {
        if (modifier.Value != 0)
        {
            _modifiers.Remove(modifier);
            _modifiersChanged = true;
        }
    }
}