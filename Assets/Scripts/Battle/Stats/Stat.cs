using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float _baseValue = 0;

    public float BaseValue => _baseValue;

    private bool _hasChanges;
    private List<StatModifier> _modifiers;

    private float _finalValue;
    private float _minValue;
    private float _maxValue;

    public Stat(float baseValue, float minValue, float maxValue)
    {
        _baseValue = baseValue;
        _minValue = minValue;
        _maxValue = maxValue;
        _finalValue = baseValue;

        _modifiers = new List<StatModifier>();
        _hasChanges = false;
    }

    public Stat(float baseValue) : this(baseValue, 0, int.MaxValue) { }

    public float GetFinalValue()
    {
        if (_hasChanges)
        {
            UpdateFinalValue();
        }

        return _finalValue;
    }

    public void AddModifier(StatModifier modifier)
    {
        if (modifier.Value != 0)
        {
            _modifiers.Add(modifier);
            _hasChanges = true;
        }
    }

    public void RemoveModifier(StatModifier modifier)
    {
        if (modifier.Value != 0)
        {
            _modifiers.Remove(modifier);
            _hasChanges = true;
        }
    }

    public void RemoveModifiers(object modifierSource)
    {
        _modifiers.RemoveAll(mod => mod.Source == modifierSource);
        _hasChanges = true;
    }

    private void UpdateFinalValue()
    {
        _finalValue = _baseValue;

        if (_modifiers.Count > 0)
        {
            _modifiers.ForEach(mod => _finalValue += mod.Value);

            _finalValue = Mathf.Clamp(Mathf.Round(_finalValue),
                                      _minValue,
                                      _maxValue);
        }

        _hasChanges = false;
    }
}