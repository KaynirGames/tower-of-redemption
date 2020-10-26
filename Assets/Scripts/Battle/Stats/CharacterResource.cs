using System;
using UnityEngine;

[Serializable]
public class CharacterResource
{
    public delegate void OnResourceChange(float currentValue);

    public event OnResourceChange OnChange = delegate { };

    [SerializeField] private Stat _maxValue;

    public Stat MaxValue => _maxValue;

    public float CurrentValue { get; private set; }

    private float _previousMaxValue;

    public CharacterResource(Stat stat, float currentValue)
    {
        _maxValue = stat;
        CurrentValue = currentValue;

        _previousMaxValue = stat.GetFinalValue();
    }

    public void ChangeResource(float amount)
    {
        CurrentValue += amount;
        FixCurrentValue(false);    
    }

    public void FixCurrentValue(bool maxValueChanged)
    {
        if (maxValueChanged)
        {
            CurrentValue += _maxValue.GetFinalValue() - _previousMaxValue;
            _previousMaxValue = _maxValue.GetFinalValue();
        }

        CurrentValue = Mathf.Round(CurrentValue);
        CurrentValue = Mathf.Clamp(CurrentValue,
                                   0,
                                   _maxValue.GetFinalValue());

        OnChange.Invoke(CurrentValue);
    }
}
