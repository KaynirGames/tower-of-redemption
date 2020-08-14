using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float _baseValue = 0; // Базовое значение стата.

    private List<StatModifier> _modifiers; // Список модификаторов базового значения стата.
    private float _currentValue; // Текущее значение стата с модификаторами.
    private bool _modifiersChanged; // Для определения изменений в модификаторах.

    public Stat(float baseValue)
    {
        _baseValue = baseValue;
        _modifiers = new List<StatModifier>();
        _currentValue = _baseValue;
        _modifiersChanged = false;
    }

    public Stat() : this(0f) { }

    /// <summary>
    /// Получить значение стата с учетом модификаторов (если они имеются).
    /// </summary>
    public float GetValue()
    {
        if (_modifiersChanged)
        {
            _currentValue = _baseValue;

            if (_modifiers.Count > 0)
            {
                _modifiers.Sort(CompareByPriority);
                float percentAddSum = 0;

                for (int i = 0; i < _modifiers.Count; i++)
                {
                    StatModifier mod = _modifiers[i];

                    if (mod.Type == ModifierType.PercentAdditive)
                    {
                        percentAddSum += mod.Value;
                        // Если дошли до конца, либо следующий модификатор другого типа.
                        if (i + 1 >= _modifiers.Count || _modifiers[i + 1].Type != ModifierType.PercentAdditive)
                        {
                            // Применяем суммарный процент к стату.
                            _currentValue = mod.Apply(_currentValue, percentAddSum, mod.Type);
                        }
                    }
                    else
                    {
                        _currentValue = mod.Apply(_currentValue, mod.Value, mod.Type);
                    }
                }

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
    /// <summary>
    /// Убирает все модификаторы, полученные от источника.
    /// </summary>
    public void RemoveSourceModifiers(Object source)
    {
        for (int i = _modifiers.Count - 1; i >= 0; i--)
        {
            if (_modifiers[i].Source == source)
            {
                _modifiers.RemoveAt(i);
                _modifiersChanged = true;
            }
        }
    }
    /// <summary>
    /// Сравнить приоритет применения модификаторов.
    /// </summary>
    private int CompareByPriority(StatModifier modA, StatModifier modB)
    {
        if (modA.Priority < modB.Priority) return 1;
        else if (modA.Priority > modB.Priority) return -1;
        return 0; // Если приоритет применения совпадает.
    }
}