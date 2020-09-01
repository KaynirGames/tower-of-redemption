using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float _baseValue = 0;

    private readonly List<StatModifier> _modifiers = new List<StatModifier>();
    private float _finalValue = 0;
    private bool _hasChanges = false;

    public Stat(float baseValue)
    {
        _baseValue = baseValue;
        _finalValue = _baseValue;
        _hasChanges = false;
    }

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

    public void RemoveSourceModifiers(Object source)
    {
        for (int i = _modifiers.Count - 1; i >= 0; i--)
        {
            if (_modifiers[i].Source == source)
            {
                _modifiers.RemoveAt(i);
                _hasChanges = true;
            }
        }
    }

    private void UpdateFinalValue()
    {
        _finalValue = _baseValue;

        if (_modifiers.Count > 0)
        {
            _modifiers.ForEach(mod => _finalValue = mod.Apply(_finalValue));

            if (_finalValue < 0) { _finalValue = 0; }
        }

        _hasChanges = false;
        _finalValue = Mathf.Round(_finalValue);
    }
}